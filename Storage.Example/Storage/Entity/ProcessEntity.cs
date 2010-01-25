using Federal.Storage;
using System;
using System.Runtime.Serialization;
namespace Example.Storage.Entity
{
	/// <summary>
	/// ProcessEntity
	/// </summary>
	[DataContract(Namespace = "http://neurox.local")]
	public class ProcessEntity : ServiceEntityBase
    {
		[DataMember]
		public int[] GridIds { get; set; }
    }
}
