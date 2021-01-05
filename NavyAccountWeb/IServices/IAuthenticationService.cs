

using NavyAccountCore.Core.Entities;
using NavyAccountCore.Core.Models;
using System.Threading.Tasks;

namespace NavyAccountWeb.IServices
{
    public interface IAuthenticationService
    {
        Task<ResponseModel<User>> SignInUserAsync(string email, string password, string client);

        Task SignOutUserAsync();
    }
}
