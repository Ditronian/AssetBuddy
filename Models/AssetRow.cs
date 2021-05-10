using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace AssetBuddy.Models
{
    //Associates an asset with this given row
    public class AssetRow : TableRow
    {
        private Asset asset;

        public Asset Asset { get => asset; set => asset = value; }
    }
}