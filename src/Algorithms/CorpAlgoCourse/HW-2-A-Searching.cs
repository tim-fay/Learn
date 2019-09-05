using System;
using Xunit;

namespace CorpAlgoCourse
{
    public class Searching
    {
        [Fact]
        public void Test1()
        {
        }

        private int[] Solve(int[] sequence, int[] queries)
        {
            Array.Sort(sequence);
            var result = new int[queries.Length];

            for (int i = 0; i < queries.Length; i++)
            {
                var query = queries[i];

                if (query == 1)
                {
                    result[i] = 0;
                    continue;
                }

                for (int j = 0; j < sequence.Length; j++)
                {
                    
                }
            }
        }
    }
}