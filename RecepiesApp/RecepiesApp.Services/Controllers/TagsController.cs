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
    public class TagsController : ApiController
    {
        
        [HttpGet]
        public HttpResponseMessage All()
        {
            var results = this.Repository.All().Select(TagModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet]
        public HttpResponseMessage Select(int id)
        {
            var tag = this.Repository.All().Select(TagModel.FromDbModelWithRecepies).FirstOrDefault(t => t.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, tag);
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
        [Authorize(Roles="Admin")]
        public HttpResponseMessage Edit(int id, [FromBody]TagModel value)
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

        [HttpDelete]
        [Authorize(Roles="Admin")]
        public HttpResponseMessage Delete(int id)
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
        
        private IRepository<Tag> Repository
        {
            get 
            {
                return UnitOfWorkHandler.Data.Tags;
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
