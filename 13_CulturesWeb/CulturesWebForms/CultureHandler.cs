using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CulturesWebForms
{
    public class CultureHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var value = context.Request.QueryString["culture"];
            Model model = new Model();
            var culture = model.Cultures.FirstOrDefault((c) => c.Name == value);
            if (culture != null)
                model.CurrentCulture = culture;
            var date = model.Date;
            var size = model.Size;
            var json = new JObject();
            json.Add("date", date);
            json.Add("size", size);
            context.Response.Write(json.ToString());
        }
    }
}