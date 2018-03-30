using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace CulturesWebForms
{
    public partial class _Default : Page
    {
        private Model _Model;

        protected void Page_Load(object sender, EventArgs e)
        {
            _Model = new Model();
            foreach (var culture in _Model.Cultures)
            {
                var li = new ListItem(string.Format("{0} ({1})", culture.DisplayName, culture.Name), culture.Name);
                CulturesListBox.Items.Add(li);
            }
            
        }
    }
}