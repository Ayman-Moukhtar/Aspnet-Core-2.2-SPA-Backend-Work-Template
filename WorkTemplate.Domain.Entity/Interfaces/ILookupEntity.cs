using System;

namespace SWE.JOIN.Domain.Entities.Interfaces
{
  public interface ILookupEntity
  {
    Guid Id { get; set; }
    string Name { get; set; }
  }
}
