namespace System.IdentityModel
{
    public class BadRequestException : RequestException
    {
        public BadRequestException() : base()
        {

        }
        public BadRequestException(string message) : base(message)
        {

        }
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}