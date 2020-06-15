using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazorMart.Client.State;

namespace MyBlazorMart.Client
{
  public class AppBase : ComponentBase
  {
    [Inject] public Cart Cart { get; set; }

    bool isInRemoveMode;

    protected void EnterRemoveMode(){
      isInRemoveMode = true;
    }

    protected async Task HandleOnItemChosen(string ean)
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