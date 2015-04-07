using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml.Linq;

namespace XmlStripNamespaces.Controllers
{
    public class RemoveNamespaceController : ApiController
    {
        // POST api/values
        public HttpResponseMessage Post(HttpRequestMessage request) 
        {
            var someText = request.Content.ReadAsStringAsync().Result;
            try
            {
                String myStringOutput = stripNS(XElement.Parse(someText)).ToString();
                return new HttpResponseMessage() { Content = new StringContent(myStringOutput) };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest) { Content = new StringContent(e.Message) };
            }
            
        }
        private static XElement stripNS(XElement root)
        {
            return new XElement(
                root.Name.LocalName,
                root.HasElements ?
                    root.Elements().Select(el => stripNS(el)) :
                    (object)root.Value
            );
        }
    }
}
