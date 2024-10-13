
using Zust.Entity.Entities;

namespace Zust.Business.Abstract
{
    public interface IUserService
    {
        Task<CustomUser> GetByUsernameOrEmail(string usernameOrEmail);
        Task Add(CustomUser user);  
        Task? Update(CustomUser customUser);

        Task? Remove(CustomUser customUser);
        Task<List<CustomUser>> GetAll(string userId);
        Task<List<CustomUser>> GetListAsync();
        Task<List<CustomUser>>? GetAllUserByIsOnline(string id);


    }
}
