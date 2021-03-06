﻿using System;
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
    public class RecepiesController : ApiController
    {
       
        // GET api/recepies/all
        [HttpGet]
        public HttpResponseMessage All()
        {
            var results = this.Repository.All().Select(RecepieLightModel.FromDbModel).OrderBy(r => r.Date);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        // GET api/recepies/select/5
        [HttpGet]
        public HttpResponseMessage Select(int id)
        {
            var recepies = this.Repository.All();
            var recepie = recepies.FirstOrDefault(u => u.Id == id);
            if (recepie != null)
            {
                var result = new RecepieModel()
                {
                    Id = recepie.Id,
                    Name = recepie.Name,
                    Description = recepie.Description,
                    Date = recepie.Date,
                    PictureUrl = recepie.PictureUrl,
                    UserInfo = new UserInfoLightModel()
                    {
                        Id = recepie.UserInfo.Id,
                        Nickname = recepie.UserInfo.Nickname,
                        PictureUrl = recepie.UserInfo.PictureUrl
                    },
                    RecepiePhases = recepie.Phases
                        .Select(RecepiePhaseModel.FromDbModel.Compile())
                        .OrderBy(ph => ph.NumberOfPhase),
                    Tags = recepie.Tags
                        .Select(TagModel.FromDbModel.Compile())
                        .OrderBy(t => t.Name),
                    Comments = recepie.Comments
                        .Select(RecepieCommentModel.FromDbModel.Compile())
                        .OrderBy(c => c.Date),
                    UsersFavouritedThisRecepie = recepie.UsersFavouritedThisRecepie
                        .Select(fav => fav.UserInfo)
                        .Select(UserInfoLightModel.FromDbModel.Compile())
                        .OrderBy(u => u.Nickname)
                };
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
	        {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The recepie was not found");
	        }
        }

        
        // GET api/recepies/minutes/5
        [HttpGet]
        public HttpResponseMessage Minutes(int recepieId)
        {
            var recepies = this.Repository.All();
            var recepie = recepies.FirstOrDefault(u => u.Id == recepieId);
            if (recepie != null)
            {
                int totalTime = 0;
                foreach (var phase in recepie.Phases.Select(p => p.Minutes))
                {
                    totalTime += phase;
                }
                return Request.CreateResponse(HttpStatusCode.OK, totalTime);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The recepie was not found");
            }
        }

        // POST api/recepies/add
        [HttpPost]
        public HttpResponseMessage Add([FromBody]Recepie value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var user = this.Data.UserInfos.All().FirstOrDefault(u=>u.Id == value.UserInfoId);
                if (user == null || user.Nickname != nickname)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only add a recepie for himself/herself");
                }

                value.Date = DateTime.Now;
                this.Repository.Add(value);
                this.Repository.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        // PUT api/recepies/edit/5
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]RecepieModel value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var recepie = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (recepie != null)
                {
                    if (recepie.UserInfo.Nickname != nickname)
                    {
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only edit his/her own recepies");
                    }
                    recepie.Date = value.Date;
                    recepie.Description = value.Description;
                    recepie.Name = value.Name;
                    recepie.PictureUrl = value.PictureUrl;

                    this.Repository.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The recepie was not found");
                }
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }

        // DELETE api/recepies/Delete/5
        [HttpDelete]
        public HttpResponseMessage Delete(int id, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var recepie = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (recepie != null)
                {
                    if (recepie.UserInfo.Nickname != nickname)
                    {
                        return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only delete his/her own recepies");
                    }

                    recepie.IsDeleted = true;
                    this.Repository.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The recepie was not found");
                }
            }
            else
            {
                return Request.CreateResponse(messageIfUserError.Key, messageIfUserError.Value);
            }
        }
        
        
        private IRepository<Recepie> Repository
        {
            get 
            {
                return UnitOfWorkHandler.Data.Recepies;
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
