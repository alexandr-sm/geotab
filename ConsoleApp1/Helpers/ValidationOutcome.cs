namespace JokeGenerator.Helpers
{
    public class ValidationOutcome
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public override bool Equals(object obj)
        {
            if(obj is ValidationOutcome validationOutcome)
            {
                return Code == validationOutcome.Code;
            }
            return false;
        }

        public static bool operator ==(ValidationOutcome leftValidationOutcome, ValidationOutcome rightValidationOutcome)
        {
            return leftValidationOutcome.Equals(rightValidationOutcome);
        }

        public static bool operator !=(ValidationOutcome leftValidationOutcome, ValidationOutcome rightValidationOutcome)
        {
            return !leftValidationOutcome.Equals(rightValidationOutcome);
        }

        public static ValidationOutcome ValidationSuccess => new() { Code = 0, Description = "Ok." };
        public static ValidationOutcome ValidationInvalidInputGeneric => new() { Code = 1, Description = "Invalid input, try again." };
        public static ValidationOutcome ValidationOutOfRange => new() { Code = 2, Description = "Provied value is out of range." };
    }
}
