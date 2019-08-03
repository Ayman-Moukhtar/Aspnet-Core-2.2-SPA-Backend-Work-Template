using System;
using System.Net.Mail;

namespace SWE.JOIN.CrossCutting.Framework.Utilities
{
  public static class ValidationUtility
  {
    public static bool IsValidEmail(string email)
    {
      try
      {
        // ReSharper disable once UnusedVariable
        var mailAddress = new MailAddress(email);

        return true;
      }
      catch (FormatException)
      {
        return false;
      }
    }
  }
}
