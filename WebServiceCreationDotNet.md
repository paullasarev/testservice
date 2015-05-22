## Создание Web-сервиса на .Net ##

Здесь описано как создать простой Web-сервис на .Net

Описан вариант под Windows, под linux нужно использовать [mod\_mono](http://www.mono-project.com/Mod_mono)

Кроме того, можно использовать написанный на C# сервер [XSP](http://www.mono-project.com/ASP.NET#ASP.NET_hosting_with_XSP)

## Установка Apache и mod\_aspdotnet ##

Apache устанавливается из любого подходящего диструбитива, например:
  * ttp://apache.rinet.ru/dist/httpd/binaries/win32/apache\_2.0.63-win32-x86-openssl-0.9.7m.msi

Кроме того, нужно установить модуль mod\_aspdotnet
  * ttp://surfnet.dl.sourceforge.net/sourceforge/mod-aspdotnet/mod\_aspdotnet-2.0.0.2006-setup.msi

После установки этих msi нужно добавить в файл конфигурации Apache (обычно "%ProgramFiles%\Apache Group\Apache2\conf\httpd.conf") загрузку модуля и описание каталогов приложений:

```
LoadModule aspdotnet_module modules/mod_aspdotnet.so

# Use the asp.net handler for all common ASP.NET file types
AddHandler asp.net asax ascx ashx asmx aspx axd config cs csproj \
                   licx rem resources resx soap vb vbproj vsdisco webinfo 

<IfModule mod_aspdotnet.cpp>

    # Mount the IBuySpy C# example application
    AspNetMount /ws1 "D:/Dotnet/Apache/ws1"

    # Map all requests for /StoreCSVS to the IBuySpy application files
    Alias /ws1 "D:/Dotnet/Apache/ws1"

    # Allow asp.net scripts to be executed in the IBuySpy example
    <Directory "D:/Dotnet/Apache/ws1">
        Options FollowSymlinks
        AspNet Files
        Order allow,deny
        Allow from all
        DirectoryIndex Default.htm Default.asmx
    </Directory>

    AspNetMount /TestService "D:/Dotnet/Apache/TestService"
    Alias /TestService "D:/Dotnet/Apache/TestService"
    <Directory "D:/Dotnet/Apache/TestService">
        Options FollowSymlinks
        AspNet Files
        Order allow,deny
        Allow from all
        DirectoryIndex Default.asmx
    </Directory>


    # For all virtual ASP.NET webs, we need the aspnet_client files 
    # to serve the client-side helper scripts.
    AliasMatch /aspnet_client/system_web/(\d+)_(\d+)_(\d+)_(\d+)/(.*) \
          "C:/Windows/Microsoft.NET/Framework/v$1.$2.$3/ASP.NETClientFiles/$4"
    <Directory \
          "C:/Windows/Microsoft.NET/Framework/v*/ASP.NETClientFiles">
        Options FollowSymlinks
        Order allow,deny
        Allow from all
    </Directory>

</IfModule>

```

## Создание проекта WebService ##

Создать проект можно в [[Studio](Visual.md)] или в [[Sharpdevelop](Sharpdevelop.md)] из соответствующего шаблона. В [[Studio](Visual.md)] шаблон есть только в версии, предназначенной для Web-разработки.

Пример проекта: [[Медиа:TestService.rar]]

Сам по себе код очень прост - достаточно унаследовать класс от '''System.Web.Services.WebService''' и пометить атрибутом '''WebMethod''' экспортируемые методы.

```

using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace TestService
{
	[WebService]
	public class Soap : System.Web.Services.WebService
	{
		/// <summary>
		/// Logs into the web service
		/// </summary>
		/// <param name="userName">The User Name to login in as</param>
		/// <param name="password">User's password</param>
		/// <returns>True on successful login.</returns>
		[WebMethod(EnableSession=true)]
		public bool Login(string userName, string password)
		{
			//NOTE: There are better ways of doing authentication. This is just illustrates Session usage.
			UserName = userName;
			return true;
		}
		
		/// <summary>
		/// Logs out of the Session.
		/// </summary>
		[WebMethod(EnableSession=true)]
		public void Logout()
		{    
			Context.Session.Abandon();
		}
		
		/// <summary>
		/// UserName of the logged in user.
		/// </summary>
		private string UserName {
			get {return (string)Context.Session["User"];}
			set {Context.Session["User"] = value;}
		}

		[WebMethod]
	      public String SayHelloWorld()
	      {
	           return "Hello World";
	      }
		
	}
}

```

## Публикация проекта ##

После компиляции проекта в сборку (например, TestService.dll), помещаем входящий в проект файл '''Default.asmx''' в каталог, заданный в конфигурации Apache (в нашем случае "D:\Dotnet\Apache\TestService\").

Например, файл Default.asmx для нашего проекта содержит
```
<%@ WebService Language="C#"  Class="TestService.Soap" %>
```

Можно задать имя сборки, если оно отличается он имени пространства имен:
```
<%@ WebService Language="C#"  Class="TestService.Soap,MyAssembly" %>
```

Скомпилированную сборку и все что к ней нужно кладем в тот же каталог в подкаталог bin ("D:\Dotnet\Apache\TestService\bin\").

Всё, теперь сервис доступен через Apache. Если открыть браузером соответствующий url (http://localhost/TestService) то ASP.NET рисует красивую страничку с описанием сервиса:

![http://testservice.googlecode.com/svn/wiki/TestService.png](http://testservice.googlecode.com/svn/wiki/TestService.png)

На этой страничке есть ссылки, соответствующие методам сервиса, на них автоматически формируются формы, которыми можно задать параметры сервиса и проверить результат.


По запросу http://localhost/TestService?wsdl сервер выдает xml-файл описания сервиса.

## Дополнительная информация ##
  * http://www.apache.org/httpd
  * http://sourceforge.net/projects/mod-aspdotnet
  * http://mod-aspdotnet.sourceforge.net/introduction.html
  * http://mod-aspdotnet.sourceforge.net/mod_aspdotnet.html
  * [Creating ASP.NET Web Services—Deploying a Web Service Directly to IIS](http://en.csharp-online.net/Creating_ASP.NET_Web_Services%E2%80%94Deploying_a_Web_Service_Directly_to_IIS)
  * [Unit Testing a Web Service](http://blogs.dovetailsoftware.com/blogs/kmiller/archive/2007/11/07/unit-testing-a-web-service.aspx)
  * [Using WebServer.WebDev For Unit Tests](http://haacked.com/archive/2006/12/12/Using_WebServer.WebDev_For_Unit_Tests.aspx)