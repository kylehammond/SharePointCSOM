using System;
using System.Security;
using Microsoft.SharePoint.Client;

public class SharePointOnlineClient : IDisposable
{
    public SharePointOnlineClient(string siteUrl, string userName, string password)
    {
        // secure password
        var securePassword = new SecureString();
        foreach (var c in password) securePassword.AppendChar(c);

        // get context
        Context = new ClientContext(siteUrl) {Credentials = new SharePointOnlineCredentials(userName, securePassword)};
    }

    private ClientContext Context { get; }

    public void Dispose()
    {
        Context?.Dispose();
    }

    public ListItemCollection GetListItemsByTitle(string listTitle)
    {
        var list = Context.Web.Lists.GetByTitle(listTitle);
        var query = CamlQuery.CreateAllItemsQuery();
        var items = list.GetItems(query);
        Context.Load(items, retrievals =>
            retrievals.IncludeWithDefaultProperties(
                item => item.DisplayName)); // wouldn't be available otherwise
        Context.ExecuteQuery();
        return items;
    }
}