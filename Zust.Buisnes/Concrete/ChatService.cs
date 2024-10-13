//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Linq.Expressions;
//using System.Text;
//using System.Threading.Tasks;
//using Zust.Business.Abstract;
//using Zust.DataAccess.Abstract;
//using Zust.Entity.Entities;

//namespace Zust.Business.Concrete
//{
//    public class ChatService:IChatService
//    {
//        private readonly IChatDal _chatDal;

//        public ChatService(IChatDal chatDal)
//        {
//            _chatDal = chatDal;
//        }

//        public async Task AddAsync(Chat entity)
//        {
//            await _chatDal.AddAsync(entity);
//        }

//        public async Task DeleteAsync(Chat entity)
//        {
//            await _chatDal.DeleteAsync(entity);
//        }

//        public async Task<Chat> GetAsync(Expression<Func<Chat, bool>> filter)
//        {
//            return await _chatDal.GetAsync(filter);
//        }

//        public async Task<List<Chat>> GetListAsync()
//        {
//            return await _chatDal.GetListAsync();
//        }

//        public async Task UpdateAsync(Chat entity)
//        {
//            await _chatDal.UpdateAsync(entity);
//        }

//        public async Task<Chat> GetChat(string senderId, string recieverId)
//        {
//            return await _chatDal.GetChat(senderId, recieverId);

//        }

//        public List<Chat> GetChatsWithReciever(string id)
//        {
//            return _chatDal.GetChatsWithReciever(id);
//        }
//    }
//}
