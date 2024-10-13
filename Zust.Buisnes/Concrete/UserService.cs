using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
    using Zust.Entity.Entities;

namespace Zust.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal userDal;

        public UserService(IUserDal userDal)
        {
            this.userDal = userDal;
        }

        public async Task Add(CustomUser user)
        {
          await userDal.Add(user);   
        }

        public async Task<List<CustomUser>> GetAll(string userId)
        {
            return await userDal.GetAll(userId);  
        }

        public async Task<List<CustomUser>>? GetAllUserByIsOnline(string id)
        {
            return await userDal.GetAllUserByIsOnline(id);
        }

        public async Task<CustomUser> GetByUsernameOrEmail(string usernameOrEmail)
        {
            return await userDal.GetByUsernameOrEmail(usernameOrEmail);
        }

        public async Task? Remove(CustomUser customUser)
        {
            await userDal.Remove(customUser);
        }

        public async Task? Update(CustomUser customUser)
        {
            await userDal.Update(customUser);
        }
    }
}
