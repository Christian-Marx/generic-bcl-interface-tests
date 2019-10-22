// This file is licensed to you under the MIT license.
// See the LICENSE.TXT file in the project root for more information.

namespace GenericBclInterfaceTests.System
{
    using global::System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestIEquatableOfTForInt : TestIEquatableOfTFor<int>
    {
        private static readonly Random RNG = new Random(DateTime.Now.Millisecond);
        public TestIEquatableOfTForInt() : base(() => RNG.Next(),
                                                other => other,
                                                other => unchecked(other + 1))
        {
        }
    }
}