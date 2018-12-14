using System;

namespace APIAcceptanceTest.Framework.Annotation
{
   public class StepText : Attribute
   {
      public string Text { get; }

      public StepText(string stepText)
      {
         Text = stepText;
      }
   }
}
