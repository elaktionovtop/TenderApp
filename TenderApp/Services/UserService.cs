using TenderApp.Data;
using TenderApp.Models;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TenderApp.Services;

namespace TenderApp.Services
{
    public class UserService(TenderDbContext db) : DbService<User>(db)
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
                    ("Имя пользователя не указано");

            if(string.IsNullOrWhiteSpace(item.Login))
                throw new ArgumentException
                    ("Логин пользователя не указано");

            if(string.IsNullOrWhiteSpace(item.Password))
                throw new ArgumentException
                    ("Пароль пользователя не указано");

            if(item.Role is null)
                throw new ArgumentException("Роль пользователя не указана");
        }

        protected override string GetDeleteErrorMessage(User item)
            => "Невозможно удалить пользователя: "
            + "есть данные связанные с ним.";
    }
}

