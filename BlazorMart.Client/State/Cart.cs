using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMart.Server;

namespace BlazorMart.Client.State
{
  public class Cart
  {
    private readonly Inventory.InventoryClient _inventoryClient;
    private readonly List<CartRow> _rows = new List<CartRow>();

    public IReadOnlyList<CartRow> Rows => _rows;
    public bool HasAnyProducts => _rows.Any(r => r.Product != null);
    public decimal GrandTotal => Rows.Where(r => r.Product != null).Sum(r => r.Quantity * r.Product.Price / 100);

    public Cart(Inventory.InventoryClient inventoryClient)
    {
      _inventoryClient = inventoryClient;
    }

    private async Task<Product> FetchProductData(string ean) 
    {
      var request = new ProductDetailsRequest {EAN = ean};
      var reply = await _inventoryClient.ProductDetailsAsync(request);
      return reply.Product;
     }



  }
}