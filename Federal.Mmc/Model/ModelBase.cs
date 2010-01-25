using Microsoft.ManagementConsole;
using System;
using System.Threading;
using System.Data.SqlClient;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Model
{
	/// <summary>
	/// Model base
	/// </summary>
	public class ModelBase
	{
		#region Observables
		/// <summary>
		/// CollectionChunkedFetch
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TOrdinal">The type of the ordinal.</typeparam>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		public class CollectionChunkedFetch<T, TOrdinal, TModel> : IObservable<TModel[]>
		{
			protected Func<string> SqlBuilder;
			protected Func<SqlDataReader, TOrdinal> OrdinalBuilder;
			protected Func<TOrdinal, SqlDataReader, TModel> ModelBuilder;

			public CollectionChunkedFetch(Func<string> sqlBuilder,
				Func<SqlDataReader, TOrdinal> ordinalBuilder,
				Func<TOrdinal, SqlDataReader, TModel> modelBuilder)
			{
				SqlBuilder = sqlBuilder;
				OrdinalBuilder = ordinalBuilder;
				ModelBuilder = modelBuilder;
			}

			public IDisposable Subscribe(IObserver<TModel[]> observer)
			{
				var thread = new Thread(new ParameterizedThreadStart(Execute));
				thread.Start(observer);
				return null;
			}

			private void Execute(object obj)
			{
				var observer = (IObserver<TModel[]>)obj;
				try
				{
					using (var sqlConnection = KernelDatabase.CreateSqlConnection())
					{
						sqlConnection.Open();
						using (var r = new SqlCommand(SqlBuilder(), sqlConnection).ExecuteReader())
						{
							var ordinal = OrdinalBuilder(r);
							var modelBuilder = ModelBuilder;
							int bufferIndex = 0;
							TModel[] buffer = new TModel[10];
							while (r.Read())
							{
								buffer[bufferIndex++] = modelBuilder(ordinal, r);
								if (bufferIndex < 10)
								{
									continue;
								}
								observer.OnNext(buffer);
								bufferIndex = 0;
								buffer = new TModel[10];
							}
							if (bufferIndex > 0)
							{
								var partialBuffer = new TModel[bufferIndex];
								Array.Copy(buffer, partialBuffer, bufferIndex);
								observer.OnNext(partialBuffer);
							}
						}
						observer.OnCompleted();
					}
				}
				catch (Exception ex)
				{
					observer.OnError(ex);
				}
			}
		}

		/// <summary>
		/// SingleFetch
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		public class SingleFetch<T, TModel> : IObservable<TModel>
		{
			protected Func<string> SqlBuilder;
			protected Func<SqlDataReader, TModel> ModelBuilder;

			public SingleFetch(Func<string> sqlBuilder,
				Func<SqlDataReader, TModel> modelBuilder)
			{
				SqlBuilder = sqlBuilder;
				ModelBuilder = modelBuilder;
			}

			public IDisposable Subscribe(IObserver<TModel> observer)
			{
				var thread = new Thread(new ParameterizedThreadStart(Execute));
				thread.Start(observer);
				return null;
			}

			private void Execute(object obj)
			{
				var observer = (IObserver<TModel>)obj;
				try
				{
					using (var sqlConnection = KernelDatabase.CreateSqlConnection())
					{
						sqlConnection.Open();
						using (var r = new SqlCommand(SqlBuilder(), sqlConnection).ExecuteReader())
						{
							if (r.Read())
							{
								var model = ModelBuilder(r);
								if (model != null)
								{
									observer.OnNext(model);
								}
							}
						}
						observer.OnCompleted();
						return;
					}
				}
				catch (Exception ex)
				{
					observer.OnError(ex);
				}
			}
		}
		#endregion
	}
}
