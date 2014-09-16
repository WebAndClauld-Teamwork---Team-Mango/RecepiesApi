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
    public class RecepiesController : ApiController, IRepositoryHandler<Recepie>
    {
        private static IRepository<Recepie> repository;

        // GET api/recepies
        public IEnumerable<RecepieLightModel> Get()
        {
            return this.Repository.All().Select(r => new RecepieLightModel()
                    {
                        Id = r.Id,
                        Name = r.Name,
                        Date = r.Date,
                        PictureUrl = r.PictureUrl,
                        Nickname = r.UserInfo.Nickname
                    }).OrderBy(r => r.Date);
        }

        // GET api/recepies/5
        public RecepieModel Get(int id)
        {
            var recepie = this.Repository.All().FirstOrDefault(u => u.Id == id);

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
                return result;
            }
            else
	        {
                //Should return HTML Responce
                throw new NotImplementedException();
	        }
        }

        // POST api/recepies
        public void Post([FromBody]Recepie value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
        }

        // PUT api/recepies/5
        public void Put(int id, [FromBody]Recepie value)
        {
            //Should only edit the passed values. Should not tuch any other values
            throw new NotImplementedException();
        }

        // DELETE api/recepies/5
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
        
        public IRepository<Recepie> Repository
        {
            get 
            {
                if (repository == null)
                {
                    repository = new Repository<Recepie>();
                }
                return repository;
            }
        }
    }
}
