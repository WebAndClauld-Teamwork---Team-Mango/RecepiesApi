using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RecepiesApp.Data.Repository;
using RecepiesApp.Models;
using RecepiesApp.Services.Models;

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
        }

        private static IRepository<UserFavouriteRecepie> repository;
        
        
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
        public HttpResponseMessage Add([FromBody]UserFavouriteRecepie value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
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

        public Data.Repository.IRepository<UserFavouriteRecepie> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
