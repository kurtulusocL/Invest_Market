using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Investigation.Business.Constants.Middlewares
{
    public class AutoLanguageRedirectMiddleware
    {
        private readonly RequestDelegate _next;

        private static readonly HashSet<string> SupportedCultures = new(StringComparer.OrdinalIgnoreCase)
        {
            "tr", "es", "en"
        };

        private static readonly Dictionary<string, string> BrowserToAppCulture = new(StringComparer.OrdinalIgnoreCase)
        {
            { "tr", "tr" }, { "tr-TR", "tr" }, { "tr-CY", "tr" },

            { "es", "es" }, { "es-ES", "es" }, { "es-MX", "es" }, { "es-AR", "es" },
            { "es-CO", "es" }, { "es-CL", "es" }, { "es-PE", "es" }, { "es-VE", "es" },
            { "es-GT", "es" }, { "es-CR", "es" }, { "es-PA", "es" }, { "es-DO", "es" },
            { "es-UY", "es" }, { "es-EC", "es" }, { "es-PY", "es" }, { "es-BO", "es" },
            { "es-NI", "es" }, { "es-HN", "es" }, { "es-SV", "es" }, { "es-PR", "es" },

            { "en", "en" }, { "en-US", "en" }, { "en-GB", "en" }, { "en-CA", "en" },
            { "en-AU", "en" }, { "en-NZ", "en" }, { "en-IE", "en" }, { "en-ZA", "en" }
        };

        public AutoLanguageRedirectMiddleware(RequestDelegate next, ILogger<AutoLanguageRedirectMiddleware> logger)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value?.TrimEnd('/') ?? "";

            if (path.Length >= 3 && path[0] == '/' && SupportedCultures.Contains(path.Substring(1, 2), StringComparer.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (path != "" && path != "/" && !path.Equals("/home", StringComparison.OrdinalIgnoreCase))
            {
                await _next(context);
                return;
            }

            if (context.Request.Cookies.TryGetValue("UserLanguage", out var savedLang) &&
                SupportedCultures.Contains(savedLang))
            {
                RedirectToCulture(context, savedLang);
                return;
            }

            var acceptLanguage = context.Request.Headers["Accept-Language"].ToString();
            var detectedCulture = DetectCultureFromHeader(acceptLanguage);

            context.Response.Cookies.Append("UserLanguage", detectedCulture, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                Path = "/",
                Secure = context.Request.IsHttps,
                HttpOnly = false,
                SameSite = SameSiteMode.Lax
            });
            RedirectToCulture(context, detectedCulture);
        }

        private static string DetectCultureFromHeader(string? header)
        {
            if (string.IsNullOrWhiteSpace(header))
                return "en";

            var languages = header
                .Split(',').Select(x => x.Trim().Split(';')[0].Trim()).Where(x => !string.IsNullOrEmpty(x));

            foreach (var lang in languages)
            {
                if (BrowserToAppCulture.TryGetValue(lang, out var culture))
                    return culture;

                var shortLang = lang.Length >= 2 ? lang.Substring(0, 2) : lang;
                if (BrowserToAppCulture.TryGetValue(shortLang, out culture))
                    return culture;
            }
            return "en";
        }

        private static void RedirectToCulture(HttpContext context, string culture)
        {
            var originalUri = context.Request.Path + context.Request.QueryString;
            var newUrl = $"/{culture}{originalUri}";
            if (!context.Request.Path.StartsWithSegments($"/{culture}"))
            {
                context.Response.Redirect(newUrl, permanent: false);
            }
            else
            {
                context.Request.Path = originalUri;
            }
        }
    }
}
