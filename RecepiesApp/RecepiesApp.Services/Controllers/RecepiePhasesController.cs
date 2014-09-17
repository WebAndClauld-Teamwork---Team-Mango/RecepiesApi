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
    public class RecepiePhasesController  : ApiController, IRepositoryHandler<RecepiePhase>, IController
    {
        public RecepiePhasesController() 
        {
            if (repository == null)
            {
                repository = new Repository<RecepiePhase>();
            }
        }

        private static IRepository<RecepiePhase> repository;
        
        [HttpGet]
        public HttpResponseMessage All()
        {
            var results = this.Repository.All().Select(RecepiePhaseModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet]
        public HttpResponseMessage ByRecepie(int recepieId)
        {
            var results = this.Repository.All().Where(p => p.RecepieId == recepieId).Select(RecepiePhaseModel.FromDbModel);
            return Request.CreateResponse(HttpStatusCode.OK, results);
        }

        [HttpGet]
        public HttpResponseMessage Select(int id)
        {
            var phase = this.Repository.All().Select(RecepiePhaseModel.FromDbModel).FirstOrDefault(t => t.Id == id);
            
            return Request.CreateResponse(HttpStatusCode.OK, phase);
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]RecepiePhase value)
        {
            this.Repository.Add(value);
            this.Repository.SaveChanges();
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        public HttpResponseMessage Edit(int id, [FromBody]RecepiePhaseModel value)
        {
            var item = this.Repository.All().FirstOrDefault(u => u.Id == id);
            if (item != null)
            {
                item.IsImportnt = value.IsImportnt;
                item.Minutes = value.Minutes;
                item.Name = value.Name;
                item.NumberOfPhase = value.NumberOfPhase;

                this.Repository.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The phase was not found");
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
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "The phase was not found");
            }
        }

        public Data.Repository.IRepository<RecepiePhase> Repository
        {
            get 
            {
                return repository;
            }
        }
    }
}
