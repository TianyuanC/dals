using System;

namespace ClientRepository.Model
{
    /// <summary>
    /// Class Ad.
    /// </summary>
    public class Ad
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public int AdID { get; set; }

        /// <summary>
        /// Gets or sets the last modification date.
        /// </summary>
        /// <value>The last modification date.</value>
        public DateTime LastModificationDate { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the test counter.
        /// </summary>
        /// <value>The test counter.</value>
        public long TestCounter { get; set; }
    }
}
