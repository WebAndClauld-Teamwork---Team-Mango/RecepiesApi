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
    public class RecepiesController : ApiController, IRepositoryHandler<Recepie>, IController
    {
        public RecepiesController() 
        {
            if (repository == null)
            {
                repository = new Repository<Recepie>();
            }
        }
        private static IRepository<Recepie> repository;

        // GET api/recepies/all
        [HttpGet]
        public HttpResponseMessage All()
        {
            var results = this.Repository.All().Select(r => new RecepieLightModel()
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Date = r.Date,
                        Description = r.Description,
                        PictureUrl = r.PictureUrl,
                        Nickname = r.UserInfo.Nickname
                    }).OrderBy(r => r.Date);
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
                    RecepiePhases = recepie.Phases.Select(ph => new RecepiePhaseModel()
                    {
                        Id = ph.Id,
                        IsImportnt = ph.IsImportnt,
                        Minutes = ph.Minutes,
                        NumberOfPhase = ph.NumberOfPhase,
                        Name = ph.Name
                    }).OrderBy(ph => ph.NumberOfPhase),
                    Tags = recepie.Tags.Select(t => new TagModel()
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).OrderBy(t => t.Name),
                    Comments = recepie.Comments.Select(c => new RecepieCommentModel()
                    {
                        Id = c.Id,
                        Date = c.Date,
                        Content = c.Content,
                        RecepieId = recepie.Id,
                        UserInfoId = c.UserInfoId,
                        Nickname = c.UserInfo.Nickname
                    }).OrderBy(c => c.Date),
                    UsersFavouritedThisRecepie = recepie.UsersFavouritedThisRecepie.Select(fav => new UserInfoLightModel()
                    {
                        Id = fav.UserInfo.Id,
                        Nickname = fav.UserInfo.Nickname,
                        PictureUrl = fav.UserInfo.PictureUrl
                    })
                };
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
	        {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The recepie was not found");
	        }
        }

        // POST api/recepies/add
        [HttpPost]
        public HttpResponseMessage Add([FromBody]
                                       Recepie value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/recepies/edit/5
        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]
                                        RecepieLightModel value)
        {
            var recepie = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (recepie != null)
            {
                //Should only edit the passed values. Should not tuch any other values
                throw new NotImplementedException();
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The recepie was not found");
            }
        }

        // DELETE api/recepies/Delete/5
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
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The recepie was not found");
            }
        }
        
        public IRepository<Recepie> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
