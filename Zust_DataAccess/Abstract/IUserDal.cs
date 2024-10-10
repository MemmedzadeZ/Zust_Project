using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Core.DataAccess;
using Zust.Entity.Entities;

namespace Zust.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<CustomUser>
    {
        Task<CustomUser> GetByUsernameOrEmail(string username);
        Task? Update(CustomUser customUser);
        Task? Remove(CustomUser customUser);
        Task<List<CustomUser>> GetAll(string userId);
        Task<List<CustomUser>>? GetAllUserByIsOnline(string id);
    }
}
