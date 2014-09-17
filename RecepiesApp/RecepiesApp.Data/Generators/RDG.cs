namespace RecepiesApp.Data.Generators
{
    using System;

    public class RDG
    {
        private const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static readonly RDG Singleton = new RDG();

        private Random rand;

        private RDG()
        {
            this.rand = new Random();
        }

        public static RDG Instance
        {
            get { return Singleton; }
        }

        public int GenerateNumber(int min, int max)
        {
            return this.rand.Next(min, max + 1);
        }

        public string GenerateString(int min, int max)
        {
            int len = this.GenerateNumber(min, max);
            var randomString = new char[len];

            for (int i = 0; i < len; i++)
            {
                randomString[i] = Alpha[this.GenerateNumber(0, Alpha.Length - 1)];
            }

            return new string(randomString);
        }

        public DateTime GenerateDateTime(DateTime from, DateTime to)
        {
            var timeSpan = to - from;
            return from.AddMinutes(this.GenerateNumber(0, (int)timeSpan.TotalMinutes));
        }

        public T GenerateItem<T>(T[] items)
        {
            return items[this.GenerateNumber(0, items.Length - 1)];
        }
    }
}
