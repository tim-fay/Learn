using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Sheldon
{
    public class WorldCup
    {
        [Theory]
        [MemberData(nameof(InputData1))]
        public void Test1(int[] queues, int expected)
        {
            var result = Solve(queues.Length, queues);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> InputData1 => new TheoryData<int[], int>
        {
            { new[] { 2, 3, 2, 0 }, 3 },
            { new[] { 10, 10 }, 1 },
            { new[] { 5, 2, 6, 5, 7, 4 }, 6 },
        };
        
        private static int Solve(int entranceNumber, int[] queues)
        {
            int quickestQueue = 0;
            int minRoundsToWait = int.MaxValue;

            for (int i = 0; i < queues.Length; i++)
            {
                var waitRounds = GetNumberOfRoundsToWait(queues[i] - i, queues.Length);
                if (waitRounds < minRoundsToWait)
                {
                    minRoundsToWait = waitRounds;
                    quickestQueue = i;
                }
            }
            
            return quickestQueue + 1;
        }

        private static int GetNumberOfRoundsToWait(int numberOfPeopleBeforeHim, int numberOfQueues)
        {
            if (numberOfPeopleBeforeHim <= 0)
            {
                return 0;
            }

            return numberOfPeopleBeforeHim % numberOfQueues == 0
                ? numberOfPeopleBeforeHim / numberOfQueues
                : numberOfPeopleBeforeHim / numberOfQueues + 1;
        }
        
        private static void Main1()
        {
            int n = int.Parse(Console.ReadLine());
            var queues = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse((string) str)).ToArray();
            var result = Solve(n, queues);

            Console.WriteLine(result);
        }
    }
}