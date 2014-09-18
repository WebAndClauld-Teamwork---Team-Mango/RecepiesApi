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
    public class TagsController : ApiController, IRepositoryHandler<Tag>
    {
        public TagsController() 
        {
            if (repository == null)
            {
                repository = new Repository<Tag>();
            }
        }

        private static IRepository<Tag> repository;
        
        [HttpGet]
        public HttpResponseMessage All(string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var results = this.Repository.All().Select(TagModel.FromDbModel);
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
                var tag = this.Repository.All().Select(TagModel.FromDbModelWithRecepies).FirstOrDefault(t => t.Id == id);
                return Request.CreateResponse(HttpStatusCode.OK, tag);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }

        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]Tag value, string nickname, string sessionKey)
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
        public HttpResponseMessage Edit(int id, [FromBody]TagModel value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var item = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (item != null)
                {
                    item.Name = value.Name;
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

        public Data.Repository.IRepository<Tag> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
