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
    public class UserInfoController : ApiController, IRepositoryHandler<UserInfo>
    {
        private static IRepository<UserInfo> repository;
        
        //// GET api/userinfo
        //public IEnumerable<UserInfoLightModel> Get()
        //{
        //    return this.Repository.All().Select(user => new UserInfoLightModel() 
        //        {
        //            Id = user.Id,
        //            Nickname = user.Description,
        //            PictureUrl = user.PictureUrl
        //        }).OrderBy(user => user.Nickname);
        //}

        // GET api/userinfo
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            var results = this.Repository.All().Select(user => new UserInfoLightModel() 
                {
                    Id = user.Id,
                    Nickname = user.Description,
                    PictureUrl = user.PictureUrl
                })
                .OrderBy(user => user.Nickname);
            return request.CreateResponse(HttpStatusCode.OK, results);
        }

        //// GET api/userinfo/5
        //public UserInfoModel Get(int id)
        //{
        //    var user = this.Repository.All().FirstOrDefault(u => u.Id == id);
        //    if (user != null)
        //    {
        //        var result = new UserInfoModel()
        //        {
        //            Id = user.Id,
        //            Description = user.Description,
        //            Nickname = user.Description,
        //            PictureUrl = user.PictureUrl,
        //            FavouriteRecepies = user.FavouriteRecepies.Select(r => new RecepieLightModel()
        //            {
        //                Id = r.Recepie.Id,
        //                Name = r.Recepie.Name,
        //                Date = r.Recepie.Date,
        //                PictureUrl = r.Recepie.PictureUrl,
        //                Nickname = r.Recepie.UserInfo.Nickname
        //            }).OrderBy(r => r.Name),
        //            Recepies = user.Recepies.Select(r => new RecepieLightModel()
        //            {
        //                Id = r.Id,
        //                Date = r.Date,
        //                PictureUrl = r.PictureUrl,
        //                Nickname = user.Nickname
        //            }).OrderBy(r => r.Date),
        //            RecepieComments = user.RecepieComments.Select(r => new RecepieCommentModel()
        //            {
        //                Id = r.Id,
        //                Date = r.Date,
        //                RecepieId = r.RecepieId,
        //                Content = r.Content,
        //                UserInfoId = user.Id,
        //                Nickname = user.Nickname
        //            }).OrderBy(c => c.Date)
        //        };
        //        return result;
        //    }
        //    else
        //    {
        //        //Should return HTML Responce
        //        throw new NotImplementedException();
        //    }
        //}

        // GET api/userinfo/5
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
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
                return request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
	        {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
	        }
        }

        // POST api/userinfo
        public void Post([FromBody]UserInfo value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
        }

        // PUT api/userinfo/5
        public void Put(int id, [FromBody]UserInfo value)
        {
            //Should only edit the passed values. Should not tuch any other values
            throw new NotImplementedException();
        }

        // DELETE api/userinfo/5
        public void Delete(int id)
        {
            var user = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                this.Repository.SaveChanges();
            }
            else
            {
                //Should return HTML Responce
                throw new NotImplementedException();
            }
        }

        public IRepository<UserInfo> Repository
        {
            get 
            {
                if (repository == null)
                {
                    repository = new Repository<UserInfo>();
                }
                return repository;
            }
        }
    }
}
