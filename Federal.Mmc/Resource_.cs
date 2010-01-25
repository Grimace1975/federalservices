using Microsoft.ManagementConsole;
using System;
using System.Drawing;

namespace Federal
{
    /// <summary>
    /// Image Index
    /// </summary>
    public enum ImageIndex
    {
        /// <summary>
        /// Undefined
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Connection
        /// </summary>
        Connection,
        /// <summary>
        /// Connection Bound
        /// </summary>
        ConnectionBound,
        /// <summary>
        /// Connection Paused
        /// </summary>
        ConnectionPaused,
        /// <summary>
        /// Connection Started
        /// </summary>
        ConnectionStarted,
        /// <summary>
        /// Connection Stopped
        /// </summary>
        ConnectionStopped,
        /// <summary>
        /// Connection Unknown
        /// </summary>
        ConnectionUnknown,
        /// <summary>
        /// Database
        /// </summary>
        Database,
        /// <summary>
        /// Database Emergency Mode
        /// </summary>
        DatabaseEmergencyMode,
        /// <summary>
        /// Database In Recovery
        /// </summary>
        DatabaseInRecovery,
        /// <summary>
        /// Database Offline
        /// </summary>
        DatabaseOffline,
        /// <summary>
        /// Database Read Only
        /// </summary>
        DatabaseReadOnly,
        /// <summary>
        /// Database Restoring
        /// </summary>
        DatabaseRestoring,
        /// <summary>
        /// Database Single User
        /// </summary>
        DatabaseSingleUser,
        /// <summary>
        /// Database Suspect
        /// </summary>
        DatabaseSuspect,
        /// <summary>
        /// Security Login
        /// </summary>
        Login,
        /// <summary>
        /// Security Login Disabled
        /// </summary>
        LoginDisabled,
        /// <summary>
        /// Network
        /// </summary>
        Network,
        /// <summary>
        /// Network Down
        /// </summary>
        NetworkDown,
        /// <summary>
        /// Network Hidden
        /// </summary>
        NetworkHidden,
        /// <summary>
        /// Network Hidden + Down
        /// </summary>
        NetworkHiddenDown,
        /// <summary>
        /// Neurox Paused
        /// </summary>
        NeuroxPaused,
        /// <summary>
        /// Neurox Started
        /// </summary>
        NeuroxStarted,
        /// <summary>
        /// Neurox Stopped
        /// </summary>
        NeuroxStopped,
        /// <summary>
        /// Neurox Unknown
        /// </summary>
        NeuroxUnknown,
        /// <summary>
        /// Security NT Group
        /// </summary>
        NtGroup,
        /// <summary>
        /// Process Paused
        /// </summary>
        ProcessPaused,
        /// <summary>
        /// Process Started
        /// </summary>
        ProcessStarted,
        /// <summary>
        /// Process Stopped
        /// </summary>
        ProcessStopped,
        /// <summary>
        /// 
        /// </summary>
        ProcessUnknown,
        /// <summary>
        /// Role
        /// </summary>
        Role,
        /// <summary>
        /// Sensor
        /// </summary>
        Sensor,
        /// <summary>
        /// Sensor Running
        /// </summary>
        SensorRunning,
        /// <summary>
        /// Sensor Stopped
        /// </summary>
        SensorStopped,
        /// <summary>
        /// Statistic
        /// </summary>
        Statistic,
    }

    /// <summary>
    /// Resource Loader
    /// </summary>
    public static class Resource_
    {
        private static string[] Images = new string[] {
            "Resource_.Connection.ico",
            "Resource_.ConnectionBound.ico",
            "Resource_.ConnectionPaused.ico",
            "Resource_.ConnectionStarted.ico",
            "Resource_.ConnectionStopped.ico",
            "Resource_.ConnectionUnknown.ico",
            "Resource_.Database.ico",
            "Resource_.DatabaseEmergencyMode.ico",
            "Resource_.DatabaseInRecovery.ico",
            "Resource_.DatabaseOffline.ico",
            "Resource_.DatabaseReadOnly.ico",
            "Resource_.DatabaseRestoring.ico",
            "Resource_.DatabaseSingleUser.ico",
            "Resource_.DatabaseSuspect.ico",
            "Resource_.Login.ico",
            "Resource_.LoginDisabled.ico",
            "Resource_.Network.ico",
            "Resource_.NetworkDown.ico",
            "Resource_.NetworkHidden.ico",
            "Resource_.NetworkHiddenDown.ico",
            "Resource_.NeuroxPaused.ico",
            "Resource_.NeuroxStarted.ico",
            "Resource_.NeuroxStopped.ico",
            "Resource_.NeuroxUnknown.ico",
            "Resource_.NtGroup.ico",
            "Resource_.ProcessPaused.ico",
            "Resource_.ProcessStarted.ico",
            "Resource_.ProcessStopped.ico",
            "Resource_.ProcessUnknown.ico",
            "Resource_.Role.ico",
            "Resource_.Sensor.ico",
            "Resource_.SensorRunning.ico",
            "Resource_.SensorStopped.ico",
            "Resource_.Statistic.ico",
        };

        /// <summary>
        /// Defines the index of the image.
        /// </summary>
        /// <param name="snapIn">The snap in.</param>
        public static void DefineImageIndex(SnapIn snapIn)
        {
            Type type = typeof(SnapIn);
            var smallImage = snapIn.SmallImages;
            smallImage.Add(new Icon(type, "Resource_.Undefined.ico"));
            foreach (string image in Images)
            {
				smallImage.Add(new Icon(type, image));
            }
        }
    }
}