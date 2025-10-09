using NUnit.Framework;
using System;
using Project.Core.Scripts.Domain;
using UniRx;


namespace Project.Core.Scripts.Tests.EditMode
{
    public sealed class UnitTest
    {
        [SetUp]
        public void SetUp()
        {
            
        }

        [Test]
        public void SetValue()
        {
            var actual = true;
            Assert.That(actual, Is.True);
        }
    }
}
