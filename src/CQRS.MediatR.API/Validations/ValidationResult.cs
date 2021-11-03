namespace CQRS.MediatR.API.Validations
{
    public record ValidationResult
    {
        public bool IsSuccessful { get; private init; } = true;
        public string Error { get; private init; }

        public static ValidationResult Success => new();
        public static ValidationResult Fail(string error) => new() { IsSuccessful = false, Error = error };
    }
}
