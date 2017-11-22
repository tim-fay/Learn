using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                { 13, 14, 15, 16 },
                { 17, 18, 19, 20 }
            };

            var span = new TwoDimensionalArrayOuterFrameSpan<int>(array, 0, 0, 5, 4);

            // Upper left corner
            Assert.Equal(array[1, 0], span[13]);
            Assert.Equal(array[0, 0], span[0]);
            Assert.Equal(array[0, 1], span[1]);
            // Upper right corner
            Assert.Equal(array[0, 2], span[2]);
            Assert.Equal(array[0, 3], span[3]);
            Assert.Equal(array[1, 3], span[4]);
            // Bottom right corner
            Assert.Equal(array[3, 3], span[6]);
            Assert.Equal(array[4, 3], span[7]);
            Assert.Equal(array[4, 2], span[8]);

            Assert.Equal(array[4, 1], span[9]);
            Assert.Equal(array[4, 0], span[10]);
            Assert.Equal(array[3, 0], span[11]);


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

        public static IEnumerable<object[]> TestInputArray3() => new TheoryData<int[,], int, int[,]>
        {
            {
                new[,]
                {
                    { 1, 2, 3, 4 },
                    { 7, 8, 9, 10 },
                    { 13, 14, 15, 16 },
                    { 19, 20, 21, 22 },
                    { 25, 26, 27, 28 }
                },
                7,
                new[,]
                {
                    { 28, 27, 26, 25 },
                    { 22, 9, 15, 19 },
                    { 16, 8, 21, 13 },
                    { 10, 14, 20, 7 },
                    { 4, 3, 2, 1 }
                }
            }
        };

        public static IEnumerable<object[]> TestInputArray4() => new TheoryData<int[,], int, int[,]>
        {
            {
                new[,]
                {
                    { 9718805, 60013003, 5103628, 85388216, 21884498, 38021292, 73470430, 31785927 },
                    { 69999937, 71783860, 10329789, 96382322, 71055337, 30247265, 96087879, 93754371 },
                    { 79943507, 75398396, 38446081, 34699742, 1408833, 51189, 17741775, 53195748 },
                    { 79354991, 26629304, 86523163, 67042516, 54688734, 54630910, 6967117, 90198864 },
                    { 84146680, 27762534, 6331115, 5932542, 29446517, 15654690, 92837327, 91644840 },
                    { 58623600, 69622764, 2218936, 58592832, 49558405, 17112485, 38615864, 32720798 },
                    { 49469904, 5270000, 32589026, 56425665, 23544383, 90502426, 63729346, 35319547 },
                    { 20888810, 97945481, 85669747, 88915819, 96642353, 42430633, 47265349, 89653362 },
                    { 55349226, 10844931, 25289229, 90786953, 22590518, 54702481, 71197978, 50410021 },
                    { 9392211, 31297360, 27353496, 56239301, 7071172, 61983443, 86544343, 43779176 }
                },
                40,
                new[,]
                {
                    { 93754371, 53195748, 90198864, 91644840, 32720798, 35319547, 89653362, 50410021 },
                    { 31785927, 25289229, 10844931, 97945481, 5270000, 69622764, 27762534, 43779176 },
                    { 73470430, 90786953, 42430633, 96642353, 88915819, 85669747, 26629304, 86544343 },
                    { 38021292, 22590518, 90502426, 67042516, 54688734, 32589026, 75398396, 61983443 },
                    { 21884498, 54702481, 17112485, 5932542, 29446517, 2218936, 71783860, 7071172 },
                    { 85388216, 71197978, 15654690, 58592832, 49558405, 6331115, 10329789, 56239301 },
                    { 5103628, 47265349, 54630910, 56425665, 23544383, 86523163, 96382322, 27353496 },
                    { 60013003, 63729346, 51189, 1408833, 34699742, 38446081, 71055337, 31297360 },
                    { 9718805, 38615864, 92837327, 6967117, 17741775, 96087879, 30247265, 9392211 },
                    { 69999937, 79943507, 79354991, 84146680, 58623600, 49469904, 20888810, 55349226 }
                }
            }
        };

        [Theory]
        [MemberData(nameof(TestInputArray1))]
        [MemberData(nameof(TestInputArray2))]
        [MemberData(nameof(TestInputArray3))]
        [MemberData(nameof(TestInputArray4))]
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
                int rows = Math.Max(input.GetUpperBound(0) + 1 - frame * 2, MinLength);
                int columns = Math.Max(input.GetUpperBound(1) + 1 - frame * 2, MinLength);
                RotateSingleFrame(input, frame, frame, rows, columns, rotate);
            }
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

            for (int i = 0; i < span.Length - rotationLength; i++)
            {
                ref int item = ref span[i];
                item = span[i + rotationLength];
            }

            int lastShiftedIndex = span.Length - rotationLength; 
            for (int i = 0; i < rotationLength; i++)
            {
                ref int item = ref span[lastShiftedIndex + i];
                item = buffer[i];
            }
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
            private readonly int _verticalItemsCount;
            private readonly int _horizontalItemsCount;
            private readonly int _horizontalAndVerticalItemsCount;
            private readonly int _bothHorizontalAndVerticalItemsCount;
            private readonly int _allItemsCount;


            public TwoDimensionalArrayOuterFrameSpan(T[,] array, int row, int column, int rowsCount, int columnsCount)
            {
                _array = array ?? throw new ArgumentNullException(nameof(array));
                _row = row;
                _column = column;

                _verticalItemsCount = rowsCount - 1;
                _horizontalItemsCount = columnsCount - 1;
                _horizontalAndVerticalItemsCount = _horizontalItemsCount + _verticalItemsCount;
                _bothHorizontalAndVerticalItemsCount = _horizontalItemsCount * 2 + _verticalItemsCount;
                _allItemsCount = _horizontalAndVerticalItemsCount * 2;
            }

            public int Length => _allItemsCount;

            /// <summary>
            /// Returns a reference to an element from the frame of a matrix
            /// </summary>
            /// <param name="index">A zero based index of the array.</param>
            /// <returns></returns>
            public ref T this[int index] => ref GetClockwiseElementAt(index);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetClockwiseElementAt(int index)
            {
                if (index < _horizontalItemsCount)
                {
                    return ref GetTopSideValue(index);
                }

                if (index < _horizontalAndVerticalItemsCount)
                {
                    return ref GetRightSideValue(index - _horizontalItemsCount);
                }

                if (index < _bothHorizontalAndVerticalItemsCount)
                {
                    return ref GetBottomSideValue(index - _horizontalAndVerticalItemsCount);
                }

                if (index < _allItemsCount)
                {
                    return ref GetLeftSideValue(index - _bothHorizontalAndVerticalItemsCount);
                }

                throw new InvalidOperationException();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetTopSideValue(int index)
            {
                Debug.Assert(index <= _horizontalItemsCount);
                return ref _array[_row, _column + index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetRightSideValue(int index)
            {
                Debug.Assert(index <= _verticalItemsCount);
                return ref _array[_row + index, _column + _horizontalItemsCount];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetBottomSideValue(int index)
            {
                Debug.Assert(index <= _horizontalItemsCount);
                return ref _array[_row + _verticalItemsCount, _column + _horizontalItemsCount - index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetLeftSideValue(int index)
            {
                Debug.Assert(index <= _verticalItemsCount);
                return ref _array[_row + _verticalItemsCount - index, _column];
            }
        }
    }



    /*
     
    Block for Hacker Rank

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution {
    static void Main(string[] args) {
        string[] arrayParams = Console.ReadLine().Split(' ');
        int m = Convert.ToInt32(arrayParams[0]);
        int n = Convert.ToInt32(arrayParams[1]);
        int r = Convert.ToInt32(arrayParams[2]);
        
        int[,] array = new int[m, n];
        
        for (int i = 0; i < m; i++) {
            var arrayLine = Console.ReadLine().Split(' ');
            
            for (int j = 0; j < n; j++) {
                array[i, j] = Convert.ToInt32(arrayLine[j]);
            }
        }
        
        var rotator = new Task2RotateMatrixCounterClockwise();
        var output = rotator.Rotate(array, r);
        
        for (int i = 0; i < m; i++) {
            for (int j = 0; j < n; j++) {
                Console.Write(array[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    
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
                { 13, 14, 15, 16 },
                { 17, 18, 19, 20 }
            };

            var span = new TwoDimensionalArrayOuterFrameSpan<int>(array, 0, 0, 5, 4);

            // Upper left corner
            Assert.Equal(array[1, 0], span[13]);
            Assert.Equal(array[0, 0], span[0]);
            Assert.Equal(array[0, 1], span[1]);
            // Upper right corner
            Assert.Equal(array[0, 2], span[2]);
            Assert.Equal(array[0, 3], span[3]);
            Assert.Equal(array[1, 3], span[4]);
            // Bottom right corner
            Assert.Equal(array[3, 3], span[6]);
            Assert.Equal(array[4, 3], span[7]);
            Assert.Equal(array[4, 2], span[8]);

            Assert.Equal(array[4, 1], span[9]);
            Assert.Equal(array[4, 0], span[10]);
            Assert.Equal(array[3, 0], span[11]);


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

        public static IEnumerable<object[]> TestInputArray3() => new TheoryData<int[,], int, int[,]>
        {
            {
                new[,]
                {
                    { 1, 2, 3, 4 },
                    { 7, 8, 9, 10 },
                    { 13, 14, 15, 16 },
                    { 19, 20, 21, 22 },
                    { 25, 26, 27, 28 }
                },
                7,
                new[,]
                {
                    { 28, 27, 26, 25 },
                    { 22, 9, 15, 19 },
                    { 16, 8, 21, 13 },
                    { 10, 14, 20, 7 },
                    { 4, 3, 2, 1 }
                }
            }
        };

        [Theory]
        [MemberData(nameof(TestInputArray1))]
        [MemberData(nameof(TestInputArray2))]
        [MemberData(nameof(TestInputArray3))]
        public void RegularInputTest(int[,] input, int rotate, int[,] expected)
        {
            Assert.Equal(expected, Rotate(input, rotate));
        }

        public int[,] Rotate(int[,] input, int rotate)
        {
            CheckInput(input, rotate);

            var minBoundary = Math.Min(input.GetLength(0), input.GetLength(1));
            var numberOfFrames = minBoundary / 2;

            for (int frame = 0; frame < numberOfFrames; frame++)
            {
                int rows = Math.Max(input.GetUpperBound(0) + 1 - frame * 2, MinLength);
                int columns = Math.Max(input.GetUpperBound(1) + 1 - frame * 2, MinLength);
                RotateSingleFrame(input, frame, frame, rows, columns, rotate);
            }
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

            for (int i = 0; i < span.Length - rotationLength; i++)
            {
                ref int item = ref span[i];
                item = span[i + rotationLength];
            }

            int lastShiftedIndex = span.Length - rotationLength; 
            for (int i = 0; i < rotationLength; i++)
            {
                ref int item = ref span[lastShiftedIndex + i];
                item = buffer[i];
            }
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
            private readonly int _verticalItemsCount;
            private readonly int _horizontalItemsCount;
            private readonly int _horizontalAndVerticalItemsCount;
            private readonly int _bothHorizontalAndVerticalItemsCount;
            private readonly int _allItemsCount;


            public TwoDimensionalArrayOuterFrameSpan(T[,] array, int row, int column, int rowsCount, int columnsCount)
            {
                _array = array ?? throw new ArgumentNullException(nameof(array));
                _row = row;
                _column = column;

                _verticalItemsCount = rowsCount - 1;
                _horizontalItemsCount = columnsCount - 1;
                _horizontalAndVerticalItemsCount = _horizontalItemsCount + _verticalItemsCount;
                _bothHorizontalAndVerticalItemsCount = _horizontalItemsCount * 2 + _verticalItemsCount;
                _allItemsCount = _horizontalAndVerticalItemsCount * 2;
            }

            public int Length => _allItemsCount;

            /// <summary>
            /// Returns a reference to an element from the frame of a matrix
            /// </summary>
            /// <param name="index">A zero based index of the array.</param>
            /// <returns></returns>
            public ref T this[int index] => ref GetClockwiseElementAt(index);

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetClockwiseElementAt(int index)
            {
                if (index < _horizontalItemsCount)
                {
                    return ref GetTopSideValue(index);
                }

                if (index < _horizontalAndVerticalItemsCount)
                {
                    return ref GetRightSideValue(index - _horizontalItemsCount);
                }

                if (index < _bothHorizontalAndVerticalItemsCount)
                {
                    return ref GetBottomSideValue(index - _horizontalAndVerticalItemsCount);
                }

                if (index < _allItemsCount)
                {
                    return ref GetLeftSideValue(index - _bothHorizontalAndVerticalItemsCount);
                }

                throw new InvalidOperationException();
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetTopSideValue(int index)
            {
                Debug.Assert(index <= _horizontalItemsCount);
                return ref _array[_row, _column + index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetRightSideValue(int index)
            {
                Debug.Assert(index <= _verticalItemsCount);
                return ref _array[_row + index, _column + _horizontalItemsCount];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetBottomSideValue(int index)
            {
                Debug.Assert(index <= _horizontalItemsCount);
                return ref _array[_row + _verticalItemsCount, _column + _horizontalItemsCount - index];
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private ref T GetLeftSideValue(int index)
            {
                Debug.Assert(index <= _verticalItemsCount);
                return ref _array[_row + _verticalItemsCount - index, _column];
            }
        }
    }
    
    
}
     
     
     */

}