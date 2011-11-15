using System;
using System.Configuration;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CSSFriendly
{
    public class MenuAdapter : System.Web.UI.WebControls.Adapters.MenuAdapter
    {
        private int _nextingLevel;
        private WebControlAdapterExtender _extender = null;
        private readonly IMenuAdapterUrlResolver _urlResolver;

        private WebControlAdapterExtender Extender
        {
            get
            {
                if (((_extender == null) && (Control != null)) ||
                    ((_extender != null) && (Control != _extender.AdaptedControl)))
                {
                    _extender = new WebControlAdapterExtender(Control);
                }

                System.Diagnostics.Debug.Assert(_extender != null, "CSS Friendly adapters internal error", "Null extender instance");
                return _extender;
            }
        }

        public MenuAdapter()
        {
            string typeString = ConfigurationManager.AppSettings["MenuAdapterUrlResolver"];
            if (!string.IsNullOrEmpty(typeString))
            {
                Type urlResolverType = Type.GetType(typeString);
                if (urlResolverType == null)
                {
                    throw new Exception("Could not find type " + typeString);
                }
                if (!typeof(IMenuAdapterUrlResolver).IsAssignableFrom(urlResolverType))
                {
                    throw new Exception("Type specified for MenuAdapterUrlResolver does not implement the interface IMenuAdapterUrlResolver.");
                }
                _urlResolver = (IMenuAdapterUrlResolver)Activator.CreateInstance(urlResolverType);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (Extender.AdapterEnabled)
            {
                //RegisterScripts();
            }
        }

        private void RegisterScripts()
        {
            Extender.RegisterScripts();
            string folderPath = WebConfigurationManager.AppSettings.Get("CSSFriendly-JavaScript-Path");
            if (String.IsNullOrEmpty(folderPath))
            {
                folderPath = "~/JavaScript";
            }
            string filePath = folderPath.EndsWith("/") ? folderPath + "MenuAdapter.js" : folderPath + "/MenuAdapter.js";
            Page.ClientScript.RegisterClientScriptInclude(GetType(), GetType().ToString(), Page.ResolveUrl(filePath));
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (Extender.AdapterEnabled)
            {
                writer.Indent++;
                _nextingLevel++;
                BuildItems(Control.Items, true, writer);
                writer.Indent--;
                _nextingLevel--;
                writer.WriteLine();
            }
            else
            {
                base.RenderContents(writer);
            }
        }

        private void BuildItems(MenuItemCollection items, bool isRoot, HtmlTextWriter writer)
        {
            if (items.Count > 0)
            {
                writer.WriteLine();

                writer.WriteBeginTag("ul");
                if (isRoot)
                {
                    if (!string.IsNullOrEmpty(Control.CssClass))
                    {
                        writer.WriteAttribute("class", Control.CssClass);
                    }
                    if (Control.ID != "SubNavMenu")
                    {
                        writer.WriteAttribute("id", Control.ID);
                    }
                }
                else if (_nextingLevel < Control.StaticDisplayLevels)
                {
                    writer.WriteAttribute("style", "position: relative !important; visibility: visible !important;");
                }
                else if (items[0].Parent.Text == "Contact Us")
                {
                    writer.WriteAttribute("style", "width: 128px !important;");
                }
                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Indent++;
                               
                foreach (MenuItem item in items)
                {
                    BuildItem(item, writer);
                }

                writer.Indent--;
                writer.WriteLine();
                writer.WriteEndTag("ul");
            }
        }

        private void BuildItem(MenuItem item, HtmlTextWriter writer)
        {
            Menu menu = Control;
            if ((menu != null) && (item != null) && (writer != null))
            {
                writer.WriteLine();
                writer.WriteBeginTag("li");

                if (item.Selected && Control.ID == "SubNavMenu")
                {
                    writer.WriteAttribute("id", "Active");
                }

                string theClass = (item.ChildItems.Count > 0) ? "AspNet-Menu-WithChildren" : "AspNet-Menu-Leaf";
                string selectedStatusClass = GetSelectStatusClass(item);
                if (!String.IsNullOrEmpty(selectedStatusClass))
                {
                    theClass += " " + selectedStatusClass;
                }
                if (!string.IsNullOrEmpty(Control.CssClass))
                {
                    writer.WriteAttribute("class", theClass);
                }

                writer.Write(HtmlTextWriter.TagRightChar);
                writer.Indent++;
                writer.WriteLine();

                if (((item.Depth < menu.StaticDisplayLevels) && (menu.StaticItemTemplate != null)) ||
                    ((item.Depth >= menu.StaticDisplayLevels) && (menu.DynamicItemTemplate != null)))
                {
                    writer.WriteBeginTag("div");
                    if (!string.IsNullOrEmpty(Control.CssClass))
                    {
                        writer.WriteAttribute("class", GetItemClass(menu, item));
                    }
                    writer.Write(HtmlTextWriter.TagRightChar);
                    writer.Indent++;
                    writer.WriteLine();

                    MenuItemTemplateContainer container = new MenuItemTemplateContainer(menu.Items.IndexOf(item), item);
                    if ((item.Depth < menu.StaticDisplayLevels) && (menu.StaticItemTemplate != null))
                    {
                        menu.StaticItemTemplate.InstantiateIn(container);
                    }
                    else
                    {
                        menu.DynamicItemTemplate.InstantiateIn(container);
                    }
                    container.DataBind();
                    container.RenderControl(writer);

                    writer.Indent--;
                    writer.WriteLine();
                    writer.WriteEndTag("div");
                }
                else
                {
                    if (IsLink(item))
                    {
                        writer.WriteBeginTag("a");
                        if (!String.IsNullOrEmpty(item.NavigateUrl))
                        {
                            string url = menu.ResolveClientUrl(item.NavigateUrl);
                            if (_urlResolver != null)
                            {
                                url = _urlResolver.ResolveUrl(Control, url);
                                
                            }
                            writer.WriteAttribute("href", Page.Server.HtmlEncode(url));
                        }
                        else
                        {
                            writer.WriteAttribute("href", Page.ClientScript.GetPostBackClientHyperlink(menu, "b" +  item.ValuePath.Replace(menu.PathSeparator.ToString(), "\\"), true));
                        }
                        if (!string.IsNullOrEmpty(Control.CssClass))
                        {
                            writer.WriteAttribute("class", GetItemClass(menu, item));
                        }
                        WebControlAdapterExtender.WriteTargetAttribute(writer, item.Target);

                        if (!String.IsNullOrEmpty(item.ToolTip))
                        {
                            writer.WriteAttribute("title", item.ToolTip);
                        }
                        else if (!String.IsNullOrEmpty(menu.ToolTip))
                        {
                            writer.WriteAttribute("title", menu.ToolTip);
                        }
                        writer.Write(HtmlTextWriter.TagRightChar);
                        writer.Indent++;
                        writer.WriteLine();
                    }
                    else
                    {
                        writer.WriteBeginTag("span");
                        if (!string.IsNullOrEmpty(Control.CssClass))
                        {
                            writer.WriteAttribute("class", GetItemClass(menu, item));
                        }
                        writer.Write(HtmlTextWriter.TagRightChar);
                        writer.Indent++;
                        writer.WriteLine();
                    }

                    if (!String.IsNullOrEmpty(item.ImageUrl))
                    {
                        writer.WriteBeginTag("img");
                        writer.WriteAttribute("src", menu.ResolveClientUrl(item.ImageUrl));
                        writer.WriteAttribute("alt", !String.IsNullOrEmpty(item.ToolTip) ? item.ToolTip : (!String.IsNullOrEmpty(menu.ToolTip) ? menu.ToolTip : item.Text));
                        writer.Write(HtmlTextWriter.SelfClosingTagEnd);
                    }

                    writer.Write(item.Text);

                    if (IsLink(item))
                    {
                        writer.Indent--;
                        writer.WriteEndTag("a");
                    }
                    else
                    {
                        writer.Indent--;
                        writer.WriteEndTag("span");
                    }

                }

                if ((item.ChildItems != null) && (item.ChildItems.Count > 0))
                {
                    BuildItems(item.ChildItems, false, writer);
                }

                writer.Indent--;
                writer.WriteLine();
                writer.WriteEndTag("li");
            }
        }

        private bool IsLink(MenuItem item)
        {
            return (item != null) /*&& !item.Selected*/ && item.Enabled && ((!String.IsNullOrEmpty(item.NavigateUrl)) || item.Selectable);
        }

        private string GetItemClass(Menu menu, MenuItem item)
        {
            //string value = "AspNet-Menu-NonLink";
            string value = "AspNet-Menu-Link";
            if (item != null)
            {
                if (((item.Depth < menu.StaticDisplayLevels) && (menu.StaticItemTemplate != null)) ||
                    ((item.Depth >= menu.StaticDisplayLevels) && (menu.DynamicItemTemplate != null)))
                {
                    value = "AspNet-Menu-Template";
                }
                else if (IsLink(item))
                {
                    value =  "AspNet-Menu-Link";
                }
                string selectedStatusClass = GetSelectStatusClass(item);
                if (!String.IsNullOrEmpty(selectedStatusClass))
                {
                    value += " " + selectedStatusClass;
                }
            }

            return value;
        }

        private string GetSelectStatusClass(MenuItem item)
        {
            string value = "";
            if (item.Selected)
            {
                //value += " AspNet-Menu-Selected";
            }
            else if (IsChildItemSelected(item))
            {
                //value += " AspNet-Menu-ChildSelected";
            }
            else if (IsParentItemSelected(item))
            {
                //value += " AspNet-Menu-ParentSelected";
            }
            return value;
        }

        private bool IsChildItemSelected(MenuItem item)
        {
            bool bRet = false;

            if ((item != null) && (item.ChildItems != null))
            {
                bRet = IsChildItemSelected(item.ChildItems);
            }

            return bRet;
        }

        private bool IsChildItemSelected(MenuItemCollection items)
        {
            bool bRet = false;

            if (items != null)
            {
                foreach (MenuItem item in items)
                {
                    if (item.Selected || IsChildItemSelected(item.ChildItems))
                    {
                        bRet = true;
                        break;
                    }
                }
            }

            return bRet;
        }

        private bool IsParentItemSelected(MenuItem item)
        {
            bool bRet = false;

            if ((item != null) && (item.Parent != null))
            {
                if (item.Parent.Selected)
                {
                    bRet = true;
                }
                else
                {
                    bRet = IsParentItemSelected(item.Parent);
                }
            }

            return bRet;
        }
    }
}
