using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.DataAccess.Abstract;
using Zust.Entity.Data;
using Zust.Entity.Entities;

namespace Zust.DataAccess.Concrete
{
    public class PostDal : IPostDal
    {
        private readonly ZustDbContext zustDbContext;

        public PostDal(ZustDbContext zustDbContext)
        {
            this.zustDbContext = zustDbContext;
        }

        public async Task Add(Post post)
        {
           await zustDbContext.Posts.AddAsync(post);  
            await zustDbContext.SaveChangesAsync();
        }

        public async Task<Post> GetPostById(int id)
        {
           return await zustDbContext.Posts.FirstOrDefaultAsync(p=>p.Id==id);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await zustDbContext.Posts.ToListAsync();
        }

        public async Task Remove(Post post)
        {

            await Task.Run(() =>
            {
                zustDbContext.Remove(post);
            });
            await zustDbContext.SaveChangesAsync(); 
        }

        public async Task Update(Post post)
        {
            await Task.Run(() =>
            {
                zustDbContext.Update(post);
            });
            await zustDbContext.SaveChangesAsync();
        }
    }
}
