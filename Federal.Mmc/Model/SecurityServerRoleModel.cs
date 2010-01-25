using System.Data.SqlClient;

namespace Federal.Model
{
	/// <summary>
	/// Server-Role Model
	/// </summary>
	public class SecurityServerRoleModel : ModelBase
	{
		public class Ordinal
		{
			public int Name;
			public Ordinal(SqlDataReader r)
			{
				Name = r.GetOrdinal("Name");
			}
		}
		/// <summary>
		/// The constructor.
		/// </summary>
		public SecurityServerRoleModel(Ordinal ordinal, SqlDataReader r)
		{
			Name = r.Field<string>(ordinal.Name);
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; protected set; }

		#region Fetch
		public class Fetch : CollectionChunkedFetch<Fetch, Ordinal, SecurityServerRoleModel>
		{
			public Fetch()
				: base(
					(() => @"
SELECT r.name AS [Name], 'Server[@Name=' + quotename(CAST(serverproperty(N'Servername') AS sysname),'''') + ']' + '/Role[@Name=' + quotename(r.name,'''') + ']' AS [Urn]
FROM sys.server_principals r
WHERE (r.type ='R')
ORDER BY [Name] ASC"),
					(r => new Ordinal(r)),
					((ordinal, r) => new SecurityServerRoleModel(ordinal, r))
				) { }
		}
		#endregion
	}
}
