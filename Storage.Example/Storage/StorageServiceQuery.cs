using System.ServiceModel;
namespace Example.Storage
{
	/// <summary>
	/// IStorageServiceQuery
	/// </summary>
	[ServiceContract]
	public interface IStorageServiceQuery
	{
		[OperationContract]
		string DoWork();
	}

    /// <summary>
	/// StorageServiceQuery
    /// </summary>
	public class StorageServiceQuery : IStorageServiceQuery
    {
        public string DoWork()
        {
            return "Tyrone";
        }
    }
}
