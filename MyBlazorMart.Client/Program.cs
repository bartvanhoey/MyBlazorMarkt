using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using System.Runtime.Serialization;
using MyBlazorMart.Server;

namespace MyBlazorMart.Client
{
    public class Program
    {
    public const string BackendUrl =  "https://localhost:5001";

    public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(services => 
            {
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var channel = GrpcChannel.ForAddress(new Uri(BackendUrl), new GrpcChannelOptions { HttpClient = httpClient});
                return new Inventory.InventoryClient(channel);
                
            });


            await builder.Build().RunAsync();
        }
    }
}
