namespace RecepiesApp.Data.Generators
{
    using System.Data.Entity;

    using FakeO;

    using RecepiesApp.Data.Repository;

    internal abstract class DataGenerator : IDataGenerator
    {
        protected const string GeneratingStringFormat = "Generating {0} {1}";
        protected const string GeneratedStringFormat = "Generated {0} {1} so far ...";
        protected const string GeneratedAllStringFormat = "Generated {0} {1} added in database";

        protected RDG rdg;
        protected FakeCreator fakeCreator;
        protected int count;

        public DataGenerator()
        {
            this.fakeCreator = new FakeCreator();
            this.rdg = RDG.Instance;
        }

        public virtual void Generate(int numberOfObjects)
        {
            this.count = numberOfObjects;
        }
    }
}
