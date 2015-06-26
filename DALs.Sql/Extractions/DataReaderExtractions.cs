namespace DALs.Sql.Extractions
{
    using System.Data;

    /// <summary>
    /// Class DataReaderExtractions.
    /// </summary>
    public static class DataReaderExtractions
    {
        /// <summary>
        /// Gets the specified reader.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader">The reader.</param>
        /// <param name="index">The index.</param>
        /// <param name="defaultVal">The default value.</param>
        /// <returns>T.</returns>
        public static T Get<T>(this IDataReader reader, int index, T defaultVal = default(T))
        {
            return reader.IsDBNull(index) ? defaultVal : (T)reader[index];
        }
    }
}
