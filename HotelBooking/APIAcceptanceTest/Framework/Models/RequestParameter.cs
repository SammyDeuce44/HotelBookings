using System.Net.Http;

namespace APIAcceptanceTest.Framework.Models
{
   public class RequestParameter
   {
      public string BaseAddress { get; set; }

      public string UriPath { get; set; }

      public HeaderParameter HeaderParameter { get; set; }

      public HttpMethod Method { get; set; }

      public object RequestBody { get; set; }

      public RequestParameter(string address, string path, HeaderParameter headers, HttpMethod method, object requestBody)
      {
         BaseAddress = address;
         UriPath = path;
         HeaderParameter = headers;
         Method = method;
         RequestBody = requestBody;
      }
   }
}