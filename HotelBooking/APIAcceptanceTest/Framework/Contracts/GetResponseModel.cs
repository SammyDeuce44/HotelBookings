using Newtonsoft.Json;

namespace APIAcceptanceTest.Framework.Contracts
{
   public class GetResponseModel
   {
      [JsonProperty("bookingid")]
      public string Key { get; set; }
   }
}