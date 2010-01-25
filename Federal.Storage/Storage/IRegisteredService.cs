using System;
using System.Timers;
namespace Federal.Storage
{
    /// <summary>
    /// IRegisteredService
    /// </summary>
    public interface IRegisteredService
    {
		event ElapsedEventHandler Clock;
        event EventHandler Start;
        event EventHandler Stop;
    }
}
