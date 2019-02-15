using System;
using AutoMapper;
using Cmt.Bll.Services.Exceptions;
using Cmt.Bll.DTOs;
using Cmt.Dal.Entities;
using Cmt.Dal.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Cmt.Bll.Services
{
    public abstract class Service
    {
        protected readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;

        protected Service(
            IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        protected TEntity CreateEntity<TId, TDto, TEntity>(TDto dto)
            where TDto : Dto<TId>
            where TEntity : Entity<TId>
        {
            var entity = Mapper.Map<TEntity>(dto);
            SetCreatedDates(entity);

            return entity;
        }

        protected void UpdateEntity<TId, TDto, TEntity>(TDto dto, TEntity dbEntity)
            where TDto: Dto<TId>
            where TEntity: Entity<TId>
        {
            var entity = Mapper.Map<TEntity>(dto);
            CopyProperties<TId, TEntity>(entity, dbEntity);
            SetUpdatedDates(entity);
        }

        protected void SetCreatedDates<TId>(Entity<TId> entity)
        {
            var now = DateTime.UtcNow;
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
        }

        protected void SetUpdatedDates<TId>(Entity<TId> entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
        }

        protected void CheckUpdatedAt(DateTime? current, DateTime db)
        {
            if (!current.HasValue || current < db)
            {
                throw new CmtException(CmtErrorCodes.LastModified);
            }
        }

        protected void CopyProperties<TId, TEntity>(
            TEntity source,
            TEntity destination,
            bool ignoreBaseEntityProps = true)
            where TEntity : Entity<TId>
        {
            var properties = GetProps(typeof(TEntity)).ToList();

            if (ignoreBaseEntityProps)
            {
                var baseProps = GetProps(typeof(Entity<TId>))
                    .Select(x => x.Name)
                    .ToList();

                properties = properties
                    .Where(x => !baseProps.Contains(x.Name))
                    .ToList();
            }

            properties.ForEach(x => x.SetValue(destination, x.GetValue(source)));
        }

        private IEnumerable<PropertyInfo> GetProps(Type type)
        {
            return type
                .GetProperties()
                .Where(x => x.CanRead && x.CanWrite);
        }
    }
}
