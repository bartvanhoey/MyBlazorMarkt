using System.Threading.Tasks;
using BlazorMart.Client.State;
using Microsoft.AspNetCore.Components;

namespace BlazorMart.Client
{
  public class AppBase : ComponentBase
  {
    [Inject] public Cart Cart { get; set; }

    protected async Task HandleItemChosen(string ean)
    {
      await Cart.AddItemAsync(ean);
    }

  }
}