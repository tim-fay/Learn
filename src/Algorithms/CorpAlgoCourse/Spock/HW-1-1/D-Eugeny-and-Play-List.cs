using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CorpAlgoCourse.Spock
{
    /// <summary>
    /// Codeforces: Vanya and Lanterns
    /// http://codeforces.com/gym/268160/problem/D
    /// </summary>
    [Trait("Category", "Spock: Corporate Algorithmic Course: HW 1.1")]
    public class EugenyAndPlayList
    {
        [Theory]
        [MemberData(nameof(InputData))]
        public void Test(SongListItem[] songList, int[] likedMoments, int[] expectedIdentifiedSongs)
        {
            var identifiedSongs = IdentifySongNumbers(songList, likedMoments);
            Assert.Equal(expectedIdentifiedSongs, identifiedSongs);
        }

        public static IEnumerable<object[]> InputData => new TheoryData<SongListItem[], int[], int[]>
        {
            { new[] { new SongListItem(1, 2, 8) }, new[] { 1, 16 }, new[] { 1, 1 } },
            {
                new[]
                {
                    new SongListItem(1, 1, 2),
                    new SongListItem(2, 2, 1),
                    new SongListItem(3, 1, 1),
                    new SongListItem(4, 2, 2),
                },
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                new[] { 1, 1, 2, 2, 3, 4, 4, 4, 4 }
            },
        };

        private static int[] IdentifySongNumbers(SongListItem[] songList, int[] likedMoments)
        {
            int[] songChangeTimes = new int[songList.Length];
            int currentPlayListDuration = 0;

            for (int i = 0; i < songList.Length; i++)
            {
                currentPlayListDuration += songList[i].TotalDuration;
                songChangeTimes[i] = currentPlayListDuration;
            }

            int[] identifiedSongs = new int[likedMoments.Length];

            for (int i = 0; i < likedMoments.Length; i++)
            {
                var songChangeTime = Array.BinarySearch(songChangeTimes, likedMoments[i]);
                if (songChangeTime < 0)
                {
                    identifiedSongs[i] = ~songChangeTime + 1;
                }
                else
                {
                    identifiedSongs[i] = songChangeTime + 1;
                }
            }

            return identifiedSongs;
        }

        public struct SongListItem
        {
            public SongListItem(int number, int playCount, int duration)
            {
                Number = number;
                PlayCount = playCount;
                Duration = duration;
            }

            public int Number { get; }
            public int PlayCount { get; }
            public int Duration { get; }

            public int TotalDuration
            {
                get { return Duration * PlayCount; }
            }
        }

        private static void Main1()
        {
            var n_m = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();
            int n = n_m[0];

            SongListItem[] songList = new SongListItem[n];
            for (int i = 0; i < n; i++)
            {
                var c_t = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();
                int c = c_t[0];
                int t = c_t[1];
                songList[i] = new SongListItem(i + 1, c, t);
            }
            
            var likedMoments = Console.ReadLine().Split(new[] {' '}).Select(str => int.Parse(str)).ToArray();

            var identifiedSongs = IdentifySongNumbers(songList, likedMoments);

            foreach (var identifiedSong in identifiedSongs)
            {
                Console.WriteLine(identifiedSong);
            }
        }
    }
}