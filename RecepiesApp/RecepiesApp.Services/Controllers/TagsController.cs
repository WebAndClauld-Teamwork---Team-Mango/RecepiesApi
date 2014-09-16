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
            var results = this.Repository.All().Select(t => new TagModel()
            {
                Id = t.Id,
                Name = t.Name
            });
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet]
        public HttpResponseMessage Select(int id)
        {
            var tag = this.Repository.All().FirstOrDefault(t => t.Id == id);
            var result = new TagModel()
            {
                Id = tag.Id,
                Name = tag.Name,
                Recepies = tag.Recepies.Select(r => new RecepieLightModel()
                { 
                    Id = r.Id,
                    Date = r.Date,
                    Name = r.Name,
                    Description = r.Description,
                    Nickname = r.UserInfo.Nickname,
                    PictureUrl = r.PictureUrl
                })
            };
            return Request.CreateResponse(HttpStatusCode.OK, result);

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

        public Data.Repository.IRepository<Tag> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
