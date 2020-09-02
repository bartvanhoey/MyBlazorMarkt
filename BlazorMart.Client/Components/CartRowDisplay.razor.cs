using BlazorMart.Client.State;
using Microsoft.AspNetCore.Components;

namespace BlazorMart.Client.Components
{
    public class CartRowDisplayBase : ComponentBase
    {
        [Parameter] public CartRow Data { get; set; }
    }
}