namespace APIAcceptanceTest.Framework.Contracts
{
   public class AddRequestModel
   {
      public AddRequestModel(string fname, string lname, double? price, bool? deposit, string checkin, string checkout)
      {
         bookingdates = new BookingDates();
         firstname = fname;
         lastname = lname;
         depositpaid = $"{deposit}";
         totalprice = $"{price}";
         bookingdates.checkin = checkin;
         bookingdates.checkout = checkout;
      }

      public string firstname { get; set; }

      public string lastname { get; set; }

      public string depositpaid { get; set; }

      public string totalprice { get; set; }

      public BookingDates bookingdates { get; set; }
   }

   public class BookingDates
   {
      public string checkin { get; set; }
      public string checkout { get; set; }
   }
}
