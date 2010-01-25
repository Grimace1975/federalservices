using Microsoft.ManagementConsole;
using System;
using System.Threading;
[assembly: System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.RequestMinimum, Unrestricted = true)]

namespace Federal.Node
{
	/// <summary>
	/// Node base
	/// </summary>
	public class NodeBase : ScopeNode
	{
		private AsyncStatus _expandStatus;

		/// <summary>
		/// Standards the collection expand.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="status">The status.</param>
		/// <param name="observable">The observable.</param>
		/// <param name="mapper">The mapper.</param>
		protected void StandardCollectionExpand<TModel>(AsyncStatus status, IObservable<TModel[]> observable, Func<TModel, ScopeNode> mapper)
		{
			_expandStatus = status;
			_expandStatus.EnableManualCompletion();
			Children.Clear();
			observable.Subscribe(new CollectionObserver<TModel>(this, status, mapper));
		}

		/// <summary>
		/// Standards the single expand.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <param name="status">The status.</param>
		/// <param name="observable">The observable.</param>
		/// <param name="builder">The builder.</param>
		protected void StandardSingleExpand<TModel>(AsyncStatus status, IObservable<TModel> observable, Func<TModel, ScopeNode[]> builder)
		{
			_expandStatus = status;
			_expandStatus.EnableManualCompletion();
			Children.Clear();
			observable.Subscribe(new SingleObserver<TModel>(this, status, builder));
		}

		#region Observers
		/// <summary>
		/// CollectionObserver
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		public class CollectionObserver<TModel> : IObserver<TModel[]>
		{
			private AsyncStatus _status;
			private NamespaceSnapInBase _snapIn;
			private ScopeNodeCollection _children;
			private Func<TModel, ScopeNode> _mapper;

			public CollectionObserver(ScopeNode parent, AsyncStatus status, Func<TModel, ScopeNode> mapper)
			{
				_snapIn = parent.SnapIn;
				_children = parent.Children;
				_status = status;
				_mapper = mapper;
				_status.ReportProgress(0, 0, "Fetching...");
			}
			public void OnCompleted()
			{
				_status.Complete("Load complete.", true);
			}
			public void OnError(Exception ex)
			{
				_status.Complete("Error.", true);
			}
			public void OnNext(TModel[] models)
			{
				_snapIn.BeginInvoke((Action<TModel[]>)AddChildren, new object[] { models });
				_status.ReportProgress(0, 0, "Loading...");
			}
			private void AddChildren(TModel[] models)
			{
				var nodes = new ScopeNode[models.Length];
				for (int modelIndex = 0; modelIndex < models.Length; modelIndex++)
				{
					nodes[modelIndex] = _mapper(models[modelIndex]);
				}
				_children.AddRange(nodes);
			}
		}

		/// <summary>
		/// SingleObserver
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		public class SingleObserver<TModel> : IObserver<TModel>
		{
			private AsyncStatus _status;
			private NamespaceSnapInBase _snapIn;
			private ScopeNodeCollection _children;
			private Func<TModel, ScopeNode[]> _builder;

			public SingleObserver(ScopeNode parent, AsyncStatus status, Func<TModel, ScopeNode[]> builder)
			{
				_snapIn = parent.SnapIn;
				_children = parent.Children;
				_status = status;
				_builder = builder;
				_status.ReportProgress(0, 0, "Fetching...");
			}
			public void OnCompleted()
			{
				_status.Complete("Load complete.", true);
			}
			public void OnError(Exception ex)
			{
				_status.Complete("Error.", true);
			}
			public void OnNext(TModel model)
			{
				_snapIn.BeginInvoke((Action<TModel>)AddChildren, new object[] { model });
				_status.ReportProgress(0, 0, "Loading...");
			}
			private void AddChildren(TModel model)
			{
				var nodes = _builder(model);
				if (nodes != null)
				{
					_children.AddRange(nodes);
				}
			}
		}
		#endregion
	}
}
