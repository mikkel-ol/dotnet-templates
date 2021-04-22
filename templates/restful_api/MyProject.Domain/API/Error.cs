namespace MyProject.Domain.API
{
    /// <summary>
    ///     Model of Error message.
    /// </summary>
    public class Error
    {
        /// <summary>
        ///     Status code of the error
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     The type of the error, often the exception name.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     The message describing the error.
        /// </summary>
        public string Message { get; set; }
    }
}
