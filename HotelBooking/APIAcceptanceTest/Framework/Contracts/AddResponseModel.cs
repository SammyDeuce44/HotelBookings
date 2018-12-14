using System.Runtime.Serialization;

namespace APIAcceptanceTest.Framework.Contracts
{
   public class AddResponseModel
   {
      [DataMember(Name = "bookingid")]
      public string BookingId { get; set; }

      [DataMember(Name = "booking")]
      public AddRequestModel Booking { get; set; }
   }
}