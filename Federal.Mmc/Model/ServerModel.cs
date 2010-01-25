using Microsoft.ManagementConsole;

namespace Federal.Model
{
    /// <summary>
    /// Server Model
    /// </summary>
    public class ServerModel : ModelBase
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        public ServerModel()
        {
            DisplayName = "Federation";
            Status = ServerStatus.Unknown;
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>The display name.</value>
        public string DisplayName { get; protected set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>The status.</value>
        public ServerStatus Status { get; protected set; }
    }
}
