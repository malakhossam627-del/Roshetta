using Microsoft.AspNetCore.Http;
using Roshetta.BLL.Services.Interfaces;
using System.Security.Claims;

namespace Roshetta.PL.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true)
            {
                var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (int.TryParse(idClaim, out var id))
                {
                    return id;
                }
            }

            // Default demo customer user ID (matches seeded demo customer)
            return 1;
        }

        public string GetCurrentUserName()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated == true && !string.IsNullOrEmpty(user.Identity.Name))
            {
                return user.Identity.Name;
            }

            return "David Samir";
        }
    }
}
