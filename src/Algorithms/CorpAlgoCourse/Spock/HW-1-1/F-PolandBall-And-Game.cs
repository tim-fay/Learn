using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: PolandBall and Game
    /// http://codeforces.com/gym/268160/problem/F
    /// </summary>
    [Trait("Category", "Spock: Corporate Algorithmic Course: HW 1.1")]
    public class PolandBallAndGame
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(string[] polandBallWords, string[] enemyBallWords, bool expectedPolandBallWin)
        {
            var identifiedSongs = CheckIfPolandBallWins(polandBallWords, enemyBallWords);
            Assert.Equal(expectedPolandBallWin, identifiedSongs);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<string[], string[], bool>
        {
            { new[] { "polandball", "is", "a", "cool", "character" }, new[] { "nope" }, true },
            { new[] { "kremowka", "wadowicka" }, new[] { "kremowka", "wiedenska" }, true },
            { new[] { "a", "b", "c" }, new[] { "a", "b" }, true },
            { new[] { "a", "b", "c" }, new[] { "a", "b", "c" }, true },
            { new[] { "a", "b", "c", "d" }, new[] { "a", "b", "c", "e" }, true },
            { new[] { "a", "b", }, new[] { "a", "b", }, false },
            { new[] { "a", "b", "c" }, new[] { "a", "b", "d" }, false },
            { new[] { "a", "b" }, new[] { "c" }, true },
            { new[] { "a" }, new[] { "a", "b" }, false },
            { new[] { "a", "b" }, new[] { "c", "d" }, false },
        };
        
        private static bool CheckIfPolandBallWins(string[] polandBallWords, string[] enemyBallWords)
        {
            if (polandBallWords.Length > enemyBallWords.Length)
            {
                return true;
            }

            if (polandBallWords.Length < enemyBallWords.Length)
            {
                return false;
            }

            // Both arrays are the same size
            var polandWordsSet = new HashSet<string>(polandBallWords);

            int sharedKnownWordCounter = 0;

            for (int i = 0; i < enemyBallWords.Length; i++)
            {
                if (polandWordsSet.Contains(enemyBallWords[i]))
                {
                    sharedKnownWordCounter++;
                }
            }

            if (sharedKnownWordCounter % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void Main1()
        {
            var n_m = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();
            int n = n_m[0];
            int m = n_m[1];

            var polandBallWords = new string[n];
            for (int i = 0; i < n; i++)
            {
                polandBallWords[i] = Console.ReadLine();
            }

            var enemyBallWords = new string[m];
            for (int i = 0; i < m; i++)
            {
                enemyBallWords[i] = Console.ReadLine();
            }

            bool isPolandBallWins = CheckIfPolandBallWins(polandBallWords, enemyBallWords);
            Console.WriteLine(isPolandBallWins ? "YES" : "NO");
        }
    }
}