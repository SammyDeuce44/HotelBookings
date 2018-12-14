using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using APIAcceptanceTest.Framework.Annotation;
using APIAcceptanceTest.Framework.Context;
using APIAcceptanceTest.Framework.Contracts;
using APIAcceptanceTest.Framework.Controller;
using APIAcceptanceTest.Framework.Models;
using NUnit.Framework;

namespace APIAcceptanceTest.Steps
{
   public class AssertSteps
   {
      private readonly FeatureContext _context;
      private readonly ApiController _apiController;

      public AssertSteps(FeatureContext context)
      {
         _context = context;
         _apiController = new ApiController();
      }

      [StepText("the status code is {0}")]
      public void TheStatusCodeIs(HttpStatusCode status, StepKey key)
      {
         var result = _context.Get<HttpResponseMessage>(key);
         Assert.That(result.StatusCode, Is.EqualTo(status));
      }

      [StepText("the booking Id in the response is not null or empty")]
      public void TheBookingIdIsNotNullOrEmpty()
      {
         var result = _context.Get<AddResponseModel>(StepKey.AddBookingSuccess);
         Assert.That(result.BookingId, Is.Not.Null.Or.Empty);
      }

      [StepText("the booking response contains the firstname {0}")]
      public void TheBookingResponseContainsTheFirstname(string name)
      {
         var result = _context.Get<AddResponseModel>(StepKey.AddBookingSuccess);
         Assert.That(result.Booking.firstname.Contains(name));
      }

      [StepText("the booking response contains the text {0}")]
      public void TheDeleteResponseContains(string text)
      {
         var result = _context.Get<string>(StepKey.DeleteBookingSuccess);
         Assert.That(result.Contains(text));
      }

      [StepText("the booking response count is greater than {0}")]
      public void TheGetResponseCountIsGreaterThan(int value)
      {
         var result = _context.Get<List<GetResponseModel>>(StepKey.GetBookingSuccess);
         Assert.That(result.Count, Is.GreaterThan(value));
      }

      [StepText("the new count is the same from the previous count")]
      public void CompareCurrentCountToPreviousCountIsTheSame()
      {
         var previousCount = _context.Get<int>(StepKey.CurrentCount);
         var currentCount = _context.Get<List<GetResponseModel>>(StepKey.GetBookingSuccess);
         Assert.AreEqual(previousCount, currentCount.Count);
      }

      [StepText("the new count is different from the previous count")]
      public void CompareCurrentCountToPreviousCountIsDifferent()
      {
         var previousCount = _context.Get<int>(StepKey.CurrentCount);
         var currentCount = _context.Get<List<GetResponseModel>>(StepKey.GetBookingSuccess);
         Assert.AreNotEqual(previousCount, currentCount.Count);
      }

      [StepText("the error message contains the text {0}")]
      public void TheErrorMessageContains(string message)
      {
         var error = _context.Get<HttpResponseMessage>(StepKey.AddBookingResponseMessage);
         var resultAsString = _apiController.ReadContentAsString(error);
         Assert.That(resultAsString.Contains(message));
      }
   }
}