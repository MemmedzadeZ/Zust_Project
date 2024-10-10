using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Business.Abstract;
using Zust.DataAccess.Abstract;
using Zust.Entity.Entities;

namespace Zust.Business.Concrete
{
    public class PostService : IPostService
    {
        private readonly IPostDal postDal;

        public PostService(IPostDal postDal)
        {
            this.postDal = postDal;
        }

        public async Task Add(Post post)
        {
           await postDal.Add(post);
        }

        public async Task<Post> GetPostById(int id)
        {
           return await postDal.GetPostById(id);
        }

        public async Task<List<Post>> GetPosts()
        {
          return  await postDal.GetPosts();
        }

        public async Task Remove(Post post)
        {
           await postDal.Remove(post);
        }

        public async Task Update(Post post)
        {
            await postDal.Update(post);
        }
    }
}
