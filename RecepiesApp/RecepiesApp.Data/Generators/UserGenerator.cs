namespace RecepiesApp.Data.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using RecepiesApp.Models;

    internal class UserGenerator : DataGenerator, IDataGenerator
    {
        private const string UsersString = "users";
        private const string DefaultPicUrl = "http://www.ask-the-crowd.com/users/avatars/default_avatar_female.png";
        private static readonly string[] Nicknames = 
        {
            "Pesho",
            "Penka",
            "Kulinatora",
            "MissHomemade",
            "FreshBaked",
            "Vegitarism",
            "ChickenOrTheEgg",
            "EpicMealTime",
            "SimpleCooking",
            "CookingOutdoors",
            "WillGiveYouDiarrhea",
            "GrandmasFavourites"
        };

        private IRecepiesDbContext data;

        public UserGenerator(IRecepiesDbContext data)
            : base()
        {
            this.data = data;
        }

        public override void Generate(int numberOfObjects)
        {
            base.Generate(numberOfObjects);
            var allNicknames = new HashSet<string>(this.data.UserInfos.Select(u => u.Nickname));

            Console.WriteLine(DataGenerator.GeneratingStringFormat, this.count, UsersString);

            int i = 0;
            while (this.count > i)
            {
                string uniqueName;
                do
                {
                    uniqueName = this.rdg.GenerateItem(Nicknames) + this.rdg.GenerateNumber(0, 9999);
                }
                while (!allNicknames.Add(uniqueName));

                var newUser = new UserInfo
                {
                    Nickname = uniqueName,
                    Description = "Description: " + this.rdg.GenerateString(0, 1000),
                    PictureUrl = DefaultPicUrl
                };

                this.data.UserInfos.Add(newUser);

                if (i % 100 == 99)
                {
                    Console.WriteLine(DataGenerator.GeneratedStringFormat, i, UsersString);
                    this.data.SaveChanges();
                }

                i++;
            }

            this.data.SaveChanges();
            Console.WriteLine(DataGenerator.GeneratedAllStringFormat, this.count, UsersString);
        }
    }
}
