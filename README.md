Hello all

Please read the following

========================
Manual Tests
========================
Stored in the folder: HotelBookings.doc
https://github.com/SammyDeuce44/HotelBookings/blob/master/HotelBookingSite/HotelBookings.doc

========================
Automated Tests
========================
To execute the tests using Visual Studio Code:
1. Download Visual Studio Code: https://code.visualstudio.com/
2. You may need .Net Core SDK 2.1: 
https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.403-windows-x64-installer
3. Get the repo: https://github.com/SammyDeuce44/HotelBookings
4. Install C# extension on Visual Studio
5. In Visual Studio Code open Terminal
6. In terminal: dotnet test
7. Go to the folder C:\...\bin\Debug\netcoreapp2.1\BDDfy.html


To execute the tests using Visual studio
1. Download Visual Studio - Community: https://visualstudio.microsoft.com/vs/community/
2. You may need .Net Core SDK 2.1: 
https://www.microsoft.com/net/download/thank-you/dotnet-sdk-2.1.403-windows-x64-installer
3. Clone the repo: https://github.com/SammyDeuce44/HotelBookings
4. Build the project
5. Execute the test
6. Go to the folder C:\...\bin\Debug\netcoreapp2.1\BDDfy.html


========================
Folder Structure
========================
The test features are stored in APIAcceptanceTest.Features folder
The test steps are stored in APIAcceptanceTest.Steps folder
The test logic are stored in APIAcceptanceTest.Framework folder

APIAcceptanceTest
> Features
> Framework
> Steps

========================
Tooling
========================
Nunit
Bddfy
HttpClient

========================
Design patterns
========================
Screenplay Pattern
Chain Commands
Parallel test execution


========================
NuGet Packages
========================
Microsoft.Extensions.Configuration.Json
Nunit
Nunit.ConsoleRunner
Nunit3TestAdapter
TestStack.BDDfy
Also update appSettings.json to CopyAlways

=========================
Editing file from GitHub
=========================
Download and extract file from GitHub
Where you see the README.md git init in Cmder
Add your changes
git add .
git commit -a -m "My latest change"
git remote add origin https://github.com/SammyDeuce44/HotelBookings.git
git push -u origin master

=========================
Create a new repository on the command line
=========================
echo "# ScreenPlay" >> README.md
git init
git add README.md
git commit -a -m "My latest change"
git remote add origin https://github.com/SammyDeuce44/ScreenPlay.git
git push -u origin master

=========================
Push an existing repository from the command line
=========================
git remote add origin https://github.com/SammyDeuce44/ScreenPlay.git
git push -u origin master
