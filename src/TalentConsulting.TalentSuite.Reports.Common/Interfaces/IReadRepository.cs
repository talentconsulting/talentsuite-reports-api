using Ardalis.Specification;

namespace TalentConsulting.TalentSuite.Reports.Common.Interfaces;


public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
{
}
