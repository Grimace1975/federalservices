namespace Federal.Model.Database
{
    /// <summary>
    /// Status of database
    /// </summary>
    public enum DatabaseStatus
    {
        /// <summary>
        /// Default
        /// </summary>
        Default,
        /// <summary>
        /// Emergency Mode
        /// </summary>
        EmergencyMode,
        /// <summary>
        /// In Recovery
        /// </summary>
        InRecovery,
        /// <summary>
        /// Offline
        /// </summary>
        Offline,
        /// <summary>
        /// ReadOnly
        /// </summary>
        ReadOnly,
        /// <summary>
        /// Restoring
        /// </summary>
        Restoring,
        /// <summary>
        /// SingleUser
        /// </summary>
        SingleUser,
        /// <summary>
        /// Suspect
        /// </summary>
        Suspect,
    }
}
