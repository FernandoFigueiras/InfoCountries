namespace InfoCountries.Models
{
    /// <summary>
    /// Class used to verify if Services were Successful
    /// </summary>
    public class Response
    {
        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }

        public int Woeidid { get; set; }

    }
}
