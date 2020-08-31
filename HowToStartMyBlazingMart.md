# Howto

1 Create new solution
`dotnet new sln -n BlazorMart`

2 Add BlazorWasm app to solution
`dotnet new blazorwasm -o BlazorMart.Client`
`dotnet sln add BlazorMart.Client/BlazorMart.Client.csproj`

3 Add BlazorGrpc app to solution
`dotnet new grpc -o BlazorMart.Server`
`dotnet sln add BlazorMart.Server/BlazorMart.Server.csproj`

4 Add reference to BlazorMart.Server.csproj file
`dotnet-grpc add-file Protos/inventory.proto`

5 Add required Grpc Packages to BlazorMart.Client project
`dotnet add package grpc.net.client`
`dotnet add package Grpc.Net.Client.Web`
`dotnet add package Google.Protobuf`
`dotnet add package Grpc.Tools`

6 Add Protobuf reference to BlazorMart.Client.csproj file

```html
<ItemGroup\>
    <Protobuf Include="Protos\Inventory.proto" GrpcServices="Server" />
</ItemGroup>
```


------

dotnet new razorclasslib -o BlazorMart.ComponentsLibrary
dotnet sln add BlazorMart.ComponentsLibrary/BlazorMart.ComponentsLibrary.csproj

dotnet new classlib -o BlazorMart.Shared
dotnet sln add BlazorMart.Shared/BlazorMart.Shared.csproj

BlazorMart.Client>
dotnet add reference ../BlazorMart.ComponentsLibrary/BlazorMart.ComponentsLibrary.csproj
dotnet add reference ../BlazorMart.Shared/BlazorMart.Shared.csproj

dotnet add package microsoft.aspnetcore.components.webassembly.authentication
dotnet add package microsoft.extensions.http

BlazorMart.Server>
dotnet add reference ../BlazorMart.Client/BlazorMart.Client.csproj
dotnet add reference ../BlazorMart.Shared/BlazorMart.Shared.csproj

dotnet add package microsoft.aspnetcore.components.webassembly.server
dotnet add package microsoft.aspnetcore.apiauthorization.identityserver
dotnet add package microsoft.aspnetcore.components.webassembly.server
dotnet add package microsoft.aspnetcore.diagnostics.entityframeworkcore
dotnet add package microsoft.aspnetcore.identity.entityframeworkcore
dotnet add package microsoft.aspnetcore.identity.ui
dotnet add package microsoft.aspnetcore.mvc.newtonsoftjson
dotnet add package microsoft.entityframeworkcore.sqlite
dotnet add package microsoft.entityframeworkcore.tools

BlazorMart.Shared>
dotnet add reference ../BlazorMart.ComponentsLibrary/BlazorMart.ComponentsLibrary.csproj

Add 'Directory.Build.props' file to root of project
<Project>
    <PropertyGroup>
        <AspNetCoreVersion>3.1.4</AspNetCoreVersion>
        <BlazorVersion>3.2.0</BlazorVersion>
        <EntityFrameworkVersion>3.1.4</EntityFrameworkVersion>
        <SystemNetHttpJsonVersion>3.2.0</SystemNetHttpJsonVersion>
    </PropertyGroup>
</Project>

Update BlazorMart.Client.csproj project file
<ItemGroup>
	<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly" Version="$(BlazorVersion)" />
	<PackageReference Include="microsoft.aspnetcore.components.webassembly.authentication" Version="$(BlazorVersion)" />
	<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.Build" Version="$(BlazorVersion)" PrivateAssets="all" />
	<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.DevServer" Version="$(BlazorVersion)" PrivateAssets="all" />
	<PackageReference Include="microsoft.extensions.http" Version="$(AspNetCoreVersion)" />
	<PackageReference Include="System.Net.Http.Json" Version="$(SystemNetHttpJsonVersion)" />
</ItemGroup>

Update BlazorMart.Server.csproj project file
<ItemGroup>
	<PackageReference Include="microsoft.aspnetcore.apiauthorization.identityserver" Version="$(AspNetCoreVersion)" />
	<PackageReference Include="microsoft.aspnetcore.components.webassembly.server" Version="$(BlazorVersion)" />
	<PackageReference Include="microsoft.aspnetcore.diagnostics.entityframeworkcore" Version="$(AspNetCoreVersion)" />
	<PackageReference Include="microsoft.aspnetcore.identity.entityframeworkcore" Version="$(AspNetCoreVersion)" />
	<PackageReference Include="microsoft.aspnetcore.identity.ui" Version="$(AspNetCoreVersion)" />
	<PackageReference Include="microsoft.aspnetcore.mvc.newtonsoftjson" Version="$(AspNetCoreVersion)" />
	<PackageReference Include="microsoft.entityframeworkcore.sqlite" Version="$(EntityFrameworkVersion)" />
	<PackageReference Include="microsoft.entityframeworkcore.tools" Version="$(EntityFrameworkVersion)">
	  <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
	  <PrivateAssets>all</PrivateAssets>
	</PackageReference>
</ItemGroup>

Add .gitignore file



