using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrowseDotNet.Web.Models.jsTree
{
    public class JsTreeModel
    {
        public string data;
        public JsTreeAttribute attr;
        public List<JsTreeModel> children;
    }

    public class JsTreeAttribute
    {
        public string id;
        public bool selected;
    }
}