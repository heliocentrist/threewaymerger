using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Merger;

namespace MergerTest
{
    [TestClass]
    public class MergerTest
    {
        [TestMethod]
        public void TestDeleteAll()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                
            };

            List<string> right = new List<string>
            {
                
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> {  }, result);
        }

        [TestMethod]
        public void TestDeleteAllLeft()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "d"
            };

            List<string> right = new List<string>
            {
                "a", "b"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "d" }, result);
        }

        [TestMethod]
        public void TestEditFront()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c"
            };

            List<string> right = new List<string>
            {
                "d", "b", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "d", "b", "c" }, result);
        }

        [TestMethod]
        public void TestEditEnd()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c"
            };

            List<string> right = new List<string>
            {
                "a", "b", "d"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "b", "d" }, result);
        }

        [TestMethod]
        public void TestEditMiddle()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c"
            };

            List<string> right = new List<string>
            {
                "a", "d", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "d", "c" }, result);
        }

        [TestMethod]
        public void TestAppendFront()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c"
            };

            List<string> right = new List<string>
            {
                "d", "a", "b", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "d", "a", "b", "c" }, result);
        }

        [TestMethod]
        public void TestAppendEnd()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c", "d"
            };

            List<string> right = new List<string>
            {
                "a", "b", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "b", "c", "d" }, result);
        }

        [TestMethod]
        public void TestInsert()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "d", "b", "c"
            };

            List<string> right = new List<string>
            {
                "a", "b", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "d", "b", "c" }, result);
        }

        [TestMethod]
        public void TestDeleteFront()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c"
            };

            List<string> right = new List<string>
            {
                "b", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "b", "c" }, result);
        }

        [TestMethod]
        public void TestDeleteEnd()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c"
            };

            List<string> right = new List<string>
            {
                "a", "b"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "b" }, result);
        }

        [TestMethod]
        public void TestDeleteMiddle()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "c"
            };

            List<string> right = new List<string>
            {
                "a", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "c" }, result);
        }

        [TestMethod]
        public void ConflictingEditMiddle()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "d", "c"
            };

            List<string> right = new List<string>
            {
                "a", "e", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "conflict: d e", "c" }, result);
        }

        [TestMethod]
        public void ConflictingEditStart()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "d", "b", "c"
            };

            List<string> right = new List<string>
            {
                "e", "b", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "conflict: d e", "b", "c" }, result);
        }

        [TestMethod]
        public void ConflictingEditEnd()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "a", "b", "d"
            };

            List<string> right = new List<string>
            {
                "a", "b", "e"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a", "b", "conflict: d e" }, result);
        }

        [TestMethod]
        public void DeleteStartInsertEnd()
        {
            List<string> original = new List<string>
            {
                "a", "b", "c"
            };

            List<string> left = new List<string>
            {
                "b", "c"
            };

            List<string> right = new List<string>
            {
                "a", "b", "c", "e"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "b", "c", "e" }, result);
        }

        [TestMethod]
        public void AsymmetricConflict()
        {
            List<string> original = new List<string>
            {
                "b", "c"
            };

            List<string> left = new List<string>
            {
                "d", "c"
            };

            List<string> right = new List<string>
            {
                "p", "q", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "conflict: d p", "q", "c" }, result);
        }

        [TestMethod]
        public void BothDeleteOneInsert()
        {
            List<string> original = new List<string>
            {
                "a"
            };

            List<string> left = new List<string>
            {
                "d", "c"
            };

            List<string> right = new List<string>
            {
                "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "conflict: d c", "c" }, result);
        }

        [TestMethod]
        public void MultipleInsertsDeletes()
        {
            List<string> original = new List<string>
            {
                "a",      "b",           "c", "d",           "e"
            };

            List<string> left = new List<string>
            {
                "a", "m", "b", "l", "q", "c", "d", "z", "y", "e"
            };

            List<string> right = new List<string>
            {
                     "r", "b", "g",      "c",      "z", "y"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "conflict: m r", "b", "conflict: l g", "q", "c", "z", "y" }, result);
        }

        [TestMethod]
        public void MultipleInsertsDeletes2()
        {
            List<string> original = new List<string>
            {
                     "e" 
            };

            List<string> left = new List<string>
            {
                "z", "e"
            };

            List<string> right = new List<string>
            {
                "z"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "z" }, result);
        }

        [TestMethod]
        public void AsymmetricConflictInsertFront()
        {
            List<string> original = new List<string>
            {
                          "c"
            };

            List<string> left = new List<string>
            {
                "l", "q", "c"
            };

            List<string> right = new List<string>
            {
                "g",      "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "conflict: l g", "q", "c" }, result);
        }

        [TestMethod]
        public void RepeatingStrings()
        {
            List<string> original = new List<string>
            {
                "a", "b", "a", "c"
            };

            List<string> left = new List<string>
            {
                "b", "a", "d", "c"
            };

            List<string> right = new List<string>
            {
                "a", "b", "a", "c"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "b", "a", "d", "c" }, result);
        }

        [TestMethod]
        public void RidiculouslyRepeatingStrings()
        {
            List<string> original = new List<string>
            {
                "a", "a", "a", "a"
            };

            List<string> left = new List<string>
            {
                "a", "a", "a"
            };

            List<string> right = new List<string>
            {
                "a"
            };

            var result = ThreeWayMerger.Merge(original, left, right);

            CollectionAssert.AreEqual(new List<string> { "a" }, result);
        }
    }
}
