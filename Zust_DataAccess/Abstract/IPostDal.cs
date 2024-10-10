﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zust.Entity.Entities;

namespace Zust.DataAccess.Abstract
{
   public interface IPostDal
    {
        Task<List<Post>> GetPosts();
        Task<Post>  GetPostById(int id);
        Task  Add(Post post);
        Task  Update(Post post);
        Task  Remove(Post post);

    }
}
