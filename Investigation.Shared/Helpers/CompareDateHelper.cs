
namespace Investigation.Shared.Helpers
{
    public static class CompareDateHelper
    {
        public static bool IsDateOld(DateTime dateFromDatabase)
        {
            return dateFromDatabase < DateTime.Now;
        }
    }
}
