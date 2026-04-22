using Ganss.Xss;
using Investigation.Business.Constants.Services;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class SendMessageManager : ISendMessageService
    {
        readonly ISendMessageRepository _sendMessageRepository;
        private readonly IHtmlSanitizer _htmlSanitizer;
        readonly EncryptionService _encryptionService;
        public SendMessageManager(ISendMessageRepository sendMessageRepository, IHtmlSanitizer htmlSanitizer, EncryptionService encryptionService)
        {
            _sendMessageRepository = sendMessageRepository;
            _htmlSanitizer = htmlSanitizer;
            _encryptionService = encryptionService;
        }

        public async Task<bool> CreateAsync(string nameSurname, string email, string phoneNumber, string messageTitle, string messageSubject, string messageContent)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(_htmlSanitizer, nameof(_htmlSanitizer));
                string safeMessageContent = _htmlSanitizer.Sanitize(messageContent ?? string.Empty);
                var entity = new SendMessage
                {
                    NameSurname = _encryptionService.Encrypt(nameSurname),
                    Email = _encryptionService.Encrypt(email),
                    PhoneNumber = _encryptionService.Encrypt(phoneNumber),
                    MessageTitle = _encryptionService.Encrypt(messageTitle),
                    MessageSubject = _encryptionService.Encrypt(messageSubject),
                    MessageContent = _encryptionService.Encrypt(safeMessageContent)
                };
                if (entity != null)
                {
                    var result = await _sendMessageRepository.AddAsync(entity);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while adding the entity.", ex);
            }
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _sendMessageRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(SendMessage entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _sendMessageRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _sendMessageRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public IQueryable<SendMessage> GetAllAsync()
        {
            try
            {
                var data = _sendMessageRepository.GetAll(i => i.IsActive == true && i.IsDeleted == false);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SendMessage>().AsQueryable();
            }
        }

        public IQueryable<SendMessage> GetAllForAdminAsync()
        {
            try
            {
                var data = _sendMessageRepository.GetAll();
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<SendMessage>().AsQueryable();
            }
        }

        public async Task<IEnumerable<SendMessage>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _sendMessageRepository.GetAllAsync();
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<SendMessage>();
            }
        }

        public async Task<SendMessage> GetByIdAsync(int? id)
        {
            try
            {
                return await _sendMessageRepository.GetAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _sendMessageRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _sendMessageRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _sendMessageRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _sendMessageRepository.SetNotDeletedAsync(id);
            return result;
        }
    }
}
