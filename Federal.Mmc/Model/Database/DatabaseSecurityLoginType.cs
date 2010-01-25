namespace Federal.Model.Database.Security
{
    /// <summary>
    /// Type of login
    /// </summary>
    public enum DatabaseSecurityLoginType
    {
        /// <summary>
        /// AsymmetricKey
        /// </summary>
        AsymmetricKey,
        /// <summary>
        /// Certificate
        /// </summary>
        Certificate,
        /// <summary>
        /// Group
        /// </summary>
        Group,
        /// <summary>
        /// SqlServer
        /// </summary>
        SqlServer,
        /// <summary>
        /// User
        /// </summary>
        User,
    }
}
