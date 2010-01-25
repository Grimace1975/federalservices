using System.Runtime.Serialization;
namespace Example.Storage.Entity
{
	/// <summary>
	/// GridRegistration
	/// </summary>
	[DataContract(Namespace = "http://neurox.local")]
    public class GridRegistration
    {
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string LocalState { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public string RowVersion { get; set; }
		[DataMember]
		public string GridXml { get; set; }
    }
}
