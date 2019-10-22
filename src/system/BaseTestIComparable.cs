// This file is licensed to you under the MIT license.
// See the LICENSE.TXT file in the project root for more information.

namespace GenericBclInterfaceTests.System
{
    using global::System;
    using global::System.Diagnostics.CodeAnalysis;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    public abstract class BaseTestIComparable<T> where T : struct
    {
        protected internal Func<T> CreateInstance { get; }
        protected internal Func<T, T> CreateSameInstance { get; }
        protected internal Func<T, T> CreatePrecedingInstance { get; }
        protected internal Func<T, T> CreateFollowingInstance { get; }
        protected internal Func<T, T, bool> Op_GreaterThan { get; }
        protected internal Func<T, T, bool> Op_LessThan { get; }
        protected internal Func<T, T, bool> Op_GreaterThanOrEqual { get; }
        protected internal Func<T, T, bool> Op_LessThanOrEqual { get; }

        /// <summary>
        /// Initializes an instance of the <see cref="BaseTestIComparable" /> class.
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
        internal BaseTestIComparable([DisallowNull] Func<T> createInstance,
                                     [DisallowNull] Func<T, T> createSameInstance,
                                     [DisallowNull] Func<T, T> createPrecedingInstance,
                                     [DisallowNull] Func<T, T> createFollowingInstance,
                                     [DisallowNull] Func<T, T, bool> op_GreaterThan,
                                     [DisallowNull] Func<T, T, bool> op_LessThan,
                                     [DisallowNull] Func<T, T, bool> op_GreaterThanOrEqual,
                                     [DisallowNull] Func<T, T, bool> op_LessThanOrEqual)
        {
            this.CreateInstance = createInstance ?? throw new ArgumentNullException(nameof(createInstance));
            this.CreateSameInstance = createSameInstance ?? throw new ArgumentNullException(nameof(createSameInstance));
            this.CreatePrecedingInstance = createPrecedingInstance ?? throw new ArgumentNullException(nameof(createPrecedingInstance));
            this.CreateFollowingInstance = createFollowingInstance ?? throw new ArgumentNullException(nameof(createFollowingInstance));
            this.Op_GreaterThan = op_GreaterThan ?? throw new ArgumentNullException(nameof(op_GreaterThan));
            this.Op_LessThan = op_LessThan ?? throw new ArgumentNullException(nameof(op_LessThan));
            this.Op_GreaterThanOrEqual = op_GreaterThanOrEqual ?? throw new ArgumentNullException(nameof(op_GreaterThanOrEqual));
            this.Op_LessThanOrEqual = op_LessThanOrEqual ?? throw new ArgumentNullException(nameof(op_LessThanOrEqual));
        }

        [TestMethod]
        [Description("Verifies that the greater than operator (>) is implemented consistently.")]
        public void OperatorGreaterThanShouldBeConsistent()
        {
            var testInstance = this.CreateInstance();

            this.Op_GreaterThan(testInstance, this.CreateSameInstance(testInstance)).ShouldBeFalse();
            this.Op_GreaterThan(testInstance, this.CreatePrecedingInstance(testInstance)).ShouldBeTrue();
            this.Op_GreaterThan(testInstance, this.CreateFollowingInstance(testInstance)).ShouldBeFalse();
        }

        [TestMethod]
        [Description("Verifies that the less than operator (<) is implemented consistently.")]
        public void OperatorLessThanShouldBeConsistent()
        {
            var testInstance = this.CreateInstance();

            this.Op_LessThan(testInstance, this.CreateSameInstance(testInstance)).ShouldBeFalse();
            this.Op_LessThan(testInstance, this.CreatePrecedingInstance(testInstance)).ShouldBeFalse();
            this.Op_LessThan(testInstance, this.CreateFollowingInstance(testInstance)).ShouldBeTrue();
        }

        [TestMethod]
        [Description("Verifies that the greater than or equal operator (>=) is implemented consistently.")]
        public void OperatorGreaterThanOrEqualShouldBeConsistent()
        {
            var testInstance = this.CreateInstance();

            this.Op_GreaterThanOrEqual(testInstance, this.CreateSameInstance(testInstance)).ShouldBeTrue();
            this.Op_GreaterThanOrEqual(testInstance, this.CreatePrecedingInstance(testInstance)).ShouldBeTrue();
            this.Op_GreaterThanOrEqual(testInstance, this.CreateFollowingInstance(testInstance)).ShouldBeFalse();
        }

        [TestMethod]
        [Description("Verifies that the less than or equal operator (<=) is implemented consistently.")]
        public void OperatorLessThanOrEqualShouldBeConsistent()
        {
            var testInstance = this.CreateInstance();

            this.Op_LessThanOrEqual(testInstance, this.CreateSameInstance(testInstance)).ShouldBeTrue();
            this.Op_LessThanOrEqual(testInstance, this.CreatePrecedingInstance(testInstance)).ShouldBeFalse();
            this.Op_LessThanOrEqual(testInstance, this.CreateFollowingInstance(testInstance)).ShouldBeTrue();
        }
    }
}