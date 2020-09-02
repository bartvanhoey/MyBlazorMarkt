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

6 Add required Grpc Packages to BlazorMart.Client project
`dotnet add package Grpc.AspNetCore.Web`

7 Add Protobuf reference to BlazorMart.Client.csproj file

```html
<ItemGroup\>
    <Protobuf Include="Protos\Inventory.proto" GrpcServices="Server" />
</ItemGroup>
```
