using System.Linq.Expressions;
using Investigation.Business.Services.Abstract;
using Investigation.DataAccess.Abstract;
using Investigation.Domain.Entities;

namespace Investigation.Business.Services.Concrete
{
    public class MessageManager : IMessageService
    {
        readonly IMessageRepository _messageRepository;
        public MessageManager(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<bool> DeleteAllByIdAsync(List<int> ids)
        {
            try
            {
                if (ids == null || !ids.Any())
                    throw new ArgumentNullException(nameof(ids), "id list was null or empty");

                var result = await _messageRepository.DeleteByIdsAsync(ids.Cast<object>());
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while bulk deleting entities.", ex);
            }
        }

        public async Task<bool> DeleteAsync(Message entity, int id)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "entity was null");

                var data = await _messageRepository.GetAsync(i => i.Id == id);
                if (data != null)
                {
                    var result = await _messageRepository.DeleteAsync(data);
                    return result;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while deleting the entity.", ex);
            }
        }

        public async Task<IEnumerable<Message>> GetAllForSignalRAsync()
        {
            try
            {
                var data = await _messageRepository.GetAllIncludeAsync(new Expression<Func<Message, bool>>[]
                {
                   
                }, null, y => y.Sender, y => y.Receiver);
                return data.OrderByDescending(i => i.CreatedDate).ToList();
            }
            catch (Exception)
            {
                return new List<Message>();
            }
        }

        public IQueryable<Message> GetAllIncludingAsync()
        {
            try
            {
                var data = _messageRepository.GetAllInclude(new Expression<Func<Message, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, null, y => y.Sender, y => y.Receiver);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Message>().AsQueryable();
            }
        }

        public IQueryable<Message> GetAllIncludingByReadAsync()
        {
            try
            {
                var data = _messageRepository.GetAllInclude(new Expression<Func<Message, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false,
                    i=>i.IsRead==true
                }, null, y => y.Sender, y => y.Receiver);
                return data.OrderByDescending(i => i.SentAt);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Message>().AsQueryable();
            }
        }

        public IQueryable<Message> GetAllIncludingByRecieverIdAsync(string recieverId)
        {
            try
            {
                if (recieverId == null)
                    throw new ArgumentNullException(nameof(recieverId), "recieverId was null");

                var data = _messageRepository.GetAllIncludeById(recieverId, "ReceiverId", new Expression<Func<Message, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sender, y => y.Receiver);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Message>().AsQueryable();
            }
        }

        public IQueryable<Message> GetAllIncludingBySenderIdAsync(string senderId)
        {
            try
            {
                if (senderId == null)
                    throw new ArgumentNullException(nameof(senderId), "senderId was null");

                var data = _messageRepository.GetAllIncludeById(senderId, "SenderId", new Expression<Func<Message, bool>>[]
                {
                    i=>i.IsActive==true,
                    i=>i.IsDeleted==false
                }, y => y.Sender, y => y.Receiver);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Message>().AsQueryable();
            }
        }

        public IQueryable<Message> GetAllIncludingForAdminAsync()
        {
            try
            {
                var data = _messageRepository.GetAllInclude(new Expression<Func<Message, bool>>[]
                {

                }, null, y => y.Sender, y => y.Receiver);
                return data.OrderByDescending(i => i.CreatedDate);
            }
            catch (Exception)
            {
                return Enumerable.Empty<Message>().AsQueryable();
            }
        }

        public async Task<Message> GetByIdAsync(int? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException(nameof(id), "id was null");

                return await _messageRepository.GetIncludeAsync(i => i.Id == id, y => y.Sender, y => y.Receiver);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while getting the entity.", ex);
            }
        }

        public async Task<bool> SetActiveAsync(int id)
        {
            var result = await _messageRepository.SetActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeActiveAsync(int id)
        {
            var result = await _messageRepository.SetDeActiveAsync(id);
            return result;
        }

        public async Task<bool> SetDeletedAsync(int id)
        {
            var result = await _messageRepository.SetDeletedAsync(id);
            return result;
        }

        public async Task<bool> SetNotDeletedAsync(int id)
        {
            var result = await _messageRepository.SetNotDeletedAsync(id);
            return result;
        }
    }
}
