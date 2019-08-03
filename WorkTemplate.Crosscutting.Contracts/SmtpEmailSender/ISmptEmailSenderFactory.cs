namespace SWE.JOIN.CrossCutting.SmtpEmailSender
{
    public interface ISmptEmailSenderFactory
    {
        ISmtpEmailSender Create();
    }
}