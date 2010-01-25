using System;
using System.Timers;
using System.ServiceModel;
namespace Federal.Storage
{
	public interface IKeepAliveActor<TService, TStorageClient>
		where TStorageClient : IStorageClient, ICommunicationObject, new()
	{
		void Initialize(RegisteredService<TService, TStorageClient> parent);
	}

	public class KeepAliveActor<TService, TStorageClient> : IKeepAliveActor<TService, TStorageClient>
		where TStorageClient : IStorageClient, ICommunicationObject, new()
	{
		private RegisteredService<TService, TStorageClient> _parent;
		private int _clockCycleSinceKeepAlive = 0;

		public void Initialize(RegisteredService<TService, TStorageClient> parent)
		{
			_parent = parent;
			_parent.Clock += OnClock;
		}

		protected void OnClock(object sender, ElapsedEventArgs e)
		{
			_clockCycleSinceKeepAlive++;
			if (_clockCycleSinceKeepAlive == 10)
			{
				TStorageClient storageClient = new TStorageClient();
				try
				{
					storageClient.SetServiceNodeState(_parent.NodeType, _parent.Registration.Id, ServiceNodeState.KeepAlive, null);
				}
				finally
				{
					try { storageClient.Close(); }
					catch (Exception) { storageClient.Abort(); }
				}
				// clear cycle till we are done, additional clocks will be eaten
				_clockCycleSinceKeepAlive = 0;
			}
		}
	}
}
