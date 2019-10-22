using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace AbstractTests.System
{
    [TestClass]
    public abstract class IEquatableTest<T> where T: struct, IEquatable<T>
    {
        public abstract T CreateTestInstance();
        public abstract T CreateSameInstance(T from);
        public abstract T CreateDifferentInstance(T from);

        [TestMethod]
        public void Equals()
        {
            var testInstance = this.CreateTestInstance();

            testInstance.Equals(this.CreateSameInstance(testInstance)).ShouldBeTrue();
            testInstance.Equals(this.CreateDifferentInstance(testInstance)).ShouldBeFalse();
            testInstance.Equals(null).ShouldBeFalse();
        }
    }
}
