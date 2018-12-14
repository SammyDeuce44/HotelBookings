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
   [Story(AsA = "User", IWant = "get the hotel booking records", SoThat = "I can view them")]
   public class GetFeature : BaseFeature
   {
      private BookingSteps _step;
      private AssertSteps _assert;
      private StepAnnotation<GetFeature> _annotate;

      [SetUp]
      public void Setup()
      {
         GlobalContext.FeatureContext = new FeatureContext();
         _annotate = new StepAnnotation<GetFeature>(this);
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

      [TestCase("R", "H", 0, false, "2018-12-09", "2018-12-10")]
      public void DeleteExistingBooking(string firstname, string surname, double price, bool deposit, string checkInDate, string checkOutDate)
      {
         var requestBody = new AddRequestModel(firstname, surname, price, deposit, checkInDate, checkOutDate);
         var getHeaders = new HeaderParameter();
         var addHeaders = getHeaders.AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();

         _annotate
            .Given(_ => _step.TheUserWithTheFollowingDetails(requestBody))
            .And(_ => _step.AddBookingIsCalled(addHeaders))
            .And(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.AddBookingResponseMessage))
            .When(_ => _step.GetBookingsIsCalled(getHeaders))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.GetBookingResponseMessage))
            .And(_ => _assert.TheGetResponseCountIsGreaterThan(1))
            .BDDfy();
      }

      [TestCase("R", "H", 0, false, "2018-12-09", "2018-12-10")]
      public void GetAddGetJourney(string firstname, string surname, double price, bool deposit, string checkInDate, string checkOutDate)
      {
         var requestBody = new AddRequestModel(firstname, surname, price, deposit, checkInDate, checkOutDate);
         var getHeaders = new HeaderParameter();
         var addHeaders = getHeaders.AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();

         _annotate
            .Given(_ => _step.GetBookingsIsCalled(getHeaders))
            .And(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.GetBookingResponseMessage))
            .And(_ => _step.GetCurrentBookingCount())
            .And(_ => _step.TheUserWithTheFollowingDetails(requestBody))
            .And(_ => _step.AddBookingIsCalled(addHeaders))
            .And(_ => _step.AddBookingIsCalled(addHeaders))
            .And(_ => _step.AddBookingIsCalled(addHeaders))
            .And(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.AddBookingResponseMessage))
            .When(_ => _step.GetBookingsIsCalled(getHeaders))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.GetBookingResponseMessage))
            .And(_ => _assert.CompareCurrentCountToPreviousCountIsDifferent())
            .BDDfy();
      }
   }
}