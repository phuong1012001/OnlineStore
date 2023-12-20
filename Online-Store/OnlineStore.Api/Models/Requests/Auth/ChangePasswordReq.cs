namespace OnlineStore.Api.Models.Requests.Auth
{
    public class ChangePasswordReq
    {
        public string Email { get; set; }

        public string PasswordOld { get; set; }

        public string PasswordNew { get; set; }
    }
}
