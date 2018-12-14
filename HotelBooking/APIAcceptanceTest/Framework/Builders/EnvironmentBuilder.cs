using System;
using Microsoft.Extensions.Configuration;

namespace APIAcceptanceTest.Framework.Builders
{
   public class EnvironmentBuilder
   {
      public static string GetConfigValue(IConfiguration config, string name)
      {
         try
         {
            return config.GetSection(name).Value;
         }
         catch (Exception e)
         {
            Console.WriteLine(e);
            throw;
         }
      }
   }
}
