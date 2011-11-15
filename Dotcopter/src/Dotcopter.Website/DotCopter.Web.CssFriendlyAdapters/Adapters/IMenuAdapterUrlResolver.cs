using System.Web.UI.WebControls;

namespace CSSFriendly
{
    public interface IMenuAdapterUrlResolver
    {
        string ResolveUrl(Menu menu, string url);
    }
}