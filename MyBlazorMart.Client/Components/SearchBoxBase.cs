using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MyBlazorMart.Server;

namespace MyBlazorMart.Client.Components
{
  public class SearchBoxBase : ComponentBase
  {
    protected string searchText;
    protected List<AutoCompleteItem> autocompleteItems = new List<AutoCompleteItem>();
    [Parameter] public EventCallback<string> OnItemChosen { get; set; }
    [Inject]
    public Inventory.InventoryClient InventoryClient { get; set; }

    protected async Task AddItem()
    {
      var ean = searchText.ToLower();
      searchText = null;
      autocompleteItems.Clear();
      await OnItemChosen.InvokeAsync(ean);
    }

    protected async Task UpdateAutoComplete(ChangeEventArgs eventArgs)
    {
      searchText = (string)eventArgs.Value;
      var matchingItem = autocompleteItems.FirstOrDefault(i => i.Name == searchText);
      if (matchingItem != null)
      {
        searchText = matchingItem.EAN;
        await AddItem();
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