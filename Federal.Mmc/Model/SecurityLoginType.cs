namespace Federal.Model
{
    /// <summary>
    /// Type of login
    /// </summary>
    public enum SecurityLoginType
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
