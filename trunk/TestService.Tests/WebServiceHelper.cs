using System.IO;
using System.Web;
using System.Web.SessionState;

namespace TestService.Tests
{
    public class WebServiceHelper
    {
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
}