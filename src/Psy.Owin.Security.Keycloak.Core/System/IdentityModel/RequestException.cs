namespace System.IdentityModel
{
    public abstract class RequestException : Exception
    {
        public RequestException() : base()
        {

        }
        public RequestException(string message) : base(message)
        {

        }
        public RequestException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}