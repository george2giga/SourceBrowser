SourceBrowser.Generator
=======================

C# in. HTML out.

Base project demo: [sourcebrowser.io/Browse/CodeConnect/SourceBrowser/](http://sourcebrowser.io/Browse/CodeConnect/SourceBrowser/)

_What is this fork about?_

This is a "generic" version of the SourceBrowser project, main changes:
*   No Github calls to retrieve User and Repo data (forks, stars etc).
*   C# avatar image is used for users/organizations.
*   Supporting Git credentials for private repositories.
*   Works with different Git servers (tested with GitHub and Atlassian Stash).
*   Navigation fixed, now working with virtual directories too.

_What is it?_

SourceBrowser.Generator takes C# solutions and creates HTML files with links between the methods, properties and types.

_Why?_

It's much easier to browse open source and online projects when you can traverse the project easily. GitHub is great, but it's a huge pain in the ass to navigate projects on here.

_What languages do you support?_ 

C# and VB .Net, but we'd like to support others.

_I found a bug. How do I tell you?_

Post it on our [Issues](https://github.com/CodeConnect/SourceBrowser.Generator/issues) page.

_I'd like a new feature. How do I tell you?_

Post it on our [Uservoice](http://sourcebrowser.uservoice.com).
