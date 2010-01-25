using System;
using System.Timers;
using System.Reflection;
using System.ServiceModel;
namespace Federal.Storage
{
	public abstract class RegisteredService<TService, TStorageClient> : IDisposable
		where TStorageClient : IStorageClient, ICommunicationObject, new()
	{
		private static readonly TService s_service;
		private Timer _clockTimer = new Timer { Interval = 1000 };
		private bool _isDisposed = false;
		private IKeepAliveActor<TService, TStorageClient> _keepAliveActor;

		public static TService Instance
		{
			get { return s_service; }
		}

		static RegisteredService()
		{
			Name = Environment.MachineName;
			var singleton = typeof(TService);
			s_service = (TService)singleton.InvokeMember(singleton.Name, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, null, null);
		}

		protected RegisteredService(int nodeType)
			: this(nodeType, new KeepAliveActor<TService, TStorageClient>()) { }
		protected RegisteredService(int nodeType, IKeepAliveActor<TService, TStorageClient> keepAliveActor)
		{
			if (keepAliveActor == null)
				throw new ArgumentNullException("keepAliveActor");
			NodeType = nodeType;
			_keepAliveActor = keepAliveActor;
			_keepAliveActor.Initialize(this);
			_clockTimer.Elapsed += OnClock;
			// Inherited class must call OnStart() after registration when isInitialRegistration is true
			NodeState = ServiceNodeState.Down;
		}

		protected virtual void Dispose(bool disposing)
		{
			OnStop();
			if (disposing)
			{
				if (Registration == null)
					return;
				var storageClient = new TStorageClient();
				try
				{
					SetNodeState(storageClient, ServiceNodeState.Down);
				}
				finally
				{
					try { storageClient.Close(); }
					catch (Exception) { storageClient.Abort(); }
				}
			}
		}

		protected void SetNodeState(TStorageClient storageClient, ServiceNodeState nodeState)
		{
			storageClient.SetServiceNodeState(NodeType, Registration.Id, nodeState, null);
			NodeState = nodeState;
		}

		public ServiceNodeState NodeState { get; private set; }

		public void Dispose()
		{
			if (!_isDisposed)
			{
				_isDisposed = true;
				Dispose(true);
			}
		}

		public static string Name { get; protected set; }

		public event ElapsedEventHandler Clock;

		public int NodeType { get; protected set; }

		public event EventHandler Start;

		public event EventHandler Stop;

		public ServiceRegistration Registration { get; set; }

		public virtual void Open()
		{
			var storageClient = new TStorageClient();
			try
			{
				RegisterWithStorage(storageClient);
				// raise storage node-state
				if (NodeState != ServiceNodeState.Up)
				{
					SetNodeState(storageClient, ServiceNodeState.Up);
					OnStart();
				}
			}
			finally
			{
				try { storageClient.Close(); }
				catch (Exception) { storageClient.Abort(); }
			}
		}

		public abstract void RegisterWithStorage(TStorageClient storageClient);

		protected virtual void OnClock(object sender, ElapsedEventArgs e)
		{
			ElapsedEventHandler clock = Clock;
			if (clock != null)
				clock(this, e);
		}

		protected virtual void OnStart()
		{
			_clockTimer.Start();
			EventHandler start = Start;
			if (start != null)
				start(this, EventArgs.Empty);
		}

		protected internal virtual void OnStop()
		{
			_clockTimer.Stop();
			EventHandler stop = Stop;
			if (stop != null)
				stop(this, EventArgs.Empty);
		}
	}
}
