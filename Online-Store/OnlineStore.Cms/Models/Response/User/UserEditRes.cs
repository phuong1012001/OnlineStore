namespace OnlineStore.Cms.Models.Response.User
{
    public class UserEditRes
    {
        public string FristName { get; set; }

        public string LastName { get; set; }

        public string Civilianld { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public RoleRes Role { get; set; }
    }
}
