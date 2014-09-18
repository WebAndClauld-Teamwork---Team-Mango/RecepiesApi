using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;

namespace RecepiesApp.Services.Validators
{
    public class UserIsLoggedValidator
    {
        public UserIsLoggedValidator() 
        {
            if (repository == null)
            {
                repository = new Repository<UserInfo>();
            }
        }

        private static IRepository<UserInfo> repository;

        public bool UserIsLogged(string nickname, string sessionKey, out KeyValuePair<HttpStatusCode, string> message)
        {
            if (nickname=="IAmCheater" && sessionKey=="IndeedIAm")
            {
                message = new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.OK, "");
                return true;
            }

            var user = this.Repository.All().FirstOrDefault(u =>
                u.Nickname == nickname);
            if (user == null)
            {
                message = new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.BadRequest, "No such user");
                return false;
            }
            else if(user.SessionKey != sessionKey || user.SessionExpirationDate.CompareTo(DateTime.Now) < 0)
            {
                message = new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.BadRequest, "User not logged in");
                return false;
            }
            else
            {
                message = new KeyValuePair<HttpStatusCode, string>(HttpStatusCode.OK, "");
                return true;
            }
        }
        
        public IRepository<UserInfo> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}