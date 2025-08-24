using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Helper
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext context;
        public CurrentUserService( IHttpContextAccessor httpContextAccessor,ApplicationDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            this.context = context;
        }
       public string UserId
        {
            get
            {
                var id = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(x=>x.Type=="user_id")?.Value;
                if (string.IsNullOrEmpty(id))
                {
                    id=_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                return id;
            }
        }

        public async Task<AppUser> GetUser()
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
            return user;
        }
    }
}
