namespace RecepiesApp.Data.Generators
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    using RecepiesApp.Models;

    internal class TagsGenerator : DataGenerator, IDataGenerator
    {
        private const string TagsString = "tags";
        private static readonly string[] Names = 
        {
            "Lunch",
            "Main",
            "Dip",
            "Sauce",
            "Banana Bread",
            "Dessert",
            "Chicken"
        };

        private IRecepiesDbContext data;

        public TagsGenerator(IRecepiesDbContext data)
            : base()
        {
            this.data = data;
        }

        public override void Generate(int numberOfObjects)
        {
            base.Generate(numberOfObjects);

            var allTags = this.data.Tags;
            var allTagNames = new HashSet<string>(allTags.Select(t => t.Name));

            Console.WriteLine(DataGenerator.GeneratingStringFormat, this.count, TagsString);

            int i = 0;
            while (this.count > i)
            {
                string uniqueName;
                do
                {
                    uniqueName = this.rdg.GenerateItem(Names) + this.rdg.GenerateNumber(0, 9999);
                }
                while (!allTagNames.Add(uniqueName));

                var newTag = new Tag
                {
                    Name = uniqueName
                };

                this.data.Tags.Add(newTag);

                if (i % 100 == 99)
                {
                    Console.WriteLine(DataGenerator.GeneratedStringFormat, i, TagsString);
                    this.data.SaveChanges();
                }

                i++;
            }

            this.data.SaveChanges();
            Console.WriteLine(DataGenerator.GeneratedAllStringFormat, this.count, TagsString);
        }
    }
}
