using System.Runtime.Serialization;
namespace Federal.Storage
{
	/// <summary>
	/// ServiceRegistration
	/// </summary>
	[DataContract(Namespace = "http://neurox.local")]
	public class ServiceRegistration
    {
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string LocalState { get; set; }
		[DataMember]
		public string RowVersion { get; set; }
    }
}
