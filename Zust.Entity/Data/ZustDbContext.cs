using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Zust.Entity.Entities;

namespace Zust.Entity.Data
{
    public class ZustDbContext:IdentityDbContext<CustomUser,CustomRole,string>
    {
        public ZustDbContext(DbContextOptions<ZustDbContext> options) : base(options)
        {

        }
        public ZustDbContext()
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Comment>()
            // .HasOne(c => c.Reply)
            // .WithMany()
            // .HasForeignKey(c => c.ReplyId)
            // .OnDelete(DeleteBehavior.NoAction); // Döngüleri önlemek için

             

            builder.Entity<Comment>()
       .HasOne(c => c.Post)
       .WithMany(p => p.Comments)
       .HasForeignKey(c => c.PostId)
       .OnDelete(DeleteBehavior.NoAction); // Kaskad silme yerine NoAction kullanıldı

           
            builder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Notification> Notifications { get; set; }  // 'Notifications' yerine yanlışlıkla 'Notification' kullanmış olabilirsin.
        public DbSet<Friend> Friends { get; set; }
        public DbSet<FriendRequest> FriendRequest { get; set; }

    }
}
