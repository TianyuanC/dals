namespace DALs.Http.Validation
{
    using System;
    using DALs.Model.Configs;

    public static class HttpConfigValidation
    {
        public static void ValidateBase(this HttpConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config.Route))
            {
                throw new ArgumentException("Route");
            }
        }

        public static void ValidateGet(this HttpConfiguration config)
        {
            config.ValidateBase();
            config.RequestMethod.ValidateGet();
        }

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
