using System;
using Federal.Storage;
using Client.StorageProxy;
using Example.Storage.Entity;
namespace Example
{
	/// <summary>
	/// ProcessService
	/// </summary>
	public class ObjectService : RegisteredService<ObjectService, StorageServiceClient>, IObjectService
	{
		private ObjectCollection _objects = new ObjectCollection();

		protected ObjectService()
			: base(0) { }

		public ObjectCollection Objects
		{
			get { return _objects; }
		}

		public override void RegisterWithStorage(StorageServiceClient storageClient)
		{
			Registration = StorageClientHelper<StorageServiceClient>.RegisterWithStorageAndSync<ObjectEntity, IObject>(storageClient, Name, _objects);
		}
	}
}
