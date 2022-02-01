using Aircon.Business.Media;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Business.Models.Customer.Profile
{
    public class UserProfileModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string WorkTitle { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public PictureModel Avatar { get; set; }
    }
}
