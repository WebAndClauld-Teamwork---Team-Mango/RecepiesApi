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

namespace RecepiesApp.Services.Controllers
{
    public class CommentsController : ApiController, IRepositoryHandler<RecepieComment>
    {
        public CommentsController() 
        {
            if (repository == null)
            {
                repository = new Repository<RecepieComment>();
            }
        }

        private static IRepository<RecepieComment> repository;
        
        [HttpGet]
        public HttpResponseMessage All()
        {
            var comments = this.Repository.All();
                
            var results = comments.Select(RecepieCommentModel.FromDbModel);

            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
        
        [HttpGet]
        public HttpResponseMessage ByUser(int userId, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var comments = this.Repository.All().Where(c => c.UserInfoId == userId);
                var results = comments.Select(RecepieCommentModel.FromDbModel);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }
        
        [HttpGet]
        public HttpResponseMessage OnRecepie(int recepieId, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var comments = this.Repository.All().Where(c => c.RecepieId == recepieId);
                var results = comments.Select(RecepieCommentModel.FromDbModel);
                return Request.CreateResponse(HttpStatusCode.OK, results);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
            
        }

        [HttpGet]
        public HttpResponseMessage Select(int id, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var cpmment = this.Repository.All().Select(RecepieCommentModel.FromDbModel).FirstOrDefault(rc => rc.Id == id);
                if (cpmment != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, cpmment);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The comment was not found");
                }
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
            
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]RecepieComment value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                this.Repository.Add(value);
                this.Repository.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]RecepieCommentModel value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var item = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (item != null)
                {
                    item.Content = value.Content;
                    item.Date = value.Date;
                    item.RecepieId = value.RecepieId;
                    item.UserInfoId = value.UserInfoId;

                    this.Repository.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The comment was not found");
                }
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
                var item = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (item != null)
                {
                    item.IsDeleted = true;
                    this.Repository.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The comment was not found");
                }
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        public Data.Repository.IRepository<RecepieComment> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
