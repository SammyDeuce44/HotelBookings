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
   [Story(AsA = "User", IWant = "get a specific booking record", SoThat = "I can view that specific record")]
   public class GetBookingItemFeature : BaseFeature
   {
      private BookingSteps _step;
      private AssertSteps _assert;
      private StepAnnotation<GetBookingItemFeature> _annotate;

      [SetUp]
      public void Setup()
      {
         GlobalContext.FeatureContext = new FeatureContext();
         _annotate = new StepAnnotation<GetBookingItemFeature>(this);
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
      public void GetBookingItem(string firstname, string surname, double price, bool deposit, string checkInDate, string checkOutDate)
      {
         var requestBody = new AddRequestModel(firstname, surname, price, deposit, checkInDate, checkOutDate);
         var getHeaders = new HeaderParameter();
         var addHeaders = getHeaders.AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();

         _annotate
            .Given(_ => _step.TheUserWithTheFollowingDetails(requestBody))
            .And(_ => _step.AddBookingIsCalled(addHeaders))
            .And(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.AddBookingResponseMessage))
            .When(_ => _step.GetBookingItemIsCalled(getHeaders))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.GetBookingItemResponseMessage))
            .And(_ => _assert.TheBookingResponseContainsTheFirstname(firstname))
            .BDDfy();
      }
   }
}