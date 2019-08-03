using System;

namespace SWE.JOIN.CrossCutting.SmtpEmailSender
{
  public static class SmtpEmailSenderFactory
  {
    #region Members

    /// CodeReview: change to private static property to improve unit test compliance.
    private static ISmptEmailSenderFactory _currentFactory;

    #endregion

    #region public members

    public static ISmptEmailSenderFactory CurrentFactory
    {
      get { return _currentFactory; }
    }

    #endregion

    #region Public Methods

    /// <summary>
    ///     Sets the current.
    /// </summary>
    /// <param name="factory">The factory.</param>
    /// <exception cref="System.ArgumentNullException">ISerializerFactory</exception>
    public static void SetCurrent(ISmptEmailSenderFactory factory)
    {
      if (null == factory) throw new ArgumentNullException(nameof(factory));
      _currentFactory = factory;
    }

    /// <summary>
    ///     Creates the validator.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">Factory is null, You must first set current factory.</exception>
    public static ISmtpEmailSender CreateSerialize()
    {
      if (null == CurrentFactory)
        throw new InvalidOperationException("Factory is null, You must first set current factory.");
      return CurrentFactory.Create();
    }

    #endregion
  }
}