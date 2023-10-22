using Domain.Aggregates.GeneralClassifications.ValueObjects;
using Domain.Interfaces;

namespace Domain.Aggregates.GeneralClassifications;
public interface IGeneralClassificationRepository : IRepository<GeneralClassification, GeneralClassificationId>
{
}
