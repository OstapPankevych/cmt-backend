using System;
using AutoMapper;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.DTOs;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces;

namespace Cmt.Bll.Services
{
    public abstract class Service
    {
        protected readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        protected Service(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        protected void Create<TId>(Dto<TId> dto)
        {
            dto.CreatedAt = DateTime.UtcNow;
            dto.UpdatedAt = DateTime.UtcNow;
        }

        protected void Update<TId>(
            Dto<TId> dto,
            Entity<TId> dbEntity,
            DateTime? lastModified)
        {
            if (dbEntity == null)
                throw new CmtException(ErrorCodes.NotFound);

            if (!lastModified.HasValue || lastModified < dbEntity.UpdatedAt)
                throw new CmtException(ErrorCodes.LastModified);

            dto.CreatedAt = dbEntity.CreatedAt;
            dto.UpdatedAt = DateTime.UtcNow;
            dto.UpdatedBy = 11;
        }
    }
}
