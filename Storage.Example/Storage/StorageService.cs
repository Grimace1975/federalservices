using System;
using System.ServiceModel;
using Federal.Storage;
using Example.Storage.Entity;
namespace Example.Storage
{
	[ServiceContract]
	public interface IStorageService : IStorageServiceBase
	{
	}

	public class StorageService : StorageServiceBase, IStorageService
	{
		public override void SetServiceNodeState(int nodeType, int id, ServiceNodeState nodeState, object context)
		{
			Console.WriteLine("NodeState: " + id + " - " + nodeState.ToString());
			//s_storage.SetServiceNodeState(nodeType, id, nodeState);
		}
		public override ServiceRegistration GetRegisteredService(string entityType, string name, out ServiceEntityBase[] entities)
		{
			Console.WriteLine("Register: " + name);
			switch (entityType)
			{
				case "Example.Storage.Entity.ProcessEntity":
					entities = new[] { new ProcessEntity { Id = 10, Name = "Process", GridIds = new[] { 100 } } };
					return new ServiceRegistration { Id = 1, Name = "Service" };
				case "Example.Storage.Entity.SensorEntity":
					entities = new[] { new SensorEntity { Id = 20, Name = "Sensor" } };
					return new ServiceRegistration { Id = 2, Name = "Service" };
				default:
					throw new InvalidOperationException();
			}
		}
	}
}
