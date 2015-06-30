namespace DALs.Http.Validation
{
    using DALs.Model.Enums;
    using System;

    /// <summary>
    /// Class HttpRequestValidation.
    /// </summary>
    public static class HttpRequestValidation
    {
        /// <summary>
        /// Validates the get.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentException">RequestMethod</exception>
        public static void ValidateGet(this HttpRequest request)
        {
            if (request != HttpRequest.Get)
            {
                throw new ArgumentException("RequestMethod");
            }
        }

        /// <summary>
        /// Validates the set.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <exception cref="System.ArgumentException">RequestMethod</exception>
        public static void ValidateSet(this HttpRequest request)
        {
            if (request != HttpRequest.Post && request != HttpRequest.Put)
            {
                throw new ArgumentException("RequestMethod");
            }
        }
    }
}
