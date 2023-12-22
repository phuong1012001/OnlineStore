using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Cms.Models.Request.User
{
    public enum RoleReq
    {
        Admin = 1,
        Clerk,
        Customer
    }
    public class UserReq
    {
        public string FristName { get; set; }

        public string LastName { get; set; }

        [StringLength(50)]
        public string Civilianld { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}",
                       ApplyFormatInEditMode = true)]
        public DateTime DateOfBirth { get; set; }

        public RoleReq Role { get; set; }
    }
}
