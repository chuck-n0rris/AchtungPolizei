using System;
using System.Collections.Concurrent;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BlockingCollectionTest
    {
        [Test]
        public void Test()
        {
            var collection = new BlockingCollection<int>();
            int element;
            collection.TryTake(out element);
        }
    }
}