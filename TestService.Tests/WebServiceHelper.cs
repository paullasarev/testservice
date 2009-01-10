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

    public class TestStoreXmlAdapter : ITestStoreAdapter
    {
        private readonly string filename;

        public TestStoreXmlAdapter(string filename)
        {
            this.filename = filename;
        }

        public void Fill(TestStore ds)
        {
            ds.ReadXml(filename, XmlReadMode.IgnoreSchema);
        }

        public void Update(TestStore ds)
        {
            throw new System.NotImplementedException();
        }
    }
}