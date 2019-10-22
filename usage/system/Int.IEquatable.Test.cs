using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace AbstractTests.System
{
    [TestClass]
    public class Int_IEquatableTest : IEquatableTest<int>
    {
        private Random random = new Random(DateTime.Now.Millisecond);
        public override int CreateTestInstance()
        {
            return this.random.Next();
        }
        public override int CreateSameInstance(int from)
        {
            return from;
        }
        public override int CreateDifferentInstance(int from)
        {
            return unchecked(from + 1);
        }
    }
}