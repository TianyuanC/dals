namespace DALs.Http.Validation
{
    using DALs.Model.Configs;
    using System;

    /// <summary>
    /// Class HttpConfigValidation.
    /// </summary>
    public static class HttpConfigValidation
    {
        /// <summary>
        /// Validates the base.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.ArgumentException">Route</exception>
        public static void ValidateBase(this HttpConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config.Route))
            {
                throw new ArgumentException("Route");
            }
        }

        /// <summary>
        /// Validates the get.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public static void ValidateGet(this HttpConfiguration config)
        {
            config.ValidateBase();
            config.RequestMethod.ValidateGet();
        }

        /// <summary>
        /// Validates the set.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <exception cref="System.ArgumentException">Data</exception>
        public static void ValidateSet(this HttpConfiguration config)
        {
            config.ValidateBase();
            config.RequestMethod.ValidateSet();

            if (null == config.Data)
            {
                throw new ArgumentException("Data");
            }
        }
    }
}
