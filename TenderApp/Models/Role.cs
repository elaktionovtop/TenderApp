namespace TenderApp.Models
{
    public class Role
    {
        public int Id { get; set; }
        public RoleCode Code { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public enum RoleCode
    {
        Admin = 1,
        CategoryManager = 2,
        Buyer = 3
    }
}
