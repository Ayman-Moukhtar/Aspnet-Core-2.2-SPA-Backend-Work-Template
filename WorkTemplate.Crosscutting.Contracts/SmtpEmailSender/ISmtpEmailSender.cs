namespace SWE.JOIN.CrossCutting.SmtpEmailSender
{
  public interface ISmtpEmailSender
  {
    void Send(EmailProperties email);
  }
}