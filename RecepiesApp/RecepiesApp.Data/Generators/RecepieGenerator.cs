namespace RecepiesApp.Data.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using RecepiesApp.Models;

    internal class RecepieGenerator : DataGenerator, IDataGenerator
    {
        private const int MaxTags = 10;
        private const int MaxFavourites = 5;
        private const string RecepiesString = "recepies";
        private const string DefaultPicUrl = "http://www.theafricachannel.co.uk/wp-content/uploads/2012/06/fruit-salad.jpg";
        private static readonly string[] Names = 
        {
            "Baked Chicken Nuggets",
            "Hot Pizza Dip",
            "Marinated Pork Tenderloin",
            "Tomato-Cream Sauce for Pasta",
            "Apple Cinnamon Oatmeal",
            "Banana Bread",
            "Fluffy Pancakes",
            "Chicken Parmesan"
        };

        private IRecepiesDbContext data;

        public RecepieGenerator(IRecepiesDbContext data)
            : base()
        {
            this.data = data;
        }

        public override void Generate(int numberOfObjects)
        {
            base.Generate(numberOfObjects);
            var allUserIds = this.data.UserInfos.Select(u => u.Id).ToArray();

            Console.WriteLine(DataGenerator.GeneratingStringFormat, this.count, RecepiesString);

            int i = 0;
            while (this.count > i)
            {
                var randomName = this.rdg.GenerateItem(Names) + " " + this.rdg.GenerateNumber(1, 99);
                var randomDescription = "Description: " + this.rdg.GenerateString(150, 1000);
                var randomDateTime = this.rdg.GenerateDateTime(DateTime.Now.AddYears(-2), DateTime.Now);
                var randomUserId = this.rdg.GenerateItem(allUserIds);

                var newRecepie = new Recepie
                {
                    Name = randomName,
                    Description = randomDescription,
                    PictureUrl = DefaultPicUrl,
                    Date = randomDateTime,
                    UserInfoId = randomUserId
                };

                this.AddPhasesToRecipe(newRecepie);
                this.AddTagsToRecipe(newRecepie);
                this.AddCommentsToRecipe(newRecepie);
                this.AddToFavourites(newRecepie);

                this.data.Recepies.Add(newRecepie);

                if (i % 100 == 99)
                {
                    Console.WriteLine(DataGenerator.GeneratedStringFormat, i, RecepiesString);
                    this.data.SaveChanges();
                }

                i++;
            }

            this.data.SaveChanges();
            Console.WriteLine(DataGenerator.GeneratedAllStringFormat, this.count, RecepiesString);
        }

        private void AddPhasesToRecipe(Recepie recepie)
        {
            var numberOfPhases = this.rdg.GenerateNumber(3, 20);

            for (int i = 1; i <= numberOfPhases; i++)
            {
                var randomName = this.rdg.GenerateString(20, 50);
                var randomMinutes = this.rdg.GenerateItem(Enumerable.Range(5, 26).Where(n => n % 5 == 0).ToArray());
                var randomIsImportant = this.rdg.GenerateNumber(1, 100) < 30;

                var newPhase = new RecepiePhase
                {
                    NumberOfPhase = i,
                    Name = randomName,
                    Minutes = randomMinutes,
                    IsImportnt = randomIsImportant
                };

                recepie.Phases.Add(newPhase);
            }
        }

        private void AddTagsToRecipe(Recepie recepie)
        {
            var allTags = this.data.Tags.ToArray();
            var allTagIds = allTags.Select(t => t.Id).ToArray();
            var tagsCount = allTagIds.Length;
            var addedTags = new HashSet<Tag>();
            var numberOfTags = this.rdg.GenerateNumber(0, Math.Min(tagsCount, MaxTags));

            for (int j = 0; j < numberOfTags; j++)
            {
                Tag uniqueTag;
                do
                {
                    uniqueTag = this.rdg.GenerateItem(allTags);
                }
                while (!addedTags.Add(uniqueTag));

                recepie.Tags.Add(uniqueTag);
            }
        }

        private void AddCommentsToRecipe(Recepie recepie)
        {
            var allUserIds = this.data.UserInfos.Select(u => u.Id).ToArray();
            var numberOfComments = this.rdg.GenerateNumber(1, 10);

            for (int i = 0; i < numberOfComments; i++)
            {
                var randomContent = this.rdg.GenerateString(5, 1000);
                var randomDateTime = this.rdg.GenerateDateTime(recepie.Date, DateTime.Now);
                var randomUserId = this.rdg.GenerateItem(allUserIds);

                var newComment = new RecepieComment
                {
                    Content = randomContent,
                    Date = randomDateTime,
                    UserInfoId = randomUserId
                };

                recepie.Comments.Add(newComment);
            }
        }

        private void AddToFavourites(Recepie recepie)
        {
            var allUserIds = this.data.UserInfos.Select(u => u.Id).ToArray();
            var usersCount = allUserIds.Length;
            var addedFavourites = new HashSet<int>();
            var numberOfFavourites = this.rdg.GenerateNumber(0, Math.Min(usersCount, MaxFavourites));

            for (int i = 0; i < numberOfFavourites; i++)
            {
                int randomUserId;
                do
                {
                    randomUserId = this.rdg.GenerateItem(allUserIds);
                }
                while (!addedFavourites.Add(randomUserId));

                var newFavourite = new UserFavouriteRecepie
                {
                    UserInfoId = randomUserId
                };

                recepie.UsersFavouritedThisRecepie.Add(newFavourite);
            }
        }
    }
}
