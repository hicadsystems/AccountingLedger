

using AutoMapper;
using NavyAccountCore.Core.Entities;
using NavyAccountWeb.ViewModels.Users;

namespace NavyAccountWeb.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            //CreateMap<User, User>();
        }
    }
}
