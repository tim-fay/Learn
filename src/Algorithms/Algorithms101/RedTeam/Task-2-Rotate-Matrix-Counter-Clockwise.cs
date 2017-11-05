using System;
using System.Collections.Generic;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Collection("Red Team")]
    public class Task2RotateMatrixCounterClockwise
    {
        private const int MinLength = 2;
        private const int MaxLength = 300;
        private const int MinRotateSteps = 1;
        private const int MaxRotateSteps = 1_000_000_000;

        [Fact]
        [Trait("Red Team: Task 2", "Test input arguments")]
        public void InputParametersTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength - 1,MinLength], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength,MinLength - 1], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MaxLength + 1,MaxLength], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MaxLength, MaxLength + 1], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength, MinLength], MinRotateSteps - 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength, MinLength], MaxRotateSteps + 1));
        }

        public static IEnumerable<object[]> TestInputArray1() => new TheoryData<int[,], int, int[,]>
        {
            {
                new[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                },
                3,
                new[,]
                {
                    { 1, 1 },
                    { 1, 1 }
                }
            }
        };

        public static IEnumerable<object[]> TestInputArray2() => new TheoryData<int[,], int, int[,]>
        {
            {
                new[,]
                {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 },
                    { 9, 10, 11, 12 },
                    { 13, 14, 15, 16 }
                },
                3,
                new[,]
                {
                    {2, 3, 4, 8},
                    {1, 7, 11, 12},
                    {5, 6, 10, 16},
                    {9, 13, 14, 15}
                }
            }
        };

        [Theory]
        [MemberData(nameof(TestInputArray1))]
        [MemberData(nameof(TestInputArray2))]
        public void RegularInputTest(int[,] input, int rotate, int[,] expected)
        {
            Assert.Equal(expected, Rotate(input, rotate));
        }

        private int[,] Rotate(int[,] input, int rotate)
        {
            CheckInput(input, rotate);

            return RotateImpl(0, 0, input.GetLength(0), input.GetLength(1), input, rotate);
        }

        private int[,] RotateImpl(int x, int y, int rows, int columns, int[,] input, int rotate)
        {
            int fullRotation = (rows + columns - 2) * 2;
            int rotation = rotate % fullRotation;

            if (rotation == 0)
            {
                return input;
            }



            return input;
        }

        private static void CheckInput(int[,] input, int rotate)
        {
            if (input.Rank != 2
                || input.GetLength(0) < MinLength
                || input.GetLength(0) > MaxLength
                || input.GetLength(1) < MinLength
                || input.GetLength(1) > MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(input));
            }

            if (rotate < MinRotateSteps || rotate > MaxRotateSteps)
            {
                throw new ArgumentOutOfRangeException(nameof(rotate));
            }
        }
    }
}