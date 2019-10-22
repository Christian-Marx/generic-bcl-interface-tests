// This file is licensed to you under the MIT license.
// See the LICENSE.TXT file in the project root for more information.

namespace GenericBclInterfaceTests.System.Collections.Generic
{
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Diagnostics.CodeAnalysis;
    using global::System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Shouldly;

    public class TestIEnumerableOfTFor<TBase, TEnumerable> where TEnumerable : IEnumerable<TBase>
    {
        protected internal Func<TEnumerable> CreateInstance { get; }
        protected internal Func<TBase, int, bool> ItemCheck { get; }

        public TestIEnumerableOfTFor([DisallowNull] Func<TEnumerable> createInstance,
                                     [DisallowNull] Func<TBase, int, bool> itemCheck)
        => (this.CreateInstance, this.ItemCheck) = (createInstance ?? throw new ArgumentNullException(nameof(createInstance)),
                                                    itemCheck ?? throw new ArgumentNullException(nameof(itemCheck)));

        [TestMethod]
        public void EnumeratorShouldReturnValuesOfEnumerable()
        {
            var testInstance = this.CreateInstance();

            testInstance
                .Select((item, index) =>
                {
                    var checkResult = this.ItemCheck(item, index);
                    checkResult.ShouldBeTrue(string.Format("Item no. {0} did not match.", index));
                    return checkResult;
                })
                .ShouldAllBe(t => t);
        }
    }
}