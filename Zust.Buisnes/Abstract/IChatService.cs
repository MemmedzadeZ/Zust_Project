using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zust.Entity.Entities;

namespace Zust.Business.Abstract
{
    public interface IChatService
    {
        Task<Chat> GetAsync(Expression<Func<Chat, bool>> filter);
        Task<List<Chat>> GetListAsync();
        Task DeleteAsync(Chat entity);
        Task AddAsync(Chat entity);
        Task UpdateAsync(Chat entity);
        public Task<Chat> GetChat(string senderId, string recieverId);
        public List<Chat> GetChatsWithReciever(string id);
    }
}
