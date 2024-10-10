using Microsoft.EntityFrameworkCore;
using Zust.Core.DataAccess.EntityFramework;
using Zust.DataAccess.Abstract;
using Zust.Entity.Data;
using Zust.Entity.Entities;
namespace Zust.DataAccess.Concrete
{
    public class UserDal : EFEntityRepositoryBase<CustomUser, ZustDbContext>, IUserDal
    {
        private readonly ZustDbContext _db;

        public UserDal(ZustDbContext db)
        {
            _db = db;
        }

        public async Task<List<CustomUser>> GetAll(string userId)
        {
            return await _db.Users.Where(u=>u.Id!=userId).ToListAsync();   
        }

        public async Task<List<CustomUser>> GetAllUserByIsOnline(string id)
        { 
            return await _db.Users.Where(u=>u.Id!=id).
                OrderByDescending(i=>i.IsOnline).
                ToListAsync(); 
        }

        public async Task<CustomUser> GetByUsernameOrEmail(string nameOrEmail)
        {
             return await _db.Users.SingleOrDefaultAsync(x => x.UserName == nameOrEmail || x.Email == nameOrEmail);
   
        }

        public async Task? Remove(CustomUser customUser)
        {
            await Task.Run(() => { _db.Remove(customUser); });
            await _db.SaveChangesAsync();
        }

        public async Task? Update(CustomUser customUser)
        {
            await Task.Run(() => { _db.Update(customUser); });
            await _db.SaveChangesAsync();   
        }
    }
}
