namespace RecepiesApp.Data.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using RecepiesApp.Models;

    public class StartupGenerator
    {
        /// <summary>
        /// Adds new data to an exsisting database
        /// </summary>
        /// <param name="context">A context where to add newly generated data</param>
        public static void AddEntriesToDatabase(IRecepiesDbContext context)
        {
            // TODO: Add some data on every program start

            // 3 Users with random data and a default picture url
            new UserGenerator(context).Generate(3);

            // Minimum 3 Tags per recepie (Not needed to be new every time, but yet they should be a bit randomish)
            new TagsGenerator(context).Generate(10);

            // 1-2 Recepies per user with random data and a default picture url - the picture must be of mango fruit salad
            // Minimum 3 Phases per recepie - each between 5-30 minutes
            // Minimum 1 comment per Recepie, from an existing user
            // Between 1 and 5 users (use old users if needed) that have put this recepie in their favourite list
            new RecepieGenerator(context).Generate(5);
        }
    }
}
