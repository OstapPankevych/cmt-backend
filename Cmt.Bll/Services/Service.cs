using System;
using Cmt.Bll.Services.Exceptions;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces.Repositories;

namespace Cmt.Bll.Services
{
    public abstract class Service
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected void Create<U>(Entity<U> entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        protected void Update<U>(
            Entity<U> entity,
            int updatedBy,
            DateTime unmodifiedSince)
        {
            if (entity == null)
                throw new CmtException(ErrorCodes.NotFound);

            if (unmodifiedSince < entity.UpdatedAt)
                throw new CmtException(ErrorCodes.UnmodifiedSince);

            entity.CreatedAt = entity.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = updatedBy;
        }
    }
}
