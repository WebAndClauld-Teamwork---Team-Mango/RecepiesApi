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
    public class CommentsController : ApiController, IRepositoryHandler<RecepieComment>, IController
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
        public HttpResponseMessage Add([FromBody]RecepieComment value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]RecepieCommentModel value)
        {
            var recepie = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (recepie != null)
            {
                //Should only edit the passed values. Should not tuch any other values
                throw new NotImplementedException();
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The comment was not found");
            }
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
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The comment was not found");
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
