# Ideas
This file is used to store ideas for the project.

## Dependency Injection
I'd like to use some form of dependency injection to allow systems and components used by Formulate to be swapped out (e.g., the data store for forms).

Look into this for dependency injection: http://www.lightinject.net/

And reference this: https://our.umbraco.org/documentation/reference/using-ioc

I wonder if there are issues with multiple IoC containers. How is the Umbraco core currently handling this?
* Seems to be using something called a "resolver": https://our.umbraco.org/documentation/Reference/Plugins/resolvers
* This describes how to create your own: https://our.umbraco.org/documentation/Reference/Plugins/creating-resolvers
* Maybe this article can clarify the differences: https://www.simple-talk.com/dotnet/.net-framework/asp.net-mvc-resolve-or-inject-that%E2%80%99s-the-issue%E2%80%A6/
