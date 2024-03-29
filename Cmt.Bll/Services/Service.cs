﻿using System;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.DTOs;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Cmt.Dal.Interfaces.Repositories;
using System.Threading.Tasks;

namespace Cmt.Bll.Services
{
    public abstract class Service
    {
        protected readonly IUnitOfWork UnitOfWork;

        protected Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected void CheckUpdatedAt<TId, TEntity>(Dto<TId> dto, TEntity entity)
            where TEntity : Entity<TId>
        {
            if (dto.UpdatedAt > entity.UpdatedAt)
            {
                return;
            }

            throw new CmtException(CmtErrorCodes.LastModified);
        }

        protected async Task<TEntity> GetRequiredEntity<TEntity, TId>(
            IRepository<TEntity> repository,
            TId id)
        {
            var entity = await repository.GetAsync(id);

            return entity ?? throw new CmtException(CmtErrorCodes.NotFound);
        }

        protected void UpdateEntity<TId, TEntity>(Dto<TId> dto, TEntity entity)
            where TEntity: Entity<TId>
        {
            CheckUpdatedAt(dto, entity);

            var source = Mapper.Map<TEntity>(dto);
            CopyProperties<TId, TEntity> (source, entity);
            entity.UpdatedAt = DateTime.UtcNow;
        }

        protected void CopyProperties<TId, TEntity>(
            TEntity source,
            TEntity destination)
            where TEntity : Entity<TId>
        {
            var type = source.GetType();
            var sourceProperties = GetProps(type).ToList();

            var rootEntityProps = GetRootEntityProperties<TId>();

            sourceProperties = sourceProperties
                .Where(x => !rootEntityProps.Contains(x.Name))
                .ToList();

            sourceProperties.ForEach(x => x.SetValue(destination, x.GetValue(source)));
        }

        private IEnumerable<PropertyInfo> GetProps(Type type)
        {
            return type
                .GetProperties()
                .Where(x => x.CanRead && x.CanWrite);
        }

        private IList<string> GetRootEntityProperties<TId>()
        {
            return GetProps(typeof(Entity<TId>))
                    .Select(x => x.Name)
                    .ToList();
        }
    }
}
