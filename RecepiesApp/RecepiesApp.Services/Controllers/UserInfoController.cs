using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;
using RecepiesApp.Services.Models;

namespace RecepiesApp.Services.Controllers
{
    public class UserInfoController : ApiController, IRepositoryHandler<UserInfo>, IController
    {
        public UserInfoController() 
        {
            if (repository == null)
            {
                repository = new Repository<UserInfo>();
            }
        }

        private static IRepository<UserInfo> repository;
        
        [HttpGet]
        public HttpResponseMessage All()
        {
            var users = this.Repository.All();
                
            var results = users.Select(user => new UserInfoLightModel() 
                {
                    Id = user.Id,
                    Nickname = user.Description,
                    PictureUrl = user.PictureUrl
                });
                
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
                    Nickname = user.Description,
                    PictureUrl = user.PictureUrl,
                    FavouriteRecepies = user.FavouriteRecepies.Select(r => new RecepieLightModel()
                    {
                        Id = r.Recepie.Id,
                        Name = r.Recepie.Name,
                        Date = r.Recepie.Date,
                        PictureUrl = r.Recepie.PictureUrl,
                        Nickname = r.Recepie.UserInfo.Nickname
                    }).OrderBy(r => r.Name),
                    Recepies = user.Recepies.Select(r => new RecepieLightModel()
                    {
                        Id = r.Id,
                        Date = r.Date,
                        PictureUrl = r.PictureUrl,
                        Nickname = user.Nickname
                    }).OrderBy(r => r.Date),
                    RecepieComments = user.RecepieComments.Select(r => new RecepieCommentModel()
                    {
                        Id = r.Id,
                        Date = r.Date,
                        RecepieId = r.RecepieId,
                        Content = r.Content,
                        UserInfoId = user.Id,
                        Nickname = user.Nickname
                    }).OrderBy(c => c.Date)
                };
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
	        {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The user was not found");
	        }
        }

        // POST api/userinfo
        [HttpPost]
        public HttpResponseMessage Add([FromBody]UserInfo value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/userinfo/5
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]UserInfoLightModel value)
        {
            var user = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                //Should only edit the passed values. Should not tuch any other values
                throw new NotImplementedException();
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The user was not found");
            }
        }

        // DELETE api/userinfo/5
        [HttpDelete]
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

        public IRepository<UserInfo> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
