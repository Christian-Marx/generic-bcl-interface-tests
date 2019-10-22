// This file is licensed to you under the MIT license.
// See the LICENSE.TXT file in the project root for more information.

namespace GenericBclInterfaceTests.System
{
    using global::System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;
    using global::System.Linq;
    using global::System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides test cases for structs implementing the <see cref="IEquatable&lt;T&gt;" interface.
    /// </summary>
    /// <typeparam name="T">The type of the equatable.</typeparam>
    public class TestIEquatableOfTFor<T> where T : struct, IEquatable<T>
    {
        public Func<T> CreateInstance { get; }

        public Func<T, T> CreateEqualInstance { get; }

        public Func<T, T> CreateUnequalInstance { get; }

        /// <summary>
        /// Initializes an instance of the <see cref="TestIEquatableOfTFor" /> class.
        /// </summary>
        /// <param name="createInstance">
        /// Function creating an instance to be tested.
        /// </param>
        /// <param name="createEqualInstance">
        /// Function creating an instance equal to the specified instance, i. e. an instance
        /// that is equal to the instance provided in the first parameter when this function is invoked.
        /// </param>
        /// <param name="createUnequalInstance">
        /// Function creating an instance unequal to the specified instance, i. e. an instance
        /// that is not equal to the instance provided in the first parameter when this function is invoked.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if any of the provided functions are null.</exceptions>
        public TestIEquatableOfTFor([DisallowNull] Func<T> createInstance,
                                    [DisallowNull] Func<T, T> createEqualInstance,
                                    [DisallowNull] Func<T, T> createUnequalInstance)
        {
            this.CreateInstance = createInstance ?? throw new ArgumentNullException(nameof(createInstance));
            this.CreateEqualInstance = createEqualInstance ?? throw new ArgumentNullException(nameof(createEqualInstance));
            this.CreateUnequalInstance = createUnequalInstance ?? throw new ArgumentNullException(nameof(createUnequalInstance));
        }

        [TestMethod]
        [Description("Verifies that the Equals(T) method is implemented correctly.")]
        public void TestEquals()
        {
            var testInstance = this.CreateInstance();

            testInstance.Equals(this.CreateEqualInstance(testInstance)).ShouldBeTrue();
            testInstance.Equals(this.CreateUnequalInstance(testInstance)).ShouldBeFalse();

            // If you implement Equals(T), you should also override the base class implementation of Equals(Object):
            testInstance.Equals((object)this.CreateEqualInstance(testInstance)).ShouldBeTrue();
            testInstance.Equals((object)this.CreateUnequalInstance(testInstance)).ShouldBeFalse();
        }

        [TestMethod]
        [Description("Verifies that equal test instances have the identical hash codes.")]
        public void EqualInstancesShouldHaveIdenticalHashCodes()
        {
            var testInstance = this.CreateInstance();

            Enumerable.Range(0, 10)
                .Select((_, idx) => this.CreateEqualInstance(testInstance).GetHashCode() == testInstance.GetHashCode())
                .ShouldAllBe(e => e);
        }

        [TestMethod]
        [Description("Verifies that unequal test instances have the different hash codes.")]
        public void UnequalInstancesShouldHaveDifferentHashCodes()
        {
            var testInstance = this.CreateInstance();

            var lastInstance = this.CreateUnequalInstance(testInstance);

            Enumerable.Range(0, 10)
                .Select((_, idx) =>
                {
                    lastInstance = this.CreateUnequalInstance(lastInstance);
                    return lastInstance.GetHashCode();
                })
                .ShouldBeUnique();
        }
    }
}
