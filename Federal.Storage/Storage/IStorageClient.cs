using System;
using System.ServiceModel;
namespace Federal.Storage
{
	/// <summary>
	/// IStorageClient
	/// </summary>
	[ServiceContract(Namespace = "http://neurox.local")]
	public interface IStorageClient
	{
		ServiceRegistration GetRegisteredService<TEntity>(string name, out TEntity[] entities)
			where TEntity : ServiceEntityBase;
		[OperationContract]
		void SetServiceNodeState(int nodeType, int id, ServiceNodeState nodeState, object context);
	}
}
