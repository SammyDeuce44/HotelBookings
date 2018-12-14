using System.Collections.Generic;
using System.Net.Http;
using APIAcceptanceTest.Framework.Annotation;
using APIAcceptanceTest.Framework.Context;
using APIAcceptanceTest.Framework.Contracts;
using APIAcceptanceTest.Framework.Controller;
using APIAcceptanceTest.Framework.Models;
using Newtonsoft.Json;

namespace APIAcceptanceTest.Steps
{
   public class BookingSteps
   {
      private readonly GlobalContext _gContext;
      private readonly FeatureContext _context;
      private readonly ApiController _apiController;

      public BookingSteps(GlobalContext globalContext)
      {
         _gContext = globalContext;
         _context = globalContext.FeatureContext;
         _apiController = new ApiController();
      }

      [StepText("the user adds the following details")]
      public void TheUserWithTheFollowingDetails(AddRequestModel request)
      {
         _context.Set(StepKey.BookingDetails, request);
      }

      [StepText("an active user")]
      public void TheUserIsActive()
      {
         // required to compile and not throw null exception for object
      }

      [StepText("the add booking endpoint is called")]
      public void AddBookingIsCalled(HeaderParameter headers)
      {
         var environmentSettings = _gContext.EnvironmentContext;
         var requestBody = _context.Get<AddRequestModel>(StepKey.BookingDetails);
         var requestParameter = new RequestParameter(environmentSettings.BaseAddress, environmentSettings.Path, headers, HttpMethod.Post, requestBody);

         var response = _apiController.Execute(requestParameter);
         _context.Set(StepKey.AddBookingResponseMessage, response);

         if (response.IsSuccessStatusCode)
         {
            var resultAsString = _apiController.ReadContentAsString(response);
            var result = JsonConvert.DeserializeObject<AddResponseModel>(resultAsString);
            _context.Set(StepKey.AddBookingSuccess, result);
         }
      }

      [StepText("the get booking endpoint is called")]
      public void GetBookingsIsCalled(HeaderParameter headers)
      {
         var environmentSettings = _gContext.EnvironmentContext;
         var requestParameter = new RequestParameter(environmentSettings.BaseAddress, environmentSettings.Path, headers, HttpMethod.Get, null);

         var response = _apiController.Execute(requestParameter);
         _context.Set(StepKey.GetBookingResponseMessage, response);

         if (response.IsSuccessStatusCode)
         {
            var resultAsString = _apiController.ReadContentAsString(response);
            var result = JsonConvert.DeserializeObject<List<GetResponseModel>>(resultAsString);
            _context.Set(StepKey.GetBookingSuccess, result);
         }
      }

      [StepText("the get booking item endpoint is called")]
      public void GetBookingItemIsCalled(HeaderParameter headers)
      {
         var environmentSettings = _gContext.EnvironmentContext;
         var bookingId = _context.Get<AddResponseModel>(StepKey.AddBookingSuccess).BookingId;
         var requestParameter = new RequestParameter(environmentSettings.BaseAddress, environmentSettings.Path + "/" + bookingId, headers, HttpMethod.Get, null);

         var response = _apiController.Execute(requestParameter);
         _context.Set(StepKey.GetBookingItemResponseMessage, response);

         if (response.IsSuccessStatusCode)
         {
            var resultAsString = _apiController.ReadContentAsString(response);
            var result = JsonConvert.DeserializeObject<AddRequestModel>(resultAsString);
            _context.Set(StepKey.GetBookingItemSuccess, result);
         }
      }

      [StepText("the delete booking endpoint is called")]
      public void DeleteBookingIsCalled(HeaderParameter headers)
      {
         var environmentSettings = _gContext.EnvironmentContext;
         var bookingId = _context.Get<AddResponseModel>(StepKey.AddBookingSuccess).BookingId;
         var requestParameter = new RequestParameter(environmentSettings.BaseAddress, environmentSettings.Path + "/" + bookingId, headers, HttpMethod.Delete, null);

         var response = _apiController.Execute(requestParameter);
         _context.Set(StepKey.DeleteBookingResponseMessage, response);

         if (response.IsSuccessStatusCode)
         {
            var resultAsString = _apiController.ReadContentAsString(response);
            _context.Set(StepKey.DeleteBookingSuccess, resultAsString);
         }
      }

      [StepText("the delete booking endpoint is called")]
      public void DeleteBookingIsCalled(HeaderParameter headers, string bookingId)
      {
         var environmentSettings = _gContext.EnvironmentContext;
         var requestParameter = new RequestParameter(environmentSettings.BaseAddress, environmentSettings.Path + "/" + bookingId, headers, HttpMethod.Delete, null);

         var response = _apiController.Execute(requestParameter);
         _context.Set(StepKey.DeleteBookingResponseMessage, response);

         if (response.IsSuccessStatusCode)
         {
            var resultAsString = _apiController.ReadContentAsString(response);
            _context.Set(StepKey.DeleteBookingSuccess, resultAsString);
         }
      }

      [StepText("the current count is stored")]
      public void GetCurrentBookingCount()
      {
         var result = _context.Get<List<GetResponseModel>>(StepKey.GetBookingSuccess).Count;
         _context.Set(StepKey.CurrentCount, result);
      }
   }
}