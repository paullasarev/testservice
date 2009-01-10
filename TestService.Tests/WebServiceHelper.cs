using System;
using System.Data;
using System.IO;
using System.Web;
using System.Web.SessionState;

namespace TestService.Tests
{
    public class WebServiceHelper
    {
        // come from http://blogs.gotdotnet.ru/personal/shapovalov/
        public static void CreateHttpSessionTest()
        {
            HttpContext.Current = new HttpContext(new HttpRequest(string.Empty,
                                                  "http://localhost/",
                                                   string.Empty),
                                                  new HttpResponse(new StringWriter()));

            IHttpSessionState iSessSteate = new HttpSessionStateContainer(string.Empty,
                                                                          new SessionStateItemCollection(),
                                                                          new HttpStaticObjectsCollection(),
                                                                          20000,
                                                                          true,
                                                                          HttpCookieMode.UseCookies,
                                                                          SessionStateMode.Off,
                                                                          false);
            SessionStateUtility.AddHttpSessionStateToContext(
                                                             HttpContext.Current,
                                                             iSessSteate);
        }
        
    }

    public class TestStoreFakeAdapter : ITestStoreAdapter
    {
        private readonly TestStore ds;
        private readonly string filename;

        public TestStoreFakeAdapter(string filename)
        {
            this.filename = filename;
            ds = new TestStore();
            ds.ReadXml(filename, XmlReadMode.IgnoreSchema);
        }

        public TestStoreFakeAdapter(TestStore ds)
        {
            this.ds = ds;
        }

        public void Fill(TestStore tds)
        {
            tds.Merge(ds);
        }

        public void Update(TestStore tds)
        {
            throw new System.NotImplementedException();
        }
    }
}