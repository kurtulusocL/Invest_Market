
namespace Investigation.Business.Constants.Helpers
{
    public static class ValidateVisibilitySettingsHelper
    {
        public static bool ValidateVisibilitySettings(bool isVisibleForCompanies, bool isVisibleForInvestors, bool isVisibleForAll, bool isVisibleForNone)
        {
            if (isVisibleForAll && (isVisibleForCompanies || isVisibleForInvestors || isVisibleForNone))
            {
                return false;
            }

            if (isVisibleForNone && (isVisibleForCompanies || isVisibleForInvestors || isVisibleForAll))
            {
                return false;
            }

            if (!isVisibleForCompanies && !isVisibleForInvestors && !isVisibleForAll && !isVisibleForNone)
            {
                return false;
            }

            return true;
        }
    }
}
