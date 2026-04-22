
namespace Investigation.Business.Services.Abstract
{
    public interface ICaptchaService
    {
        string SiteKey { get; }
        Task<bool> VerifyAsync(string token, string? remoteIp = null);
    }
}
