// This file is licensed to you under the MIT license.
// See the LICENSE.TXT file in the project root for more information.

namespace GenericBclInterfaceTests.System
{
    using global::System;
    using global::System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    /// <summary>
    /// Provides test cases for structs implementing the <see cref="IComparable" interface.
    /// </summary>
    public class TestIComparableFor<T> : BaseTestIComparable<T> where T : struct, IComparable
    {
        /// <summary>
        /// Initializes an instance of the <see cref="TestIComparableFor" /> class.
        /// </summary>
        /// <param name="createInstance">
        /// Function creating an instance to be tested.
        /// </param>
        /// <param name="createSameInstance">
        /// Function creating an instance equal to the specified instance, i. e. an instance
        /// that occurs in the same position in the sort order as the instance provided in
        /// the first parameter when this function is invoked.
        /// </param>
        /// <param name="createPrecedingInstance">
        /// Function creating an instance preceding to the specified instance, i. e. an instance
        /// that precedes the instance provided in the first parameter in sort order when this
        /// function is invoked.
        /// </param>
        /// <param name="createFollowingInstance">
        /// Function creating an instance following to the specified instance, i. e. an instance
        /// that follows the instance provided in the first parameter in sort order when this
        /// function is invoked.
        /// </param>
        /// <param name="op_GreaterThan">
        /// Function returning the result of invoking the greater than operator (&gt;) on the first and
        /// second parameter passed to this function.
        /// </param>
        /// <param name="op_LessThan">
        /// Function returning the result of invoking the less than operator (&lt;) on the first and
        /// second parameter passed to this function.
        /// </param>
        /// <param name="op_GreaterThanOrEqual">
        /// Function returning the result of invoking the greater than or equal operator (&gt;=) on the first and
        /// second parameter passed to this function.
        /// </param>
        /// <param name="op_LessThanOrEqual">
        /// Function returning the result of invoking the less than or equal operator (&lt;=) on the first and
        /// second parameter passed to this function.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if any of the provided functions are null.</exceptions>
        public TestIComparableFor([DisallowNull] Func<T> createInstance,
                                  [DisallowNull] Func<T, T> createSameInstance,
                                  [DisallowNull] Func<T, T> createPrecedingInstance,
                                  [DisallowNull] Func<T, T> createFollowingInstance,
                                  [DisallowNull] Func<T, T, bool> op_GreaterThan,
                                  [DisallowNull] Func<T, T, bool> op_LessThan,
                                  [DisallowNull] Func<T, T, bool> op_GreaterThanOrEqual,
                                  [DisallowNull] Func<T, T, bool> op_LessThanOrEqual)
        : base(createInstance,
               createSameInstance,
               createPrecedingInstance,
               createFollowingInstance,
               op_GreaterThan,
               op_LessThan,
               op_GreaterThanOrEqual,
               op_LessThanOrEqual)
        {
        }

        [TestMethod]
        [Description("Verifies that the CompareTo(T) method is implemented correctly.")]
        public void CompareTo()
        {
            var testInstance = this.CreateInstance();

            testInstance.CompareTo(this.CreateSameInstance(testInstance)).ShouldBe(0);
            testInstance.CompareTo(this.CreatePrecedingInstance(testInstance)).ShouldBeGreaterThan(0);
            testInstance.CompareTo(this.CreateFollowingInstance(testInstance)).ShouldBeLessThan(0);
            testInstance.CompareTo(null).ShouldBeGreaterThan(0);
        }
    }
}
