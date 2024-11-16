using CoSpace.Core.Entities;
using CoSpace.Infrastruture.Data;

namespace CoSpace.Infrastruture.Repository
{
    public class RepositoryBase<T> where T : Base
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SetAuditFields(T entity, int currentUser, string tranactionType)
        {
            var currentTime = DateTime.UtcNow;


            if (tranactionType == "INSERT")
            {
                entity.CreatedBy = currentUser;
                entity.CreatedDate = currentTime;
                entity.IsDeleted = false;
            }
            else if (tranactionType == "DELETE")
            {
                entity.IsDeleted = true;
            }
            entity.UpdatedBy = currentUser;
            entity.UpdatedDate = currentTime;
        }
    }
}
