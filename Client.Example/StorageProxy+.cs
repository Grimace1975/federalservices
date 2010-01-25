using Federal.Storage;
namespace Client.StorageProxy
{
	public partial class StorageServiceClient : IStorageClient
	{
		public ServiceRegistration GetRegisteredService<TEntity>(string name, out TEntity[] entities)
			where TEntity : ServiceEntityBase { return StorageClientHelper<StorageServiceClient>.GetRegisteredService<TEntity>(GetRegisteredService, name, out entities); }
	}
}
