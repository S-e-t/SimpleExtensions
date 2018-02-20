using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SimpleExtensions.Test {
    [TestClass]
    public class TestIDictionaryExtensions {
        [TestMethod]
        public void DictionaryExtensionsTest() {
            var dict = new Dictionary<int, string>() { { 1, "key1" }, { 2, "key2" } }
                .AddIfNew(2, "keyNew2").AddIfNew(4, "key4");

            Assert.AreEqual(dict.TryGetValue(1), "key1");

            Assert.AreNotEqual(dict.TryGetValue(2), "keyNew2");
            Assert.AreEqual(dict.TryGetValue(2), "key2");

            Assert.AreNotEqual(dict.TryGetValue(3), "key3");
            Assert.AreEqual(dict.TryGetValue(3, "key3"), "key3");

            Assert.AreEqual(dict.TryGetValue(4), "key4");
        }        
    }
}
