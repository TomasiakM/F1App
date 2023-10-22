using Domain.Aggregates.GeneralClassifications;
using Domain.Aggregates.GeneralClassifications.ValueObjects;

namespace Infrastructure.Persistence.Repositories;
internal sealed class GeneralClassificationRepository : GenericRepository<GeneralClassification, GeneralClassificationId>, IGeneralClassificationRepository
{
    public GeneralClassificationRepository(AppDbContext dbContext) 
        : base(dbContext)
    {
    }
}
