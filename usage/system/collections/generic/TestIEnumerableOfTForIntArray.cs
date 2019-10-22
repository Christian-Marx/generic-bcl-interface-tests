// This file is licensed to you under the MIT license.
// See the LICENSE.TXT file in the project root for more information.

namespace GenericBclInterfaceTests.System.Collections.Generic
{
    using global::System.Collections;
    using global::System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public class IntEnumerator : IEnumerator<int>
    {
        private readonly int[] values;
        private int index;

        public IntEnumerator(int[] values)
        {
            this.values = values;
            this.index = -1;
        }

        public int Current { get; private set; }

        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (++this.index >= this.values.Length)
            {
                return false;
            }
            else
            {
                // Set current box to next item in collection.
                this.Current = this.values[this.index];
            }

            return true;
        }

        public void Reset() => this.index = -1;
    }

    public class IntEnumerable : IEnumerable<int>
    {
        private readonly int[] values;

        public IntEnumerable(int[] values) => this.values = values;

        public IEnumerator<int> GetEnumerator() => new IntEnumerator(this.values);

        IEnumerator IEnumerable.GetEnumerator() => new IntEnumerator(this.values);
    }

    [TestClass]
    public class TestIEnumerableOfTForIntEnumerable : TestIEnumerableOfTFor<int, IntEnumerable>
    {
        private static readonly IntEnumerable IntEnumerable = new IntEnumerable(new int[] { 1, 2, 3 });

        public TestIEnumerableOfTForIntEnumerable() : base(() => IntEnumerable,
                                                           (item, index) => item == index + 1)
        {
        }
    }

    [TestClass]
    public class TestIEnumerableOfTForIntArray : TestIEnumerableOfTFor<int, int[]>
    {
        private static readonly int[] TestInstance = new int[] { 1, 2, 3, 4, 5 };

        public TestIEnumerableOfTForIntArray() : base(() => TestInstance,
                                                      (item, index) => item == TestInstance[index])
        {
        }
    }
}