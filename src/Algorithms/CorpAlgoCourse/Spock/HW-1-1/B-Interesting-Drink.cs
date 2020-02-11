using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Interesting Drink
    /// http://codeforces.com/gym/268160/problem/B
    /// </summary>
    [Trait("Category", "Spock: Corporate Algorithmic Course: HW 1.1")]
    public class InterestingDrink
    {
        [Theory]
        [MemberData(nameof(InputData1))]
        public void Test1(int[] beecolaPricesPerShop, int[] moneyToSpendPerDay, int[] expected)
        {
            var result = CountNumberOfAffordedBars(beecolaPricesPerShop, moneyToSpendPerDay);
            Assert.Equal(expected, result);
        }

        public static IEnumerable<object[]> InputData1 => new TheoryData<int[], int[], int[]>
        {
            { new[] { 3, 10, 8, 6, 11 }, new[] { 1, 10, 3, 11 }, new[] { 0, 4, 1, 5 } },
            { new[] { 10, 20, 80, 60, 100 }, new[] { 5 }, new[] { 0 } },
            { new[] { 10, 20, 80, 60, 100 }, new[] { 500 }, new[] { 5 } },
        };

        
        private static int[] CountNumberOfAffordedBars(int[] beecolaPricesPerShop, int[] moneyToSpendPerDay)
        {
            var result = new int[moneyToSpendPerDay.Length];

            Array.Sort(beecolaPricesPerShop);

            for (int i = 0; i < moneyToSpendPerDay.Length; i++)
            {
                var currentDayAvailableMoney = moneyToSpendPerDay[i];
                int barNumber = BinarySearchEx(beecolaPricesPerShop, currentDayAvailableMoney, false);

                if (barNumber < 0)
                {
                    result[i] = ~barNumber; // Cannot afford any beecola
                }
                else
                {
                    result[i] = barNumber + 1;
                }
            }
            
            return result;
        }
        
        private static int BinarySearchEx(int[] sequence, int target, bool firstOccurence = true)
        {
            if (sequence.Length == 0)
            {
                return -1;
            }
            if (target < sequence[0])
            {
                return -1;
            }
            if (sequence[sequence.Length - 1] < target)
            {
                return ~sequence.Length;
            }
            
            int low = 0;
            int high = sequence.Length - 1;
            int resultIndex = -1;

            while (low <= high)
            {
                var mid = low + ((high - low) >> 1);
                if (sequence[mid] == target)
                {
                    resultIndex = mid;
                    if (firstOccurence)
                    {
                        high = mid - 1;
                    }
                    else
                    {
                        low = mid + 1;
                    }
                }
                else if (target < sequence[mid])
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }

            if (resultIndex == -1)
            {
                return ~low;
            }
            
            return resultIndex;
        }

        private static void Main1()
        {
            int n = int.Parse(Console.ReadLine());
            
            var bottlePricesPerShop = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse((string) str)).ToArray();
            int q = int.Parse(Console.ReadLine());
            var moneyPerDay = new int[q];

            for (int i = 0; i < q; i++)
            {
                moneyPerDay[i] = int.Parse(Console.ReadLine());
            }

            var numberOfAffordedBars = CountNumberOfAffordedBars(bottlePricesPerShop, moneyPerDay);

            for (int i = 0; i < numberOfAffordedBars.Length; i++)
            {
                Console.WriteLine(numberOfAffordedBars[i]);
            }
        }
    }
}