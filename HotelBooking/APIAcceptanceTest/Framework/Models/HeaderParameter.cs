using System.Collections.Generic;
using System.Net;

namespace APIAcceptanceTest.Framework.Models
{
   public class HeaderParameter
   {
      private readonly Dictionary<string, string> _parameter;

      public HeaderParameter()
      {
         _parameter = new Dictionary<string, string>();
         AddAccept();
         AddUserAgent();
      }

      private HeaderParameter AddAccept()
      {
         _parameter.Add(HttpRequestHeader.Accept.ToString(), "application/json");
         return this;
      }

      public HeaderParameter AddOrigin()
      {
         _parameter.Add("Origin", "http://hotel-test.equalexperts.io");
         return this;
      }

      public HeaderParameter AddXmlHttpRequest()
      {
         _parameter.Add("X-Requested-With", "XMLHttpRequest");
         return this;
      }

      private HeaderParameter AddUserAgent()
      {
         _parameter.Add("User-Agent", "api");
         return this;
      }

      public HeaderParameter AddReferer()
      {
         _parameter.Add("Referer", "http://hotel-test.equalexperts.io");
         return this;
      }

      public HeaderParameter AddEncoding()
      {
         _parameter.Add("Accept-Encoding", "gzip");
         return this;
      }

      public HeaderParameter AddLanguage()
      {
         _parameter.Add("Accept-Language", "en-GB");
         return this;
      }

      public HeaderParameter AddAuthorization()
      {
         _parameter.Add("authorization", "Basic YWRtaW46cGFzc3dvcmQxMjM=");
         return this;
      }

      public Dictionary<string, string> GetParameters()
      {
         return _parameter;
      }
   }
}
