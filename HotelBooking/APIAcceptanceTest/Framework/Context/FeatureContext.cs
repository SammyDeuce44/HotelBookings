using System;
using System.Collections.Generic;
using APIAcceptanceTest.Framework.Models;

namespace APIAcceptanceTest.Framework.Context
{
   public class FeatureContext
   {
      private readonly IDictionary<StepKey, object> _dict;

      public FeatureContext()
      {
         _dict = new Dictionary<StepKey, object>();
      }

      public void Set(StepKey key, object value)
      {
         _dict[key] = value;
      }

      public T Get<T>(StepKey key)
      {
         if (_dict.ContainsKey(key))
         {
            return (T)_dict[key];
         }

         throw new ArgumentException("Error: Could not ", key.ToString());
      }
   }
}