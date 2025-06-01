using Microsoft.EntityFrameworkCore;

using TenderApp.Data;
using TenderApp.Models;

namespace TenderApp.Services
{
    public interface IAuthService
    {
        User? CurrentUser { get; }
        bool IsLoginValid(string login, string password);
    }

    public class AuthService(TenderDbContext db) : IAuthService
    {
        private readonly TenderDbContext _db = db;

        public User? CurrentUser { get; private set; }

        public bool IsLoginValid(string login, string password)
        {
            CurrentUser = _db.Users?.Include(u => u.Role)?.FirstOrDefault(u =>
                u.Login == login && u.Password == password);
            return CurrentUser != null;
        }
    }
}


