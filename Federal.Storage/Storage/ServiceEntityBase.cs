using System.ServiceModel;
using System.Runtime.Serialization;
using System;
namespace Federal.Storage
{
	/// <summary>
	/// ServiceEntityBase
	/// </summary>
	[DataContract(Namespace = "http://neurox.local")]
	[KnownType("GetKnownType")]
	public class ServiceEntityBase
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public int Id { get; set; }
		[DataMember]
		public string LocalState { get; set; }
		[DataMember]
		public string RowVersion { get; set; }
		[DataMember]
		public string ObjectType { get; set; }

		private static Type[] GetKnownType()
		{
			return StorageServiceBase.KnownServiceEntityTypes;
		}
	}
}
