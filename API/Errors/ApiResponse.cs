namespace API.Errors
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode , string? message = null ) 
        {
            StatusCode = statusCode;
            Message = message ?? DefaultMessageFromStatusCode(statusCode);
        }

        private string? DefaultMessageFromStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "you have made a bad request",
                401 => "you are not authorizee",
                404 => "Resources not found",
                500 => "internal server error",
                _ => "an error has occures"
            };
        }
    }
}
