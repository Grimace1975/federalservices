using System;
using System.ServiceModel;
using Example.Storage;
using Federal.Storage;
using Example.Storage.Entity;
namespace Example
{
	class Program
	{
		static void Main(string[] args)
		{
			RegisterKnownTypes();
			Console.WriteLine("Storage-Service");
			// Wcf Service
			using (var queryHost = new ServiceHost(typeof(StorageServiceQuery)))
			using (var host = new ServiceHost(typeof(StorageService)))
			{
				queryHost.Open();
				host.Open();
				//// Windows Service
				//var servicesToRun = new[] { new Service() };
				//ServiceBase.Run(servicesToRun);
				// pause
				Console.ReadKey();
			}
		}

		static void RegisterKnownTypes()
		{
			var knownServiceEntityTypes = new Type[2];
			knownServiceEntityTypes[0] = typeof(ProcessEntity);
			knownServiceEntityTypes[1] = typeof(SensorEntity);
			StorageServiceBase.KnownServiceEntityTypes = knownServiceEntityTypes;
		}
	}
}
