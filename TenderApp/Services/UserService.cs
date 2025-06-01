using TenderApp.Data;
using TenderApp.Models;

using Microsoft.EntityFrameworkCore;

namespace TenderApp.Services
{
    public class UserService(TenderDbContext db) 
        : DbService<User>(db)
    {
        public override IEnumerable<User> GetAll()
            => _db.Users.Include(it => it.Role);

        public override User Clone(User source)
        {
            return new()
            {
                Id = source.Id,
                Name = source.Name,
                Login = source.Login,
                Password = source.Password,

                RoleId = source.RoleId,
                Role = source.Role
            };
        }

        public override void CopyTo(User source, User target)
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Login = source.Login;
            target.Password = source.Password;

            target.RoleId = source.RoleId;
            target.Role = source.Role;
        }

        public override void Validate(User item)
        {
            if(string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException
                    ("User name is not specified");

            if(string.IsNullOrWhiteSpace(item.Login))
                throw new ArgumentException
                    ("User login is not specified");

            if(string.IsNullOrWhiteSpace(item.Password))
                throw new ArgumentException
                    ("User password is not specified");

            if(item.Role is null)
                throw new ArgumentException("User role is not specified");
        }

        protected override string GetDeleteErrorMessage(User item)
            => "Unable to delete user: "
            + "there is data associated with them.";
    }
}

