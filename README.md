# MVC Navigation
![mvc Navigation logo](https://raw.githubusercontent.com/anth12/mvc-navigation/master/logo.png)

## Getting started
Install the [nuget package](https://www.nuget.org/packages/Mvc.Navigation/1.0.0) using the command:
`Install-Package Mvc.Navigation`

In your `Startup.cs` add the following lines
`services.AddNavigation();`
`app.UseNavigation();`

By default a tree builder will be registered that scans your controllers & auto-generates a navigation tree. To register a custom provider see [TODO](#)

ASP.NET core navigation (Breadcrumb, Sitemap & Navigation tree) provider.

*Logo: Navigation by Nhor from the Noun Project*
