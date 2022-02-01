using Aircon.Business.Media;
using Aircon.Business.Models.Customer.Profile;
using Aircon.Data;
using System.Linq;

namespace Aircon.Business.Services.Customer
{
    public interface IUserProfileService
    {
        UserProfileModel GetUserProfile(int userId);
        UserProfileModel SaveUserProfile(UserProfileModel userProfileModel);

    }

    public class UserProfileService : IUserProfileService
    {
        private readonly AirconDbContext _airconDbContext;
        public UserProfileService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }


        public UserProfileModel GetUserProfile(int userId)
        {
            return _airconDbContext.Users.Where(x => x.Id == userId).Select(x => new UserProfileModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                WorkTitle = x.WorkTitle,
                UserName = x.UserName,
                Email = x.Email,
                IsActive = x.IsActive,
                PhoneNumber = x.PhoneNumber,
                Avatar = new Media.PictureModel { EntityId = x.Id, Id = x.AvatarId, PictureType = PictureType.UserAvatar}
            }).SingleOrDefault();
        }

        public UserProfileModel SaveUserProfile(UserProfileModel userProfileModel)
        {
            var user = _airconDbContext.Users.Where(x => x.Id == userProfileModel.Id).SingleOrDefault();
            if (user != null)
            {
                user.FirstName = userProfileModel.FirstName;
                user.LastName = userProfileModel.LastName;
                user.WorkTitle = userProfileModel.WorkTitle;
                user.Email = userProfileModel.Email;
                user.PhoneNumber = userProfileModel.PhoneNumber;
                user.AvatarId  = userProfileModel.Avatar.Id.HasValue ? userProfileModel.Avatar.Id.Value == 0 ? null : userProfileModel.Avatar.Id.Value : null;
                _airconDbContext.Users.Update(user);
                _airconDbContext.SaveChanges();
            }
            else
            {
                return null;
            }
            return userProfileModel;
        }
    }
}
