using System.Net;

namespace Business.Commons
{
    public class ResponseException : Exception
    {
        public int StatusCode { get; set; }
        public bool Succeeded { get; set; } = false;
        public new string Message { get; set; }
        public object Errors { get; }
        public new object? Data { get; set; }
        public ResponseException(int statusCode, string message, object errors)
        {
            StatusCode = statusCode;
            Message = message;
            Errors = errors;
        }

        public ResponseException(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ResponseException(string message)
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Message = message;
        }

       
    }
}
