Drone Project
Author: Jose Enrique Alvarez Iglesias

**Requirements**
- Install Net Core 6.0 (https://dotnet.microsoft.com/es-es/download/dotnet/6.0)
- Visual Code for edition. 

**Build**
- Open a terminal and select the root directory of the project. (DroneProject)
- Run the instruction: dotnet run
- This build in bin\Debug\net6.0 the executable and run it.

**Run**
- Run DronesProject.exe in bin\Debug\net6.0.

**Test**
- Open http://localhost:[Port]/swagger in a browser to test and check the documentation generate by Swagger 6.1.4 package.
 


 The program was created with ASP.Net Core version 6.0.

**NUnit test**
 It was not possible to include a NUnit test because some package are not accesible:
 For example: "Microsoft.TestPlatform.TestHost.17.1.0" from "https://api.nuget.org/v3-flatcontainer/microsoft.testplatform.testhost/17.1.0
  /microsoft.testplatform.testhost.17.1.0.nupkg".

  Note:
  - The console display the battery level each 20 seconds.




