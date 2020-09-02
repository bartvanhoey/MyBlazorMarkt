using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorMart.Client.State;
using BlazorMart.Server;
using Microsoft.AspNetCore.Components;

namespace BlazorMart.Client.Components
{
  public class SearchBoxBase : ComponentBase
  {
    [Parameter] public EventCallback<string> OnItemChosen { get; set; }
    [Inject] public Inventory.InventoryClient InventoryClient { get; set; }
    protected List<AutoCompleteItem> autoCompleteItems = new List<AutoCompleteItem>();
    protected string searchText;

    protected async Task UpdateAutoComplete(ChangeEventArgs eventArgs)
    {
      searchText = (string)eventArgs.Value;
      var matchingItem = autoCompleteItems.FirstOrDefault(p => p.Name == searchText);
      if (matchingItem != null)
      {
        searchText = matchingItem.EAN;
        await AddItem();
      }
      else
      {
        var request = new AutoCompleteRequest { SearchQuery = searchText };
        var reply = await InventoryClient.AutoCompleteAsync(request);
        autoCompleteItems = reply.Items.ToList();
      }
    }

    protected async Task AddItem()
    {
        var ean = searchText.ToLower();
        searchText = null;
        autoCompleteItems.Clear();
        await OnItemChosen.InvokeAsync(ean);
    }
  }
}