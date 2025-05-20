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
    public class RoleService(TenderDbContext db) : DbService<Role>(db)
    {
        public override Role Clone(Role source)
        {
            return new()
            {
                Id = source.Id,
                Code = source.Code,
                Name = source.Name
            };
        }

        public override void CopyTo(Role source, Role target)
        {
            target.Id = source.Id;
            target.Code = source.Code;
            target.Name = source.Name;
        }

        public override void Validate(Role item)
        {
            if(string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException
                    ("Роль не указана");
        }

        protected override string GetDeleteErrorMessage(Role item)
            => "Невозможно удалить роль: "
            + "есть данные связанные с ней.";
    }
}

