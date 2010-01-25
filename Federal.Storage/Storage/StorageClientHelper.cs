using System;
using System.Collections.Generic;
namespace Federal.Storage
{
	public static class StorageClientHelper<TStorageClient>
		where TStorageClient : IStorageClient
	{
		public delegate ServiceRegistration GetRegisteredServiceDelegate(string entityType, string name, out ServiceEntityBase[] entities);
		public static ServiceRegistration GetRegisteredService<TEntityRegistration>(GetRegisteredServiceDelegate getRegisteredService, string name, out TEntityRegistration[] entities)
			where TEntityRegistration : ServiceEntityBase
		{
			ServiceEntityBase[] entities2;
			var serviceRegistration = getRegisteredService(typeof(TEntityRegistration).FullName, name, out entities2);
			entities = new TEntityRegistration[entities2.Length];
			for (int entityIndex = 0; entityIndex < entities2.Length; entityIndex++)
				entities[entityIndex] = (entities2[entityIndex] as TEntityRegistration);
			return serviceRegistration;
		}

		public static ServiceRegistration RegisterWithStorageAndSync<TEntity, IObject>(TStorageClient storageClient, string name, Dictionary<string, IObject> set)
			where TEntity : ServiceEntityBase
			where IObject : class, IServiceObject
		{
			TEntity[] entities;
			var registration = storageClient.GetRegisteredService<TEntity>(name, out entities);
			if (registration == null)
				throw new Exception("Invalid registration for this service.");
			if (entities != null)
				foreach (var entity in entities)
				{
					IObject obj;
					string rowVersion = entity.RowVersion;
					string name2 = entity.Name;
					// delete
					if (set.TryGetValue(name2, out obj))
					{
						if (obj.RowVersion == rowVersion)
							continue;
						set.Remove(name);
					}
					// merge
					//int index = 0;
					string objectType = entity.ObjectType;
					switch (objectType)
					{
						case "Default":
							//process = (IProcess)Instinct.Core.Create("Neurox.Process.NetworkProcess, Neurox.Space", this, processRegistration, index);
							obj = null; // (IProcess)new NetworkProcess(this, processEntity, index);
							break;
						default:
							obj = null; // (IProcess)Instinct.Core.Create(processType, this, processRegistration, index);
							break;
					}
					set.Add(name, obj);
				}
			else
				set.Clear();
			return registration;
		}
	}
}
