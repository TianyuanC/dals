namespace DALs.Http.Validation
{
    using DALs.Model.Enums;
    using System;

    public static class HttpRequestValidation
    {
        public static void ValidateGet(this HttpRequest request)
        {
            if (request != HttpRequest.Get)
            {
                throw new ArgumentException("RequestMethod");
            }
        }

        public static void ValidateSet(this HttpRequest request)
        {
            if (request != HttpRequest.Post && request != HttpRequest.Put)
            {
                throw new ArgumentException("RequestMethod");
            }
        }
    }
}
