namespace APIAcceptanceTest.Framework.Context
{
   public class GlobalContext
   {
      public EnvironmentContext EnvironmentContext { get; set; }

      public FeatureContext FeatureContext { get; set; }

      public GlobalContext()
      {
         EnvironmentContext = new EnvironmentContext();
      }
   }
}