using Aircon.Business.Media;

namespace Aircon.Business.Models.Identity.SignUp
{
    public class UserDetailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Phone { get; set; }
        public string CompanyName { get; set; }
        public int CompanyId { get; set; }
        public string BranchName { get; set; }
        public string BranchId { get; set; }
        public int UserId { get; set; }
        public PictureModel Avatar { get; set; }
    }
}
