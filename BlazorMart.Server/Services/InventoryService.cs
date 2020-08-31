using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;

namespace BlazorMart.Server.Services
{
  public class InventoryService : Inventory.InventoryBase
  {
    private static Product[] _products = JsonSerializer.Deserialize<Product[]>(File.ReadAllText("products.json"));

    public override Task<AutoCompleteReply> AutoComplete(AutoCompleteRequest request, ServerCallContext context)
    {
      var reply = new AutoCompleteReply();

      if (!string.IsNullOrEmpty(request.SearchQuery))
      {
        var productMatches = _products
         .Where(p => p.Name.StartsWith(request.SearchQuery, StringComparison.CurrentCultureIgnoreCase))
         .Select(p => new AutoCompleteItem { EAN = p.EAN, Name = p.Name })
         .Take(10);
        reply.Items.AddRange(productMatches);
      }
      return Task.FromResult(reply);
    }


    public override async Task<ProductDetailsReply> ProductDetails(ProductDetailsRequest request, ServerCallContext context)
    {
        await Task.Delay(500);
        var product = _products.FirstOrDefault(p => p.EAN == request.EAN);
        return new ProductDetailsReply { Product = product};
    }



  }
}