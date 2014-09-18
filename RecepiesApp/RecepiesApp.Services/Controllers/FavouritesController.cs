using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;
using RecepiesApp.Services.Models;
using RecepiesApp.Services.Validators;
using RecepiesApp.Services.Notifications;

namespace RecepiesApp.Services.Controllers
{
    public class FavouritesController  : ApiController, IRepositoryHandler<UserFavouriteRecepie>
    {
        public FavouritesController() 
        {
            if (repository == null)
            {
                repository = new Repository<UserFavouriteRecepie>();
            }

            this.notifier = new Notifier();
        }

        private static IRepository<UserFavouriteRecepie> repository;
        private INotifier notifier;
        
        
        [HttpGet]
        public HttpResponseMessage ByUser(int userId, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var items = this.Repository.All().Where(c => c.UserInfoId == userId).Select(fav => fav.Recepie);
                var results = items.Select(RecepieLightModel.FromDbModel);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage ByRecepie(int recepieId, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var items = this.Repository.All().Where(c => c.RecepieId == recepieId).Select(f => f.UserInfo);
                var results = items.Select(UserInfoLightModel.FromDbModel);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]UserFavouriteRecepie value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                this.Repository.Add(value);
                this.Repository.SaveChanges();
                var notifyNickname = this.Repository.All()
                    .Where(ufr => ufr.RecepieId == value.RecepieId)
                    .Select(r => r.Recepie.UserInfo.Nickname)
                    .FirstOrDefault();
                var notificationResult = this.notifier.Notify(notifyNickname, value);
                return Request.CreateResponse(HttpStatusCode.OK, notificationResult);
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

        public Data.Repository.IRepository<UserFavouriteRecepie> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
