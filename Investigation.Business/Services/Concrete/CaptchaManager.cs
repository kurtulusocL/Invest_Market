using System.Text.Json;
using Investigation.Business.Constants.Utilities.CaptchaModel;
using Investigation.Business.Services.Abstract;
using Microsoft.Extensions.Configuration;

namespace Investigation.Business.Services.Concrete
{
    public class CaptchaManager : ICaptchaService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _siteKey;
        private readonly string _secretKey;

        public CaptchaManager(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;

            var section = _configuration.GetSection("ReCaptcha");
            _siteKey = section["SiteKey"] ?? throw new InvalidOperationException("ReCaptcha:SiteKey is missing.");
            _secretKey = section["SecretKey"] ?? throw new InvalidOperationException("ReCaptcha:SecretKey is missing.");
        }

        public string SiteKey => _siteKey;

        public async Task<bool> VerifyAsync(string token, string? remoteIp = null)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            var client = _httpClientFactory.CreateClient();

            var formData = new Dictionary<string, string>
            {
                { "secret", _secretKey },
                { "response", token }
            };

            if (!string.IsNullOrEmpty(remoteIp))
                formData.Add("remoteip", remoteIp);

            var content = new FormUrlEncodedContent(formData);

            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);
            if (!response.IsSuccessStatusCode)
                return false;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ReCaptchaResponse>(json);

            if (result == null || !result.Success)
            {
                return false;
            }
            return true;
        }
    }
}
