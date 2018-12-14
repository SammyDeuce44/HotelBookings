using System.IO;
using APIAcceptanceTest.Framework.Builders;
using APIAcceptanceTest.Framework.Context;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace APIAcceptanceTest.Features
{
   [Parallelizable(ParallelScope.Fixtures)] // Parallel Execution
   public class BaseFeature
   {
      public GlobalContext GlobalContext;

      [OneTimeSetUp]
      public void MasterSetup()
      {
         var globalContext = new GlobalContext();
         var environmentContext = new EnvironmentContext();
         var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "\\Framework\\Config")
            .AddJsonFile("appSettings.json", false, true)
            .Build();
         environmentContext.BaseAddress = EnvironmentBuilder.GetConfigValue(config, "environment");
         environmentContext.Path = EnvironmentBuilder.GetConfigValue(config, "path");
         globalContext.EnvironmentContext = environmentContext;
         GlobalContext = globalContext;
      }

      [OneTimeTearDown]
      public void MasterTearDown()
      {
         GlobalContext = null;
      }
   }
}