using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecepiesApp.Data;

namespace RecepiesApp.ConsoleClient
{
    internal class StartPoint
    {
        private static void Main(string[] args)
        {
            //var db = RecepiesDbContext.MsSqlExpressInstance();
            var db = new RecepiesDbContext();
            db.Database.CreateIfNotExists();
            Console.WriteLine("Database initialized.");
        }
    }
}
