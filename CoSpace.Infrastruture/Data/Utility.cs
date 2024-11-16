using System;
using System.Threading.Tasks;
using CoSpace.Core.Entities;
using CoSpace.Infrastruture.Data;
using Microsoft.EntityFrameworkCore;

namespace CoSpace.Infrastructure.Data
{
    public class Utility
    {
        private readonly ApplicationDbContext _dbContext;

        public Utility(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddAsync<T>(T entity, int createdBy) where T : Base
        {
            SetAuditFieldsForCreate(entity, createdBy);
            _dbContext.Set<T>().Add(entity);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        private void SetAuditFieldsForCreate<T>(T entity, int createdBy) where T : Base
        {
            entity.CreatedBy = createdBy;
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedBy = createdBy;
            entity.UpdatedDate = DateTime.Now;
        }
    }
}
