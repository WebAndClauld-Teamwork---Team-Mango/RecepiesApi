using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecepiesApp.ConsoleClient
{
    public class RecepiesDbContextViewer : IRecepiesViewer 
    {
        public RecepiesDbContextViewer(RecepiesApp.Data.RecepiesDbContext context)
        {
            this.Context = context;
        }

        public IEnumerable<string> ViewRecepiesPage(int pageNumber, int recepiesOnPage = 10)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewRecepieComments(int recepieId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewRecepiePhases(int recepieId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewRecepieTags(int recepieId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewTagRecepies(string tagName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewTagsPage(int pageNumber, int recepiesOnPage = 10)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewUsersPage(int pageNumber, int recepiesOnPage = 10)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewUserComments(int userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> ViewUsersFavouriteRecepies(int userId)
        {
            throw new NotImplementedException();
        }

        public Data.RecepiesDbContext Context { get; set; }
    }
}
