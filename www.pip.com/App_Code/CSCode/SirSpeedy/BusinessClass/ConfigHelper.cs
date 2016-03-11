using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace SirSpeedy.CMS
{
    /// <summary>
    /// This class retrieves configuration values.
    /// </summary>
    public class ConfigHelper
    {
        /// <summary>
        /// Get the config Value for the specified Label and return it as a String.
        /// </summary>
        /// <param name="configLabel">The Label for the Value to return.</param>
        /// <returns>The Value as a string, or String.Empty.</returns>
        public static string GetValueString(string configLabel)
        {
            string configValue = ConfigurationDAO.GetConfigKeyValue(configLabel);
            if (!string.IsNullOrEmpty(configValue))
                return configValue;
            else
                return string.Empty;
        }

        /// <summary>
        /// Get the config Value for the specified Label and return it as an int.
        /// </summary>
        /// <param name="configLabel">The Label for the Value to return.</param>
        /// <returns>The Value as an int, or 0.</returns>
        public static int GetValueInt(string configLabel)
        {
            int configValue = 0;
            int.TryParse(ConfigurationDAO.GetConfigKeyValue(configLabel), out configValue);
            return configValue;
        }

        /// <summary>
        /// Get the config Value for the specified Label and return it as a long.
        /// </summary>
        /// <param name="configLabel">The Label for the Value to return.</param>
        /// <returns>The Value as a long, or 0.</returns>
        public static long GetValueLong(string configLabel)
        {
            long configValue = 0;
            long.TryParse(ConfigurationDAO.GetConfigKeyValue(configLabel), out configValue);
            return configValue;
        }

        /// <summary>
        /// Get the config Value for the specified Label and return it as a bool.
        /// </summary>
        /// <param name="configLabel">The Label for the Value to return.</param>
        /// <returns>The Value as a bool, or false.</returns>
        public static bool GetValueBool(string configLabel)
        {
            bool configValue = false;
            bool.TryParse(ConfigurationDAO.GetConfigKeyValue(configLabel), out configValue);
            return configValue;
        }

        /// <summary>
        /// Get the config Value for the 301 configs.
        /// </summary>
        /// <param name="configLabel">The Label for the Value to return.</param>
        /// <returns>The Value as a string, or String.Empty.</returns>
        public static string Get301ValueString(string configLabel)
        {
            string configValue = ConfigurationDAO.GetConfigKeyValue(configLabel, false);
            if (!string.IsNullOrEmpty(configValue))
                return configValue;
            else
                return string.Empty;
        }
    }
}