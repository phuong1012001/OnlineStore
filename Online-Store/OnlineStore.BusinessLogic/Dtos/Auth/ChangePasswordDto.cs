using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Dtos.Auth
{
    public class ChangePasswordDto
    {
        public string Email { get; set; }

        public string PasswordOld { get; set; }

        public string PasswordNew { get; set; }
    }
}
