namespace DALs.Sql.Extractions
{
    using System;
    using System.Data;
    using System.Diagnostics;

    /// <summary>
    /// Class DataReaderExtractions.
    /// </summary>
    public static class DataReaderExtractions
    {

        /// <summary>
        /// Gets the reader value by column name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="defaultVal">The default value.</param>
        /// <returns>T.</returns>
        public static T Get<T>(this IDataReader reader, string columnName, T defaultVal = default(T))
        {
            int index;
            try
            {
                index = reader.GetOrdinal(columnName);
            }
            catch (IndexOutOfRangeException)
            {
                Trace.TraceError("Cannot find the column named: {0}", columnName);
                throw;
            }
            return reader.Get(index, defaultVal);
        }

        /// <summary>
        /// Gets reader value by index.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="index">The index.</param>
        /// <param name="defaultVal">The default value.</param>
        /// <returns>T.</returns>
        public static T Get<T>(this IDataReader reader, int index, T defaultVal = default(T))
        {
            if (reader.IsDBNull(index))
            {
                return defaultVal;
            }

            var result = reader[index];
            try
            {
                return (T)result;
            }
            catch (InvalidCastException)
            {
                Trace.TraceError("Cannot cast from {0} to {1}"
                    , result.GetType().ToString(), defaultVal.GetType().ToString());
                throw;
            }
        }
    }
}
