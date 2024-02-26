namespace SampleCoreApi.Helpers
{
    public class ApiException : Exception
    {
        public int StatusCode { get; set; }

        public bool IsModelValidatonError { get; set; }

        public IEnumerable<ValidationError> Errors { get; set; }

        public string ReferenceErrorCode { get; set; }

        public string ReferenceDocumentLink { get; set; }

        public object CustomError { get; set; }

        public bool IsCustomErrorObject { get; set; }

        public ApiException(string message, int statusCode = 400, string errorCode = null, string refLink = null)
            : base(message)
        {
            StatusCode = statusCode;
            ReferenceErrorCode = errorCode;
            ReferenceDocumentLink = refLink;
        }

        public ApiException(object customError, int statusCode = 400)
        {
            IsCustomErrorObject = true;
            StatusCode = statusCode;
            CustomError = customError;
        }

        public ApiException(IEnumerable<ValidationError> errors, int statusCode = 400)
        {
            IsModelValidatonError = true;
            StatusCode = statusCode;
            Errors = errors;
        }

        public ApiException(Exception ex, int statusCode = 500)
            : base(ex.Message)
        {
            StatusCode = statusCode;
        }
    }

    public class ValidationError
    {
        public string Name { get; }

        public string Reason { get; }

        public ValidationError(string name, string reason)
        {
            Name = ((name != string.Empty) ? name : null);
            Reason = reason;
        }
    }
}
