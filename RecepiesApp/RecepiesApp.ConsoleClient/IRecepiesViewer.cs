using System.Collections.Generic;

namespace RecepiesApp.ConsoleClient
{
    public interface IRecepiesViewer
    {
        IEnumerable<string> ViewRecepiesPage(int pageNumber, int recepiesOnPage = 10);
        
        IEnumerable<string> ViewRecepieComments(int recepieId);
        
        IEnumerable<string> ViewRecepiePhases(int recepieId);

        IEnumerable<string> ViewRecepieTags(int recepieId);
        
        IEnumerable<string> ViewTagRecepies(string tagName);

        IEnumerable<string> ViewTagsPage(int pageNumber, int recepiesOnPage = 10);
        
        IEnumerable<string> ViewUsersPage(int pageNumber, int recepiesOnPage = 10);

        IEnumerable<string> ViewUserComments(int userId);

        IEnumerable<string> ViewUsersFavouriteRecepies(int userId);
    }
}