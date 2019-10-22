// This file is licensed to you under the MIT license.
// See the LICENSE.TXT file in the project root for more information.

namespace GenericBclInterfaceTests.System
{
    using global::System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestIComparableOfTForInt : TestIComparableOfTFor<int>
    {
        private static readonly Random RNG = new Random(DateTime.Now.Millisecond);
        public TestIComparableOfTForInt() : base(() => {
                                                           var instance = RNG.Next();

                                                            if (instance == int.MinValue)
                                                            {
                                                                ++instance;
                                                            }
                                                            else if (instance == int.MaxValue)
                                                            {
                                                                --instance;
                                                            }

                                                            return instance;
                                                       },
                                                       other => other,
                                                       other => unchecked(other - 1),
                                                       other => unchecked(other + 1),
                                                       (lhs, rhs) => lhs > rhs,
                                                       (lhs, rhs) => lhs < rhs,
                                                       (lhs, rhs) => lhs >= rhs,
                                                       (lhs, rhs) => lhs <= rhs)
        {
        }
    }
}