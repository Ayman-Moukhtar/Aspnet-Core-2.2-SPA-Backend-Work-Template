using System;

namespace SWE.JOIN.Domain.Entities.Interfaces
{
  public interface IIocContainer
  {
    object Resolve(Type type);
  }
}
