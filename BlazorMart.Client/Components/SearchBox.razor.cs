using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMart.Client.State;
using BlazorMart.Server;
using Microsoft.AspNetCore.Components;
using static BlazorMart.Server.Inventory;

namespace BlazorMart.Client.Components
{
  public class SearchBoxBase : ComponentBase
  {
    [Inject] public InventoryClient InventoryClient { get; set; }
    [Parameter] public EventCallback<string> OnItemChosen { get; set; }
    protected bool isInRemoveMode;
    protected string searchText;
    protected List<AutoCompleteItem> autocompleteItems = new List<AutoCompleteItem>();



    protected async Task AddItemAsync()
    {
      var ean = searchText.ToLower();
      searchText = null;
      autocompleteItems.Clear();
      await OnItemChosen.InvokeAsync(ean);
    }

    protected async Task UpdateAutoCompleteAsync(ChangeEventArgs eventArgs)
    {
      searchText = (string)eventArgs.Value;
      var matchingItem = autocompleteItems.FirstOrDefault(i => i.Name == searchText);
      if (matchingItem != null)
      {
        searchText = matchingItem.EAN;
        await AddItemAsync();
      }
      else
      {
        var request = new AutoCompleteRequest { SearchQuery = searchText };
        var reply = await InventoryClient.AutoCompleteAsync(request);
        autocompleteItems = reply.Items.ToList();
      }
    }
  }
}