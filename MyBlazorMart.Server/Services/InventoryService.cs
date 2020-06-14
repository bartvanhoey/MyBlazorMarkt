using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyBlazorMart.Server
{
  public class InventoryService : Inventory.InventoryBase
  {
    private static Product[] _products =
        JsonSerializer.Deserialize<Product[]>(File.ReadAllText("products.json"));

    public override Task<AutoCompleteReply> AutoComplete(AutoCompleteRequest request, Grpc.Core.ServerCallContext context)
    {
      var autoCompleteReply = new AutoCompleteReply();
      if (!string.IsNullOrEmpty(request.SearchQuery))
      {
        var matches = _products
            .Where(p => p.Name.StartsWith(request.SearchQuery, StringComparison.CurrentCultureIgnoreCase))
            .Select(p => new AutoCompleteItem { EAN = p.EAN, Name = p.Name })
            .Take(10);
        autoCompleteReply.Items.AddRange(matches);
      }
      return Task.FromResult(autoCompleteReply);
    }

    public override async Task<ProductDetailsReply> ProductDetails(ProductDetailsRequest request, Grpc.Core.ServerCallContext context)
    {
      await Task.Delay(500);
      var product = _products.FirstOrDefault(p => p.EAN == request.EAN);
      return new ProductDetailsReply { Product = product };
    }

  }
}