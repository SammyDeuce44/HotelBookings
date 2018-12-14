using System.Net;
using APIAcceptanceTest.Framework.Annotation;
using APIAcceptanceTest.Framework.Context;
using APIAcceptanceTest.Framework.Contracts;
using APIAcceptanceTest.Framework.Models;
using APIAcceptanceTest.Steps;
using NUnit.Framework;
using TestStack.BDDfy;

namespace APIAcceptanceTest.Features
{
   [Story(AsA = "User", IWant = "Add my details", SoThat = "I can book a hotel")]
   public class AddFeature : BaseFeature
   {
      private BookingSteps _step;
      private AssertSteps _assert;
      private StepAnnotation<AddFeature> _annotate;

      [SetUp]
      public void Setup()
      {
         GlobalContext.FeatureContext = new FeatureContext();
         _annotate = new StepAnnotation<AddFeature>(this);
         _step = new BookingSteps(GlobalContext);
         _assert = new AssertSteps(GlobalContext.FeatureContext);
      }

      [TearDown]
      public void TearDown()
      {
         _annotate = null;
         _step = null;
         _assert = null;
      }

      [TestCase("Robin", "Hood", 20.00, true, "2018-10-01", "2018-10-10")]
      [TestCase("R", "H", 0, false, "2018-12-09", "2018-12-10")]
      public void AddNewBooking(string firstname, string surname, double price, bool deposit, string checkInDate, string checkOutDate)
      {
         var requestBody = new AddRequestModel(firstname, surname, price, deposit, checkInDate, checkOutDate);
         var headers = new HeaderParameter().AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();

         _annotate
            .Given(_ => _step.TheUserWithTheFollowingDetails(requestBody))
            .When(_ => _step.AddBookingIsCalled(headers))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.AddBookingResponseMessage))
            .And(_ => _assert.TheBookingIdIsNotNullOrEmpty())
            .And(_ => _assert.TheBookingResponseContainsTheFirstname(firstname))
            .BDDfy();
      }

      [TestCase("", "Hood", 20.00, true, "2018-10-01", "2018-10-10")]
      [TestCase("Robin", "", 20.00, true, "2018-10-01", "2018-10-10")]
      [TestCase("Robin", "Hood", null, true, "2018-10-01", "2018-10-10")]
      [TestCase("Robin", "Hood", null, null, "2018-10-01", "2018-10-10")]
      [TestCase("Robin", "Hood", 20.00, true, null, "2018-10-10")]
      [TestCase("Robin", "Hood", 20.00, true, "2018-10-01", null)]
      public void AddNewBooking_NegativeTest(string firstname, string surname, double? price, bool? deposit, string checkInDate, string checkOutDate)
      {
         var requestBody = new AddRequestModel(firstname, surname, price, deposit, checkInDate, checkOutDate);
         var headers = new HeaderParameter().AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();

         _annotate
            .Given(_ => _step.TheUserWithTheFollowingDetails(requestBody))
            .When(_ => _step.AddBookingIsCalled(headers))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.InternalServerError, StepKey.AddBookingResponseMessage))
            .And(_ => _assert.TheErrorMessageContains("Internal Server Error"))
            .BDDfy();
      }
   }
}
