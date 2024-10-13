//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Zust.DataAccess.Abstract;
//using Zust.Entity.Data;
//using Zust.Entity.Entities;

//namespace Zust.DataAccess.Concrete
//{
//    public class ChatDal :IChatDal
//    {
//        private readonly ZustDbContext _context;
//        public ChatDal(ZustDbContext context):base()
//        {
//            _context = context;
//        }

//        public async Task<Chat> GetChat(string senderId, string recieverId)
//        {
//            return await _context.Chats.Include(nameof(Chat.Messages)).FirstOrDefaultAsync(c => c.SenderId == senderId && c.RecieverId == recieverId ||
//                         c.SenderId == recieverId && c.RecieverId == senderId);
//        }

//        public List<Chat> GetChatsWithReciever(string id)
//        {
//            return _context.Chats.Include(nameof(Chat.Messages)).Include(nameof(Chat.Recevier)).Where(c => c.SenderId == id || c.RecieverId == id).ToList();
//        }
//    }
//}
