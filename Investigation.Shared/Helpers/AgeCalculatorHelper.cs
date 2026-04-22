
namespace Investigation.Shared.Helpers
{
    public static class AgeCalculatorHelper
    {
        public static int CalculateAge(DateTime birthdate)
        {
            var today = DateTime.UtcNow;
            int age = today.Year - birthdate.Year;
            if (birthdate > today.AddYears(-age)) age--;
            return age;
        }
    }
}
