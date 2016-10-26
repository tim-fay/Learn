using System;
using System.Linq;

namespace BitManipulation
{
    public class UniqueIntegerInPairwiseArray
    {
        public void Run()
        {
            var testData = ConstructTestData();

            // Can be expressed with a LINQ Aggregate
            //var loneInt = testData.Aggregate(0, (accumulated, current) => accumulated ^ current);

            var loneInt = 0;
            foreach (var current in testData)
            {
                loneInt ^= current;
            }

            Console.WriteLine($"Lone integer: {loneInt}");
        }

        private int[] ConstructTestData()
        {
            return new[] { 10, 10, 12, 8, 12, 7, 8, 2, 4, 2, 4, 3, 3, 100, 200, 700, 100, 700, 200 };
        }
    }
}