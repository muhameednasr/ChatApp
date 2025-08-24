using ChatApp.Models;

namespace ChatApp.Helper
{
    public interface ICurrentUserService
    {
        string UserId { get; }
        Task<AppUser> GetUser();
    }
    
}
