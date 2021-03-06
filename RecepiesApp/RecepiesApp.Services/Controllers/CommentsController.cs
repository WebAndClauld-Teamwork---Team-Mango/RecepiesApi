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
using RecepiesApp.Services.Notifications;
using RecepiesApp.Services.Validators;

namespace RecepiesApp.Services.Controllers
{
    public class CommentsController : ApiController
    { 
        [HttpGet]
        public HttpResponseMessage All()
        {
            var comments = this.Repository.All();
            var results = comments.Select(RecepieCommentModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
        
        [HttpGet]
        public HttpResponseMessage ByUser(int userId)
        {
            var comments = this.Repository.All().Where(c => c.UserInfoId == userId);
            var results = comments.Select(RecepieCommentModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }
        
        [HttpGet]
        public HttpResponseMessage OnRecepie(int recepieId)
        {
            var comments = this.Repository.All().Where(c => c.RecepieId == recepieId);
            var results = comments.Select(RecepieCommentModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet]
        public HttpResponseMessage Select(int id)
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

        [HttpPost]
        public HttpResponseMessage Add([FromBody]RecepieComment value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var user = this.Data.UserInfos.All().FirstOrDefault(u=>u.Id == value.UserInfoId);
                if (user == null || user.Nickname != nickname)
                {
                    return Request.CreateResponse(HttpStatusCode.Forbidden, "A user can only post his/her own comments");
                }
                
                if (this.Data.Recepies.All().FirstOrDefault(r=>r.Id == value.RecepieId) == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No such recepie");
                }

                this.Repository.Add(value);
                this.Repository.SaveChanges();
                var notifyNickname = this.Repository.All()
                    .Where(rc => rc.RecepieId == value.RecepieId)
                    .Select(r => r.Recepie.UserInfo.Nickname)
                    .FirstOrDefault();
                var notifyData = new
                {
                    Event = "NewComment",
                    Nickname = nickname,
                    Content = value.Content,
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

        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]RecepieCommentModel value, string nickname, string sessionKey)
        {
            KeyValuePair<HttpStatusCode, string> messageIfUserError;
            if (new UserIsLoggedValidator().UserIsLogged(nickname, sessionKey, out messageIfUserError))
            {
                var item = this.Repository.All().FirstOrDefault(u => u.Id == id);
                if (item != null)
                {
                    if (item.UserInfo.Nickname != nickname)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "A user can only edit his/her own comments");
                    }

                    item.Content = value.Content;

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
                    if (item.UserInfo.Nickname != nickname)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, "A user can only delete his/her own comments");
                    }

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

        private IRepository<RecepieComment> Repository
        {
            get 
            {
                return UnitOfWorkHandler.Data.RecepieComments;
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
