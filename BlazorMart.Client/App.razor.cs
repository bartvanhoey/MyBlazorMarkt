using System.Threading.Tasks;
using BlazorMart.Client.State;
using Microsoft.AspNetCore.Components;

namespace BlazorMart.Client
{
  public class AppBase : ComponentBase
  {
    [Inject] public Cart Cart { get; set; }

    bool isInRemoveMode;

    protected void EnterRemoveMode()
    {
      isInRemoveMode = true;
    }

    protected async Task HandleItemChosen(string ean)
    {
      if (isInRemoveMode)
      {
        Cart.RemoveItem(ean);
        isInRemoveMode = false;
      }
      else
      {
        await Cart.AddItemAsync(ean);
      }
    }

  }
}