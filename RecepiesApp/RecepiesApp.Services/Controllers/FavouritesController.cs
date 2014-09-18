using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecepiesApp.Data;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;
using RecepiesApp.Services.Models;
using RecepiesApp.Services.Validators;
using RecepiesApp.Services.Notifications;

namespace RecepiesApp.Services.Controllers
{
    public class FavouritesController  : ApiController
    {
        [HttpGet]
        public HttpResponseMessage ByUser(int userId)
        {
            var items = this.Repository.All().Where(c => c.UserInfoId == userId).Select(fav => fav.Recepie);
            var results = items.Select(RecepieLightModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
        
        [HttpGet]
        public HttpResponseMessage ByRecepie(int recepieId)
        {
            var items = this.Repository.All().Where(c => c.RecepieId == recepieId).Select(f => f.UserInfo);
            var results = items.Select(UserInfoLightModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]UserFavouriteRecepie value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var user = this.Data.UserInfos.All().FirstOrDefault(u=>u.Id == value.UserInfoId);
                if (user == null || user.Nickname != nickname)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only favourite a recepie for himself/herself");
                }

                this.Repository.Add(value);
                this.Repository.SaveChanges();
                var notifyNickname = this.Repository.All()
                    .Where(ufr => ufr.RecepieId == value.RecepieId)
                    .Select(r => r.Recepie.UserInfo.Nickname)
                    .FirstOrDefault();
                var notifyData = new
                {
                    Event = "NewFavourite",
                    Nickname = nickname,
                    UserInfoId = user.Id,
                    RecepieId = value.RecepieId
                };
                new Notifier().Notify(notifyNickname, notifyData);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var recepie = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (recepie != null)
                {
                    var user = this.Data.UserInfos.All().FirstOrDefault(u=>u.Id == recepie.UserInfoId);
                    if (user == null || user.Nickname != nickname)
                    {
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only delete his/her own favourites");
                    }

                    recepie.IsDeleted = true;
                    this.Repository.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The favourtite connection was not found");
                }
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        
        private IRepository<UserFavouriteRecepie> Repository
        {
            get 
            {
                return UnitOfWorkHandler.Data.UserFavouriteRecepies;
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
