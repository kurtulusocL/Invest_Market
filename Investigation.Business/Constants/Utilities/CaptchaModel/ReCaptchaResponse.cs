using System.Text.Json.Serialization;

namespace Investigation.Business.Constants.Utilities.CaptchaModel
{
    public class ReCaptchaResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("error-codes")]
        public List<string> ErrorCodes
        {
            get; set;
        }
    }
}
