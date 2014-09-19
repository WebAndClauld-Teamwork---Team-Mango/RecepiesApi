using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecepiesApp.Data;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;
using RecepiesApp.Services.Models;
using RecepiesApp.Services.Validators;
using Newtonsoft.Json.Linq;
using System.IO;

namespace RecepiesApp.Services.Controllers
{
    public class UserInfoController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage All()
        {
            var users = this.Repository.All();
            var results = users.Select(UserInfoLightModel.FromDbModel);
            results = results.OrderBy(user => user.Nickname);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
        
        [HttpGet]
        public HttpResponseMessage Select(int id)
        {
            var user = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                var result = new UserInfoModel()
                {
                    Id = user.Id,
                    Description = user.Description,
                    Nickname = user.Nickname,
                    PictureUrl = user.PictureUrl,
                    FavouriteRecepies = user.FavouriteRecepies
                        .Select(f => f.Recepie)
                        .Select(RecepieLightModel.FromDbModel.Compile())
                        .OrderBy(r => r.Name),
                    Recepies = user.Recepies
                        .Select(RecepieLightModel.FromDbModel.Compile())
                            .OrderBy(r => r.Date),
                    RecepieComments = user.RecepieComments
                        .Select(RecepieCommentModel.FromDbModel.Compile())
                        .OrderBy(c => c.Date)
                };
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
	        {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The user was not found");
	        }
        }


        [HttpPost]
        public HttpResponseMessage Register([FromBody]UserInfoLoginModel value) 
        {
            if (this.Repository.All().Any(u => u.Nickname == value.Nickname))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The user name is already taken");
            }
            if (!value.AuthCode.StartsWith(value.Nickname))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid AuthCode");
            }
            if (value.AuthCode.Length - 6 < value.Nickname.Length)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The password is not long enough. Minimum 6 symbols");
            }

            string sessionKey = Guid.NewGuid().ToString();
            this.Repository.Add(new UserInfo() 
            {
                Nickname = value.Nickname,
                AuthCode = value.AuthCode,
                SessionKey = sessionKey,
                SessionExpirationDate = DateTime.Now.AddDays(1),
                PictureUrl = this.GetRandomPictureUrl()
            });
            this.Repository.SaveChanges();
            
            return Request.CreateResponse(HttpStatusCode.OK, sessionKey);
        }

        private string GetRandomPictureUrl()
        {
            string url = "https://api.github.com/repos/WebAndClauld-Teamwork---Team-Mango/RecepiesApi/contents/Avatars";
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.ContentType = "application/json";
            request.Method = "GET";

            request.ContentLength = 0;
            request.Accept = "*/*";
            request.Headers.Add(HttpRequestHeader.AcceptLanguage, "en-us");
            request.UserAgent =
               "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;" +
               ".NET CLR 1.1.4322; .NET CLR 2.0.50727; .NET CLR 3.0.4506.2152; InfoPath.2;" +
               ".NET CLR 3.5.21022; .NET CLR 3.5.30729; .NET4.0C; .NET4.0E)";
            request.UseDefaultCredentials = true;
            request.PreAuthenticate = true;
            request.AllowAutoRedirect = false;

            WebResponse response = request.GetResponse();

            string responseString;
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                responseString = reader.ReadToEnd();
            }

            JArray json = JArray.Parse(responseString);
            JToken value = json[new Random().Next(json.Count)]["name"];

            string picName = value.Value<string>();

            return "https://raw.githubusercontent.com/WebAndClauld-Teamwork---Team-Mango/RecepiesApi/master/Avatars/" + picName;
        }

        [HttpPut]
        public HttpResponseMessage Login([FromBody]UserInfoLoginModel value)
        {
            var user = this.Repository.All().FirstOrDefault(u =>
                u.Nickname == value.Nickname &&
                u.AuthCode == value.AuthCode);
            if (user == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Incorrect password or username");
            }
            else
            {
                string sessionKey = Guid.NewGuid().ToString();
                user.SessionKey = sessionKey;
                user.SessionExpirationDate = DateTime.Now.AddDays(1);
                return Request.CreateResponse(HttpStatusCode.OK, sessionKey);
            }
        }
        
        [HttpPut]
        public HttpResponseMessage Logout(string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var user = this.Repository.All().FirstOrDefault(u =>
                u.Nickname == nickname);
                user.SessionKey = "";
                user.SessionExpirationDate = DateTime.Now.AddDays(-1);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        // POST api/userinfo
        [HttpPost]
        [Authorize(Roles="Admin")]
        public HttpResponseMessage Add([FromBody]UserInfo value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/userinfo/5
        [HttpPut]
        [Authorize(Roles="Admin")]
        public HttpResponseMessage Edit(int id, [FromBody]UserInfoModel value)
        {
            var item = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (item != null)
            {
                item.Nickname = value.Nickname;
                item.Description = value.Description;
                item.PictureUrl = value.PictureUrl;

                this.Repository.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The user was not found");
            }
        }

        // DELETE api/userinfo/5
        [HttpDelete]
        [Authorize(Roles="Admin")]
        public HttpResponseMessage Delete(int id)
        {
            var user = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                this.Repository.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The user was not found");
            }
        }

        private IRepository<UserInfo> Repository
        {
            get 
            {
                return UnitOfWorkHandler.Data.UserInfos;
            }
        }
        private IRecepiesData Data
        {
            get 
            {
                return UnitOfWorkHandler.Data;
            }
        }
    }
}
