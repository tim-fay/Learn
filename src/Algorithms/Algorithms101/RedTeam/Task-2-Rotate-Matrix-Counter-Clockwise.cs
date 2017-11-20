using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace Algorithms101.RedTeam
{
    [Trait("Category", "Red Team")]
    public class Task2RotateMatrixCounterClockwise
    {
        private const int MinLength = 2;
        private const int MaxLength = 300;
        private const int MinRotateSteps = 1;
        private const int MaxRotateSteps = 1_000_000_000;

        [Fact]
        public void InputParametersTest()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength - 1,MinLength], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength,MinLength - 1], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MaxLength + 1,MaxLength], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MaxLength, MaxLength + 1], MinRotateSteps));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength, MinLength], MinRotateSteps - 1));
            Assert.Throws<ArgumentOutOfRangeException>(() => Rotate(new int[MinLength, MinLength], MaxRotateSteps + 1));
        }

        [Fact]
        public void SpecializedSpanTest()
        {
            var array = new[,]
            {
                { 1, 2, 3, 4 },
                { 5, 6, 7, 8 },
                { 9, 10, 11, 12 },
                { 13, 14, 15, 16 }
            };

            var span = new TwoDimensionalArrayOuterFrameSpan<int>(array, 0, 0, 4, 4);

            Assert.Equal(array[0, 0], span[0]);
            Assert.Equal(array[0, 1], span[11]);
        }

        public static IEnumerable<object[]> TestInputArray1() => new TheoryData<int[,], int, int[,]>
        {
            {
                new[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                },
                2,
                new[,]
                {
                    { 4, 3 },
                    { 2, 1 }
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
                1,
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
        //[MemberData(nameof(TestInputArray1))]
        [MemberData(nameof(TestInputArray2))]
        public void RegularInputTest(int[,] input, int rotate, int[,] expected)
        {
            Assert.Equal(expected, Rotate(input, rotate));
        }

        private int[,] Rotate(int[,] input, int rotate)
        {
            CheckInput(input, rotate);

            var minBoundary = Math.Min(input.GetLength(0), input.GetLength(1));
            var numberOfFrames = minBoundary / 2;

            for (int frame = 0; frame < numberOfFrames; frame++)
            {
                RotateSingleFrame(input, frame, frame, input.GetUpperBound(0) + 1 - frame, input.GetUpperBound(1) + 1 - frame, rotate);
            }
            RotateSingleFrame(input, 0, 0, input.GetLength(0), input.GetLength(1), rotate);
            return input;
        }

        private void RotateSingleFrame(int[,] array, int x, int y, int rows, int columns, int rotate)
        {
            int totalItemsCountToRotate = (rows + columns - 2) * 2;
            int rotationLength = rotate % totalItemsCountToRotate;

            if (rotationLength == 0)
            {
                return;
            }
            
            var span = new TwoDimensionalArrayOuterFrameSpan<int>(array, x, y, rows, columns);
            int[] buffer = new int[rotationLength];

            for (int i = 0; i < rotationLength; i++)
            {
                buffer[i] = span[i];
            }

            for (int i = rotationLength; i < span.Length; i++)
            {
                ref int item = ref span[i];
                item = span[i - rotationLength];
            }

            int j = 0;
            for (int i = span.Length - rotationLength; i < span.Length; i++)
            {
                ref int item = ref span[i];
                item = buffer[j];
                j++;
            }


            //int nextValue;
            //int currentValue = span[0];
            //int currentIndex = 0;
            //int nextIndex = 0;
            ////ref int val;



            //for (int i = 0; i < totalItemsCountToRotate; i++)
            //{
            //    nextIndex = (currentIndex + rotate) % totalItemsCountToRotate;
            //    ref int refToNextItem = ref span[nextIndex];
            //    nextValue = refToNextItem;
            //    refToNextItem = currentValue;
            //    currentIndex = nextIndex;
            //    currentValue = nextValue;
            //}
        }

        private static void CheckInput(int[,] input, int rotate)
        {
            if (input.Rank != 2
                || Math.Min(input.GetLength(0), input.GetLength(1)) < MinLength
                || Math.Min(input.GetLength(0), input.GetLength(1)) % 2 != 0
                || input.GetLength(0) > MaxLength
                || input.GetLength(1) > MaxLength)
            {
                throw new ArgumentOutOfRangeException(nameof(input));
            }

            if (rotate < MinRotateSteps || rotate > MaxRotateSteps)
            {
                throw new ArgumentOutOfRangeException(nameof(rotate));
            }
        }

        private struct TwoDimensionalArrayOuterFrameSpan<T>
        {
            private readonly T[,] _array;
            private readonly int _row;
            private readonly int _column;
            private readonly int _rowsCount;
            private readonly int _columnsCount;
            private readonly int _leftItemsCount;
            private readonly int _leftBottomItemsCount;
            private readonly int _leftBottomRightItemsCount;
            private readonly int _leftBottomRightTopItemsCount;


            public TwoDimensionalArrayOuterFrameSpan(T[,] array, int row, int column, int rowsCount, int columnsCount)
            {
                _array = array ?? throw new ArgumentNullException(nameof(array));
                _row = row;
                _column = column;
                _rowsCount = rowsCount;
                _columnsCount = columnsCount;

                _leftItemsCount = _rowsCount - 1;
                _leftBottomItemsCount = _leftItemsCount + _columnsCount - 1;
                _leftBottomRightItemsCount = _leftBottomItemsCount + _rowsCount - 1;
                _leftBottomRightTopItemsCount = _leftBottomRightItemsCount + _columnsCount - 1;
            }

            /// <summary>
            /// Returns a reference 
            /// </summary>
            /// <param name="index">A zero based index of the array.</param>
            /// <returns></returns>
            public ref T this[int index]
            {
                get
                {
                    if (index < _leftItemsCount)
                    {
                        return ref GetLeftSideValue(index);
                    }

                    int indexRemainder = index - _leftItemsCount;
                    if (index < _leftBottomItemsCount)
                    {
                        return ref GetBottomSideValue(indexRemainder);
                    }

                    indexRemainder = index - _leftBottomItemsCount;
                    if (index < _leftBottomRightItemsCount)
                    {
                        return ref GetRightSideValue(indexRemainder);
                    }

                    indexRemainder = index - _leftBottomRightItemsCount;
                    if (index < _leftBottomRightTopItemsCount)
                    {
                        return ref GetTopSideValue(indexRemainder);
                    }

                    throw new InvalidOperationException();
                }
            }

            public int Length => _leftBottomRightTopItemsCount;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetLeftSideValue(int index)
            {
                return ref _array[index + _row, _column];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetBottomSideValue(int index)
            {
                return ref _array[_row + _rowsCount - 1, _column + index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetRightSideValue(int index)
            {
                return ref _array[_row + _rowsCount - 1 - index, _column + _columnsCount - 1];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetTopSideValue(int index)
            {
                return ref _array[_row, _column + _columnsCount - 1 - index];
            }
        }
    }
}