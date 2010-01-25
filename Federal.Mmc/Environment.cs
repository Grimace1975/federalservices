using System.Diagnostics;
namespace Federal
{
    /// <summary>
    /// Environment class
    /// </summary>
    public class Environment
    {
        /// <summary>
        /// Startups this instance.
        /// </summary>
        public static void Startup()
        {
			//Debugger.Launch();

            //System.AppDomain.CurrentDomain.SetupInformation.ConfigurationFile = @"C:\_APPLICATION2\NEUROX\MMC3\Neurox.Mmc\app.config";
            //System.IO.Path.GetDirectoryName(typeof(Program).Assembly.Location)
        }
    }
}