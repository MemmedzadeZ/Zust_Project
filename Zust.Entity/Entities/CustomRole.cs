using Microsoft.AspNetCore.Identity;
using Zust.Core.Entities;

namespace Zust.Entity.Entities
{
    public class CustomRole:IdentityRole<string>,IEntity
    {
        public CustomRole()
        {

            Id = Guid.NewGuid().ToString();
        }
    }
}
