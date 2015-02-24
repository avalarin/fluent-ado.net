using System;
using System.Configuration;
using System.Globalization;

namespace Avalarin.Data.Utils {

    /// <summary>
    /// The <see cref="ConnectionStringUtility"/> class provides a set of utility methods that can be used to work with configuration files.
    /// </summary>
    public static class ConnectionStringUtility {
        private const string DefaultConnectionStringName = "Default";

        /// <summary>
        /// Gets the default connection string.
        /// </summary>
        /// <returns>Default connection string.</returns>
        public static string GetConnectionString() {
            return GetConnectionString(null);
        }

        /// <summary>
        /// Gets the connection string associated with specified name.
        /// </summary>
        /// <param name="connectionStringName">The name of connection string to get.</param>
        /// <returns>Connection string associated with the specified name.</returns>
        public static string GetConnectionString(String connectionStringName) {
            return GetConnectionStringSettings(connectionStringName).ConnectionString;
        }

        /// <summary>
        /// Gets the default <see cref="ConnectionStringSettings"/>.
        /// </summary>
        /// <returns>Default <see cref="ConnectionStringSettings"/>.</returns>
        /// <exception cref="InvalidOperationException">Connection strings section is invalid or not found.</exception>
        /// <exception cref="InvalidOperationException">Default connection string not found.</exception>
        public static ConnectionStringSettings GetConnectionStringSettings() {
            return GetConnectionStringSettings(null);
        }

        /// <summary>
        /// Gets the <see cref="ConnectionStringSettings"/> string associated with specified name.
        /// </summary>
        /// <param name="connectionStringName">The name of connection string to get.</param>
        /// <returns><see cref="ConnectionStringSettings"/> associated with the specified name.</returns>
        /// <exception cref="InvalidOperationException">Connection strings section is invalid or not found.</exception>
        /// <exception cref="InvalidOperationException">Connection string not found.</exception>
        public static ConnectionStringSettings GetConnectionStringSettings(String connectionStringName) {
            if (connectionStringName == null) {
                connectionStringName = DefaultConnectionStringName;
            }
            var section = ConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;
            if (section == null) {
                throw new InvalidOperationException(
                    "ConnectionStringsSection is invalid or not found.");
            }
            var settings = section.ConnectionStrings[connectionStringName];
            if (settings == null) {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "ConnectionString '{0}' not found.",
                        connectionStringName)
                );
            }
            return settings;
        }
    }
}