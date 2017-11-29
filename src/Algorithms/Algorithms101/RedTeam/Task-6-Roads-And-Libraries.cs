using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Trait("Category", "Red Team")]
    public class Task6RoadsAndLibraries
    {
        public static TheoryData<string[], string[]> GetInputData00()
        {
            var testData = new TheoryData<string[], string[]>();
            var input = File.ReadAllLines(@".\RedTeam\TestData\Task-6-Roads-And-Libraries-input00.txt");
            var output = File.ReadAllLines(@".\RedTeam\TestData\Task-6-Roads-And-Libraries-output00.txt");
            testData.Add(input, output);
            return testData;
        }

        public static TheoryData<string[], string[]> GetInputData03()
        {
            var testData = new TheoryData<string[], string[]>();
            var input = File.ReadAllLines(@".\RedTeam\TestData\Task-6-Roads-And-Libraries-input03.txt");
            var output = File.ReadAllLines(@".\RedTeam\TestData\Task-6-Roads-And-Libraries-output03.txt");
            testData.Add(input, output);
            return testData;
        }

        public static TheoryData<string[], string[]> GetInputData07()
        {
            var testData = new TheoryData<string[], string[]>();
            var input = File.ReadAllLines(@".\RedTeam\TestData\Task-6-Roads-And-Libraries-input07.txt");
            var output = File.ReadAllLines(@".\RedTeam\TestData\Task-6-Roads-And-Libraries-output07.txt");
            testData.Add(input, output);
            return testData;
        }

        [Theory]
        //[MemberData(nameof(GetInputData00))]
        //[MemberData(nameof(GetInputData03))]
        [MemberData(nameof(GetInputData07))]
        public void MinimumCostCalculationsTest(string[] input, string[] expected)
        {
            int inputLine = 0;
            int expectedLine = 0;
            int q = int.Parse(input[inputLine++]);

            for (int a0 = 0; a0 < q; a0++)
            {
                string[] tokens_n = input[inputLine++].Split(' ');
                int n = int.Parse(tokens_n[0]);
                int m = int.Parse(tokens_n[1]);
                long x = long.Parse(tokens_n[2]);
                long y = long.Parse(tokens_n[3]);

                var roadsBetweenCities = new HashSet<Road>(m);

                for (int a1 = 0; a1 < m; a1++)
                {
                    string[] tokens_city_1 = input[inputLine++].Split(' ');
                    int city_1 = int.Parse(tokens_city_1[0]);
                    int city_2 = int.Parse(tokens_city_1[1]);

                    roadsBetweenCities.Add(new Road(city_1 - 1, city_2 - 1));
                }

                long cost = CalculateMinimumRestorationCosts(n, x, y, roadsBetweenCities);
                long expectedCost = long.Parse(expected[expectedLine++]);
                Assert.Equal(expectedCost, cost);
            }
        }

        private static long CalculateMinimumRestorationCosts(int cities, long libraryCost, long roadCost, HashSet<Road> roadsBetweenCities)
        {
            if (libraryCost <= roadCost)
            {
                return cities * libraryCost;
            }

            var citiesWithLibraryAccess = new bool[cities];
            long sum = 0;

            for (int currentCity = 0; currentCity < cities; currentCity++)
            {
                if (!citiesWithLibraryAccess[currentCity])
                {
                    citiesWithLibraryAccess[currentCity] = true; // Adding a library here
                    sum += libraryCost;

                    for (int anotherCity = 0; anotherCity < cities; anotherCity++)
                    {
                        if (roadsBetweenCities.Contains(new Road(currentCity, anotherCity)))
                        {
                            citiesWithLibraryAccess[anotherCity] = true;
                            sum += roadCost;
                        }
                    }
                }
            }

            return sum;
        }

        private struct Road
        {
            private readonly int _city1;
            private readonly int _city2;

            public Road(int city1, int city2)
            {
                _city1 = city1;
                _city2 = city2;
            }

            public bool Equals(Road other)
            {
                return _city1 == other._city1 && _city2 == other._city2
                    || _city1 == other._city2 && _city2 == other._city1;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is Road && Equals((Road)obj);
            }

            public override int GetHashCode()
            {
                return _city1 ^ _city2;
            }
        }
    }
}