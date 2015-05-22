**"Testservice"** is a SOAP web-service provides a repository for storing and retrieving functional data-driven tests and testing logs.

When we try to step into massive functional testing area, we meet some difficulties, such as:
  * Interoperability. We use large amount of software - .net C#, 1c, on windows and linux
  * Multiple human- and auto- testers work separately, but must merge results
  * Functional tests are (and should be) _data-driven_, so it is possible manage tests in an independent manner

That is the ground for this project.

Main project architecture principles are:
  * Work as a [web-service](http://en.wikipedia.org/wiki/Web_service)
  * Use underline SQL storage (mysql)
  * Be a [test-driven in development](http://en.wikipedia.org/wiki/Test-driven_development)
  * Use [migratordotnet](http://code.google.com/p/migratordotnet/) engine for database scheme managment
  * Use `"WebDev.WebHost.dll"` for unit testing web-servcie (see [Unit Testing a Web Service](http://blogs.dovetailsoftware.com/blogs/kmiller/archive/2007/11/07/unit-testing-a-web-service.aspx))