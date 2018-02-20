using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;

namespace SimpleExtensions.Test {
    [TestClass]
    public class TestDataTableExtensions {
        [TestMethod]
        public void DataTableExtensionsTest() {
            var dt = new DataTable()
                .ColumnAdd<int>("id")
                .ColumnAdd<string>("Name")
                .Fill(new[] {
                    new { Id = 1, Name = "Name1" },
                    new { Id = 2, Name = "Name2" },
                    new { Id = 3, Name = "Name3" }
                }, i => new object[] { i.Id, i.Name });

            Assert.AreEqual(dt.Columns.Count, 2);
            Assert.AreEqual(dt.Columns["id"].DataType, typeof(int));
            Assert.AreEqual(dt.Columns["Name"].DataType, typeof(string));
            Assert.AreEqual(dt.Rows.Count, 3);
            Assert.AreEqual(dt.Rows[0]["id"], 1);
            Assert.AreEqual(dt.Rows[0]["Name"], "Name1");

            dt = new DataTable().ColumnAdd<int>("id").Fill(new[] { 1, 1, 1, 1 });

            Assert.AreEqual(dt.Columns.Count, 1);
            Assert.AreEqual(dt.Columns["id"].DataType, typeof(int));
            Assert.AreEqual(dt.Rows.Count, 4);
            Assert.AreEqual(dt.Rows[0]["id"], 1);
        }
    }
}
