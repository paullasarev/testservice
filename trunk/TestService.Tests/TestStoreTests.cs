using System;
using NUnit.Framework;

namespace TestService.Tests
{
    [TestFixture]
    public class TestStoreTests
    {
        //[SetUp]
        //public void Setup()
        //{
        //    WebServiceHelper.CreateHttpSessionTest();
        //}

        [Ignore]
        [Test]
        public void CreateTest1()
        {
            TestStore ds = CreateTest1DS();
            ds.WriteXml(@"..\..\Tests\test1.xml", System.Data.XmlWriteMode.IgnoreSchema);
        }

        private TestStore CreateTest1DS()
        {
            TestStore ds = new TestStore();
            Guid guid = Guid.NewGuid();
            TestStore.TestRow testRow = ds.Test.AddTestRow(guid, "test1");
            ds.TestRevisions.AddTestRevisionsRow(testRow, System.DateTime.Now, "test data", "none");
            return ds;
        }

        [Test]
        public void TestStoreXmlAdapter()
        {
            TestStore ds = new TestStore();
            Assert.AreEqual(0, ds.Test.Rows.Count);

            ITestStoreAdapter adapter = new TestStoreFakeAdapter(@"..\..\Tests\test1.xml");
            adapter.Fill(ds);
            Assert.AreEqual(1, ds.Test.Rows.Count);
        }

        [Test]
        public void TestStoreDSAdapter()
        {
            TestStore ds = new TestStore();
            Assert.AreEqual(0, ds.Test.Rows.Count);

            ITestStoreAdapter adapter = new TestStoreFakeAdapter(CreateTest1DS());
            adapter.Fill(ds);
            Assert.AreEqual(1, ds.Test.Rows.Count);
        }

    }
}
