using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SimpleExtensions.Test {
    [TestClass]
    public class TestStringExtention {
        [TestMethod]
        public void ToInt32Test() {
            Assert.AreEqual("  123  ".ToInt32(), 123);
            Assert.AreEqual(123.ToString().ToInt32(), 123);
            Assert.AreEqual("asd".ToInt32(), 0);
            Assert.AreEqual("asd".ToInt32(123), 123);
            Assert.AreEqual("123.2".ToInt32(), 0);
            Assert.AreEqual("123,2".ToInt32(), 0);
        }

        [TestMethod]
        public void ToInt64Test() {
            Assert.AreEqual("  123  ".ToInt64(), 123);
            Assert.AreEqual((123 << 40).ToString().ToInt64(), (123 << 40));
            Assert.AreEqual("asd".ToInt64(), 0);
            Assert.AreEqual("asd".ToInt64(123), 123);
            Assert.AreEqual("123.2".ToInt64(), 0);
            Assert.AreEqual("123,2".ToInt64(), 0);
        }

        [TestMethod]
        public void ToDoubleTest() {
            Assert.AreEqual("  123  ".ToDouble(), 123);
            Assert.AreEqual((123 << 40 / 7).ToString().ToDouble(), (123 << 40 / 7));
            Assert.AreEqual("asd".ToDouble(), 0);
            Assert.AreEqual("asd".ToDouble(123), 123);
            Assert.AreEqual("123.2".ToDouble(), 123.2D, "123.2");
            Assert.AreEqual("123,2".ToDouble(), 123.2D, "123,2");
            Assert.AreEqual("1,7E-3".ToDouble(), 1.7E-3D);
            Assert.AreEqual("1.7E-3".ToDouble(), 1.7E-3D);

        }

        [TestMethod]
        public void ToFloatTest() {
            Assert.AreEqual("  123  ".ToFloat(), 123);
            Assert.AreEqual((123 << 10 / 7).ToString().ToFloat(), (123 << 10 / 7));
            Assert.AreEqual("asd".ToFloat(), 0);
            Assert.AreEqual("asd".ToFloat(123), 123);
            Assert.AreEqual("123.2".ToFloat(), 123.2F);
            Assert.AreEqual("123,2".ToFloat(), 123.2F);
            Assert.AreEqual("1,7E-3".ToFloat(), 1.7E-3F);
            Assert.AreEqual("1.7E-3".ToFloat(), 1.7E-3F);
        }

        [TestMethod]
        public void ToDecimalTest() {
            Assert.AreEqual("  123  ".ToDecimal(), 123);
            Assert.AreEqual((123 << 40 / 7).ToString().ToDecimal(), (123 << 40 / 7));
            Assert.AreEqual("asd".ToDecimal(), 0);
            Assert.AreEqual("asd".ToDecimal(123), 123);
            Assert.AreEqual("123.2".ToDecimal(), 123.2m);
            Assert.AreEqual("123,2".ToDecimal(), 123.2m);
        }

        public enum TestEnum {
            Value1 = 1,
            Value2 = 2,
            Value3 = 3,
        }

        [TestMethod]
        public void ToEnumTest() {
            Assert.AreEqual("  123  ".ToEnum<TestEnum>(), default (TestEnum));
            Assert.AreEqual((123 << 40 / 7).ToString().ToEnum<TestEnum>(), default (TestEnum));
            Assert.AreEqual("asd".ToEnum<TestEnum>(), default (TestEnum));
            Assert.AreEqual("Value1".ToEnum<TestEnum>(), TestEnum.Value1);
            Assert.AreEqual("  vaLUe2  ".ToEnum<TestEnum>(), TestEnum.Value2);
            Assert.AreEqual("  vaLUe2  ".ToEnum<TestEnum>(ignoreCase: false), default (TestEnum));
            Assert.AreEqual("asd".ToEnum(TestEnum.Value3), TestEnum.Value3);
            Assert.AreEqual("123.2".ToEnum(TestEnum.Value3), TestEnum.Value3);
            Assert.IsTrue("123,2".ToEnum<TestEnum>() == 0);
        }

        [TestMethod]
        public void ToGuidTest() {
            var id = Guid.NewGuid();
            Assert.AreEqual(id.ToString().ToGuid(), id);
            Assert.AreEqual(("   " + id.ToString().Replace("-", "").ToUpper() + "     ").ToGuid(), id);
            Assert.AreEqual(id.ToString("X").ToGuid(), id);
            Assert.AreEqual(id.ToString("N").ToGuid(), id);
            Assert.AreEqual(id.ToString("D").ToGuid(), id);
            Assert.AreEqual(id.ToString("B").ToGuid(), id);
            Assert.AreEqual(id.ToString("P").ToGuid(), id);
            Assert.AreEqual("asd".ToGuid(), Guid.Empty);
        }

        [TestMethod]
        public void ToDateTimeTest() {
            var date = DateTime.UtcNow;
            Assert.AreEqual(date.Subtract(date.ToString().ToDateTime()).Seconds, 0);
            Assert.AreEqual("2017-2-1".ToDateTime(), new DateTime(2017, 2, 1));
            Assert.AreEqual(date.ToString("dd-MM-yyyy HH:mm:ss.fffffff").ToDateTime("dd-MM-yyyy HH:mm:ss.fffffff"), date);
        }

        [TestMethod]
        public void ToTimeSpanTest() {
            var span = TimeSpan.FromMinutes(200);
            Assert.AreEqual(span.Subtract(span.ToString().ToTimeSpan()).Seconds, 0);
            Assert.AreEqual("3:10:20".ToTimeSpan(), new TimeSpan(3,10,20));            
            Assert.AreEqual(span.ToString(@"mm\:hh\:ss").ToTimeSpan(@"mm\:hh\:ss"), span);
        }

        [TestMethod]
        public void ToByteArrayTest() {
            var str = @"My Test string with 5 words.";
            Assert.AreEqual(str.ToByteArray().ByteToString(), str);
            Assert.AreEqual(str.ToByteArrayUtf8().ByteToStringUtf8(), str);
            Assert.AreEqual(str.ToByteArray(System.Text.Encoding.ASCII).ByteToString(System.Text.Encoding.ASCII), str);
        }

        [TestMethod]
        public void EqualsWithEpsilonTest() {
            Assert.IsTrue(1.7E-3D.EqualsWithEpsilon(1.7E-3));
            Assert.IsTrue(1.7E-3F.EqualsWithEpsilon(1.7E-3F));
        }

        [TestMethod]
        public void ToBoolTest() {
            Assert.IsTrue("  123  ".ToBool());
            Assert.IsFalse("asd".ToBool());
            Assert.IsTrue("123.2".ToBool());
            Assert.IsTrue("123,2".ToBool());

            Assert.IsFalse("0".ToBool());
            Assert.IsFalse("-1".ToBool());
            Assert.IsTrue("1".ToBool());

            Assert.IsTrue("trUE".ToBool());
            Assert.IsTrue("Yes".ToBool());
            Assert.IsFalse("FALse".ToBool());
            Assert.IsFalse("NO".ToBool());
            Assert.IsFalse((null as string).ToBool());
        }
    }
}
