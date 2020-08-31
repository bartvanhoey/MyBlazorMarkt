using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMart.Server;
using Grpc.Core;

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
      var request = new ProductDetailsRequest { EAN = ean };
      var reply = await _inventoryClient.ProductDetailsAsync(request);
      return reply.Product;
    }

    public async Task AddItemAsync(string ean)
    {
      var existingRow = Rows.SingleOrDefault(r => r.EAN == ean);
      if (existingRow != null) existingRow.Quantity++;
      else
      {
        var row = new CartRow { EAN = ean, Quantity = 1 };
        _rows.Add(row);

        for (int i = 0; i < 20; i++)
        {
          try
          {
            row.Product = await FetchProductData(ean);
            break;
          }
          catch (RpcException) { await Task.Delay(5000); }
        }

        if (row.Product == null) _rows.Remove(row);
      }


    }



  }
}