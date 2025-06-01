using TenderApp.Data;
using TenderApp.Models;

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
                    ("Role is not specified");
        }

        protected override string GetDeleteErrorMessage(Role item)
            => "Unable to delete role: "
            + "there is data associated with it.";
    }
}

