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
    public class TagsController : ApiController, IRepositoryHandler<Tag>, IController
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
        public HttpResponseMessage Add([FromBody]Tag value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
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

        public Data.Repository.IRepository<Tag> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
