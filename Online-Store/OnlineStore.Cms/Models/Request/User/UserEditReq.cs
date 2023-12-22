namespace OnlineStore.Cms.Models.Request.User
{
    public class UserEditReq
    {
        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Civilianld { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public RoleReq Role { get; set; }
    }
}
