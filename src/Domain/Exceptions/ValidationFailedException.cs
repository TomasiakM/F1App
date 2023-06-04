namespace Domain.Exceptions;
public sealed class ValidationFailedException : Exception
{
    public readonly Dictionary<string, string> ValidationErrors;

    public ValidationFailedException(Dictionary<string, string> validationErrors)
    {
        ValidationErrors = validationErrors;
    }
}
