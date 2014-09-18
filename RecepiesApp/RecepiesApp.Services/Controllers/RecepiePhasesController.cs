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

namespace RecepiesApp.Services.Controllers
{
    public class RecepiePhasesController  : ApiController
    {
        
        [HttpGet]
        public HttpResponseMessage All(string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var results = this.Repository.All().Select(RecepiePhaseModel.FromDbModel);
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
                var results = this.Repository.All().Where(p => p.RecepieId == recepieId).Select(RecepiePhaseModel.FromDbModel);
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
                var phase = this.Repository.All().Select(RecepiePhaseModel.FromDbModel).FirstOrDefault(t => t.Id == id);
                return Request.CreateResponse(HttpStatusCode.OK, phase);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]RecepiePhase value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var recepie = this.Data.Recepies.All().FirstOrDefault(r=>r.Id == value.RecepieId);

                if (recepie == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No such recepie was found");
                }
                if (recepie.UserInfo.Nickname != nickname)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only add phases to his/her own recepie");
                }

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
        public HttpResponseMessage Edit(int id, [FromBody]RecepiePhaseModel value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var item = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (item != null)
                {
                    if (item.Recepie.UserInfo.Nickname != nickname)
                    {
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only edit phases of his/her own recepie");
                    }

                    item.IsImportnt = value.IsImportnt;
                    item.Minutes = value.Minutes;
                    item.Name = value.Name;
                    item.NumberOfPhase = value.NumberOfPhase;

                    this.Repository.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The phase was not found");
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
                    if (item.Recepie.UserInfo.Nickname != nickname)
                    {
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only delete phases of his/her own recepie");
                    }

                    item.IsDeleted = true;
                    this.Repository.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The phase was not found");
                }
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }
        
        private IRepository<RecepiePhase> Repository
        {
            get 
            {
                return UnitOfWorkHandler.Data.RecepiePhases;
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
