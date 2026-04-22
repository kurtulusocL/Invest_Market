using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Investigation.Shared.Helpers
{
    public static class ViewCountHelper
    {
        public static HtmlString FormatViewCount(this IHtmlHelper _, long? count)
        {
            if (!count.HasValue)
                return new HtmlString("0");

            long value = count.Value;

            if (value < 0)
                return new HtmlString(value.ToString("N0")); 

            if (value < 10_000)
                return new HtmlString(value.ToString("N0"));

            if (value < 1_000_000)
            {
                double thousands = value / 1000.0;
                string formatted = Math.Abs(thousands % 1) < 0.01d
                    ? $"{thousands:F0}K"
                    : $"{thousands:F1}K";

                return new HtmlString(formatted.Replace('.', ','));
            }

            if (value < 1_000_000_000)
            {
                double millions = value / 1_000_000.0;
                string formatted = Math.Abs(millions % 1) < 0.01d
                    ? $"{millions:F0}M"
                    : $"{millions:F1}M";

                return new HtmlString(formatted.Replace('.', ','));
            }

            double billions = value / 1_000_000_000.0;
            string formattedB = Math.Abs(billions % 1) < 0.01d
                ? $"{billions:F0}B"
                : $"{billions:F1}B";

            return new HtmlString(formattedB.Replace('.', ','));
        }

        public static HtmlString FormatViewCount(this IHtmlHelper html, int count)
            => FormatViewCount(html, (long?)count);
    }
}