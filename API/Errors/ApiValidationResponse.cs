namespace API.Errors
{
    public class ApiValidationResponse : ApiResponse
    {
        public IEnumerable<string> Errors  { get; set; }
        public ApiValidationResponse(int statusCode, IEnumerable<string> errors, string? message = null) : base(StatusCodes.Status400BadRequest)
        {

            Errors = errors;
        }
    }
}
