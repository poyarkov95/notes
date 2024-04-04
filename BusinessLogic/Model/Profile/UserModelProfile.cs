using Postgres.Entity;

namespace BusinessLogic.Model.Profile
{
    public class UserModelProfile : AutoMapper.Profile
    {
        public UserModelProfile()
        {
            CreateMap<UserRegisterModel, User>();
        }
    }
}