using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace SimpleExtensions.Test {
    [TestClass]
    public class TestIEnumerableExtensions {
        [TestMethod]
        public void ForEachTest() {
            var arr = new[] { 1, 2, 3, 4 };
            var res = 0;
            arr.ForEach(i => res += i);
            Assert.AreEqual(res, 10);
        }

        public class TestModel {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Value { get; set; }
            public IEnumerable<int> Ids { get; set; }
        }

        private IEnumerable<TestModel> GetTestModel() {
            return new TestModel[] {
                new TestModel{ Id =  1 , Name = "A" , Value = 12E-3D, Ids = new [] { 1, 1, 3 } },
                new TestModel{ Id =  2 , Name = "B" , Value = 12E-3D, Ids = new [] { 1, 3, 3 } },
                new TestModel{ Id =  3 , Name = "C" , Value = 13E-3D, Ids = new [] { 1, 2, 3 } },
                new TestModel{ Id =  4 , Name = "C" , Value = 14E-3D, Ids = new [] { 1, 1, 3, 7 } },
            };            
        }

        [TestMethod]
        public void ToDictionaryTryTest() {
            var arr = GetTestModel();
            var res = arr.ToDictionaryTry(i => i.Id);
            Assert.IsTrue(res.Count == 4);
            Assert.IsTrue(res[1].Id == 1);
            Assert.IsTrue(res[4].Id == 4);

            var res2 = arr.ToDictionaryTry(i => i.Id, v => v.Ids);
            Assert.IsTrue(res2.Count == 4);
            Assert.IsTrue(res2[1].Count() == 3);
            Assert.IsTrue(res2[4].Count() == 4);
        }

        [TestMethod]
        public void GroupByToDictionaryTest() {
            var arr = GetTestModel();
            var res = arr.GroupByToDictionary(i => i.Name);
            Assert.IsTrue(res.Count == 3);
            Assert.IsTrue(res["A"].Count() == 1);
            Assert.IsTrue(res["B"].Count() == 1);
            Assert.IsTrue(res["C"].Count() == 2);

            var res2 = arr.GroupByToDictionary(i => i.Name, v => v.Ids);
            Assert.IsTrue(res2.Count == 3);
            Assert.IsTrue(res2["A"].FirstOrDefault().Count() == 3);
            Assert.IsTrue(res2["C"].FirstOrDefault(i => i.Count() == 3).Any());
            Assert.IsTrue(res2["C"].FirstOrDefault(i => i.Count() == 4).Any());

            var res3 = arr.GroupByToDictionary(i => i.Name, v => v.Ids)
                .ToDictionaryTry(
                par => par.Key, 
                par => new HashSet<int>(
                    par.Value.Aggregate(
                        new int[0] as IEnumerable<int>, 
                        (item1, item2) => item1.Concat(item2))));

            Assert.IsTrue(res3.Count == 3);
            Assert.IsTrue(res3["A"].Count() == 2);
            Assert.IsTrue(res3["B"].Count() == 2);
            Assert.IsTrue(res3["C"].Count() == 4);
        }
    }
}
