namespace ProjectRed.Application.Validators
{
    public class YearValidator
    {
        public static (bool IsValid, string Message) ValidateYear(int year)
        {
            int currentYear = DateTime.Now.Year;

            if (year < 1000 || year > 9999)
                return (false, "Year must be 4 digits");

            if ((currentYear - year) < 13)
                return (false, "You must be at least 13 years old");

            return (true, "Year is valid");
        }
    }
}
