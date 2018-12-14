using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using APIAcceptanceTest.Framework.Models;
using Newtonsoft.Json;

namespace APIAcceptanceTest.Framework.Controller
{
   public class ApiController
   {
      public HttpResponseMessage Execute(RequestParameter requestParameter)
      {
         var handler1 = new HttpClientHandler
         {
            ClientCertificateOptions = ClientCertificateOption.Manual,
            ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
         };

         var client = new HttpClient(handler1);

         var buildUrl = new UriBuilder(requestParameter.BaseAddress) { Path = requestParameter.UriPath };

         var request = new HttpRequestMessage()
         {
            RequestUri = buildUrl.Uri,
            Method = requestParameter.Method
         };

         foreach (var header in requestParameter.HeaderParameter.GetParameters())
         {
            client.DefaultRequestHeaders.Add(header.Key, header.Value);
         }

         if (requestParameter.RequestBody != null)
         {
            var json = JsonConvert.SerializeObject(requestParameter.RequestBody);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
         }

         return client.SendAsync(request).Result;
      }

      public string ReadContentAsString(HttpResponseMessage response)
      {
         return response.Content.ReadAsStringAsync().Result;
      }

      private static string ToQueryString(IDictionary<string, string> dict)
      {
         var list = dict.Select(item => item.Key + "=" + item.Value).ToList();
         return string.Join("&", list);
      }
   }
}
