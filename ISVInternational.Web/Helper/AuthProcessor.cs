using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using ISVInternational.Core.ServiceRepos;
using ISVInternational.Entities.DTO;

namespace ISVInternational.Web.Helper
{
    public static class AuthProcessor
    {
        public static UserDetailsDto ProcessAuthToken(IEnumerable<Claim> claims)
        {
            var userDetail = new UserDetailsDto();
            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case "http://schemas.microsoft.com/identity/claims/objectidentifier":
                        userDetail.UserId = claim.Value;
                        break;
                    case "aud":
                        userDetail.IdentityProvider = claim.Value;
                        break;
                    case "auth_time":
                        userDetail.AuthTime = claim.Value;
                        break;
                    case "name":
                        userDetail.DisplayName = claim.Value;
                        break;
                    case "extension_Mobile":
                        userDetail.Mobile = claim.Value;
                        break;
                    case "extension_UserType":
                        userDetail.UserType = claim.Value;
                        break;
                    case "emails":
                        userDetail.Email = claim.Value;
                        break;
                    case "newUser":
                        userDetail.IsNewUser = Boolean.Parse(claim.Value);
                        break;
                    default:
                        break;
                }
                
            }
            userDetail = ProcessUser(userDetail);
            SaveToSession(userDetail);
            return userDetail;
        }

        private static UserDetailsDto ProcessUser(UserDetailsDto para)
        {
            return UserRepo.Instance.GetOrCreateUser(para);
        }

        public static UserDetailsDto GetMySessionObject()
        {
            return (UserDetailsDto)HttpContext.Current.Session["_userDetailsDto"];
        }

        private static void SaveToSession(UserDetailsDto dto)
        {
            HttpContext.Current.Session.Add("_userDetailsDto", dto);
        }
    }
}