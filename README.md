# Azure Functions for Developers
This is the repository for the LinkedIn Learning course `Azure Functions for Developers`. The full course is available from [LinkedIn Learning][lil-course-url].

![course-name-alt-text][lil-thumbnail-url] 

In this course, learn how to use Azure Functions to develop applications. Instructor Rodrigo Díaz Concha covers the benefits of Azure Functions and their different hosting plans. Learn how to create and deploy function apps and how to develop, test, debug, and run event-driven code on your local computer, as well as in Visual Studio using .NET. Learn about triggers, bindings, and how to implement both in Azure Functions. The course begins with an introduction to the core concepts, including the benefits of Azure Functions, execution models, and the setup of the development environment.  After that, Rodrigo covers advanced topics such as dependency injection, middleware integration, and the creation of Function Apps using different methods, including ARM templates and Azure Bicep.

_See the readme file in the main branch for updated instructions and information._
## Instructions
This repository has branches for each of the videos in the course. You can use the branch pop up menu in github to switch to a specific branch and take a look at the course at that stage, or you can add `/tree/BRANCH_NAME` to the URL to go to the branch you want to access.

## Branches
The branches are structured to correspond to the videos in the course. The naming convention is `CHAPTER#_MOVIE#`. As an example, the branch named `02_03` corresponds to the second chapter and the third video in that chapter. 
Some branches will have a beginning and an end state. These are marked with the letters `b` for "beginning" and `e` for "end". The `b` branch contains the code as it is at the beginning of the movie. The `e` branch contains the code as it is at the end of the movie. The `main` branch holds the final state of the code when in the course.

When switching from one exercise files branch to the next after making changes to the files, you may get a message like this:

    error: Your local changes to the following files would be overwritten by checkout:        [files]
    Please commit your changes or stash them before you switch branches.
    Aborting

To resolve this issue:
	
    Add changes to git using this command: git add .
	Commit changes using this command: git commit -m "some message"

## Installing
1. To use these exercise files, you must have the following installed:
	- [Visual Studio 2022](https://visualstudio.com)
	- [.NET 8 SDK or above](https://dot.net)
	- [Azure Functions Core Tools](https://github.com/Azure/azure-functions-core-tools) (this will be automatically included when you select the Azure workload in Visual Studio.)
	- [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/install-azure-cli)
	- [Git](https://git-scm.com/downloads)
	- [Postman](https://www.postman.com/downloads/)
	- [Microsoft Azure Storage Explorer](https://azure.microsoft.com/products/storage/storage-explorer)
	- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms)
2. You can install all the pre-requisites by using a package manager such as [Chocolatey](https://chocolatey.org/install).
3. Clone this repository into your local machine using the terminal (Mac), CMD (Windows), or a GUI tool like SourceTree.


[0]: # (Replace these placeholder URLs with actual course URLs)

[lil-course-url]: https://www.linkedin.com/learning/azure-functions-for-developers-24637001
[lil-thumbnail-url]: https://media.licdn.com/dms/image/v2/D4E0DAQF_liEP_rO4ow/learning-public-crop_675_1200/learning-public-crop_675_1200/0/1723500158341?e=2147483647&v=beta&t=Df6VJgFb2R87niMYKqGTrXh610VPTR_b5caQkBOJjX0