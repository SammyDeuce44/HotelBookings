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
   [Story(AsA = "User", IWant = "to delete my booking", SoThat = "my reservation no longer exists")]
   public class DeleteFeature : BaseFeature
   {
      private BookingSteps _step;
      private AssertSteps _assert;
      private StepAnnotation<DeleteFeature> _annotate;

      [SetUp]
      public void Setup()
      {
         GlobalContext.FeatureContext = new FeatureContext();
           _annotate = new StepAnnotation<DeleteFeature>(this);
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
         var addHeaders = new HeaderParameter().AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();
         var deleteHeaders = addHeaders.AddAuthorization();

         _annotate
            .Given(_ => _step.TheUserWithTheFollowingDetails(requestBody))
            .And(_ => _step.AddBookingIsCalled(addHeaders))
            .And(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.AddBookingResponseMessage))
            .When(_ => _step.DeleteBookingIsCalled(deleteHeaders))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.Created, StepKey.DeleteBookingResponseMessage))
            .And(_ => _assert.TheDeleteResponseContains("Created"))
            .BDDfy();
      }

      [Test]
      public void DeleteWithoutAuthorization()
      {
         var deleteHeaders = new HeaderParameter().AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();

         _annotate
            .Given(_ => _step.TheUserIsActive())
            .When(_ => _step.DeleteBookingIsCalled(deleteHeaders, "12345"))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.Forbidden, StepKey.DeleteBookingResponseMessage))
            .BDDfy();
      }

      [TestCase("R", "H", 0, false, "2018-12-09", "2018-12-10")]
      public void GetAddDeleteGetJourney(string firstname, string surname, double price, bool deposit, string checkInDate, string checkOutDate)
      {
         var requestBody = new AddRequestModel(firstname, surname, price, deposit, checkInDate, checkOutDate);
         var getHeaders = new HeaderParameter();
         var addHeaders = getHeaders.AddEncoding().AddLanguage().AddOrigin().AddReferer().AddXmlHttpRequest();
         var deleteHeaders = addHeaders.AddAuthorization();

         _annotate
            .Given(_ => _step.GetBookingsIsCalled(getHeaders))
            .And(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.GetBookingResponseMessage))
            .And(_ => _step.GetCurrentBookingCount())
            .And(_ => _step.TheUserWithTheFollowingDetails(requestBody))
            .And(_ => _step.AddBookingIsCalled(addHeaders))
            .And(_ => _assert.TheStatusCodeIs(HttpStatusCode.OK, StepKey.AddBookingResponseMessage))
            .When(_ => _step.DeleteBookingIsCalled(deleteHeaders))
            .Then(_ => _assert.TheStatusCodeIs(HttpStatusCode.Created, StepKey.DeleteBookingResponseMessage))
            .And(_ => _assert.TheDeleteResponseContains("Created"))
            .And(_ => _assert.CompareCurrentCountToPreviousCountIsTheSame())
            .BDDfy();
      }
   }
}