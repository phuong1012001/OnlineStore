using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.BusinessLogic.Constants
{
    public static class ErrorCodes
    {
        public const string NotFound = "NotFound";

        public const string NotFoundProfile = "NotFoundProfile";

        public const string NotFoundUser = "NotFoundUser";

        public const string NotFoundProduct = "NotFoundProduct";

        public const string NotFoundStock = "NotFoundStock";

        public const string NotFoundCart = "NotFoundCart";

        public const string AccountAlreadyExists = "AccountAlreadyExists";

        public const string IncorrectPassword = "IncorrectPassword";

        public const string IncorrectEmail = "IncorrectEmail";

        public const string InvalidQuantity = "InvalidQuantity";

        public const string EmptyCart = "EmptyCart";
    }
}
