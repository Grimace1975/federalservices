using System;
using System.ServiceModel;
using Federal.Storage;
using System.Runtime.Serialization;

namespace Federal.Storage
{
	[ServiceContract(Namespace = "http://neurox.local")]
	public interface IStorageServiceBase : IStorageClient
	{
		[OperationContract]
		ServiceRegistration GetRegisteredService(string entityType, string name, out ServiceEntityBase[] entities);
	}

	public abstract class StorageServiceBase : IStorageServiceBase
	{
		public StorageServiceBase()
		{
			if (KnownServiceEntityTypes == null)
				throw new NullReferenceException("KnownServiceEntityTypes");
		}

		public static Type[] KnownServiceEntityTypes { get; set; }
		public abstract void SetServiceNodeState(int nodeType, int id, ServiceNodeState nodeState, object context);
		public abstract ServiceRegistration GetRegisteredService(string entityType, string name, out ServiceEntityBase[] entities);
		public ServiceRegistration GetRegisteredService<TEntity>(string name, out TEntity[] entities)
			where TEntity : ServiceEntityBase { throw new NotSupportedException(); }
	}
}
