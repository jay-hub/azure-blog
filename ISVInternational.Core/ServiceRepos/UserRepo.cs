using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISVInternational.Ef;
using ISVInternational.Entities.DTO;
using ISVInternational.Entities.Entities;

namespace ISVInternational.Core.ServiceRepos
{
    public class UserRepo
    {
        private static UserRepo _instance;
        private BlogContext db;
        private UserRepo()
        {
            db = new BlogContext();
        }
        public static UserRepo Instance => _instance ?? (_instance = new UserRepo());

        public UserDetailsDto GetOrCreateUser(UserDetailsDto para)
        {
            var user = (from x in db.Users
                where x.EmailAddress.Equals(para.Email)
                select x).SingleOrDefault();

            var userDto = new UserDetailsDto();
            try
            {
                if (user != null)
                {
                    userDto = new UserDetailsDto()
                    {
                        UserGuid = user.UserOId,
                        DisplayName = user.NickName,
                        Email = user.EmailAddress,
                        Mobile = user.MobileNo,
                        IsNewUser = false,
                        ProfileUrl = user.ProfileUrl,
                        AuthTime = user.LastLoginTime.ToLongDateString()
                    };
                }
                else
                {
                    user = new User
                    {
                        UserOId = userDto.UserGuid = Guid.NewGuid(),
                        EmailAddress = para.Email,
                        MobileNo = para.Mobile,
                        Address = para.Email,
                        Birthday = DateTime.Now,
                        IsDeleted = 0,
                        UserType = 1,
                        NickName = para.DisplayName,
                        CreatedDate = DateTime.Now,
                        LastLoginTime = DateTime.Now,
                        ProfileUrl = para.ProfileUrl
                    };

                    db.Users.Add(user);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return userDto;
        }

        public List<UserDetailsDto> GetAllUsers()
        {
            var users = (from x in db.Users
                select new UserDetailsDto()
                {
                    UserGuid = x.UserOId,
                    Bio = x.Bio,
                    DisplayName = x.NickName,
                    Email = x.EmailAddress,
                    ProfileUrl = x.ProfileUrl
                }).ToList();

            return users;
        }

        public bool UpdateProfile(UserDetailsDto userDto)
        {
            try
            {
                var user = (from x in db.Users
                    where x.UserOId == userDto.UserGuid
                    select x).SingleOrDefault();

                if (user != null)
                {
                    user.NickName = userDto.DisplayName;
                    user.ProfileUrl = userDto.ProfileUrl;
                    db.SaveChanges();

                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }
    }
}
 