
namespace Investigation.Business.Services.Abstract
{
    public interface IMailService
    {
        Task SendEmail(string to, string subject, string body);
    }
}
