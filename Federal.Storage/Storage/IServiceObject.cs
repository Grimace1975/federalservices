namespace Federal.Storage
{
	/// <summary>
	/// IServiceObject
	/// </summary>
	public interface IServiceObject
	{
		string Name { get; }
		string RowVersion { get; set; }
	}
}
