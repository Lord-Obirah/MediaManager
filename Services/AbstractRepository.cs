using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public abstract class AbstractRepository<TData> : IRepository<TData>
        where TData : IID
    {
        protected readonly MediaContext Context;
        protected readonly IPropertyMappingService PropertyMappingService;

        protected AbstractRepository(MediaContext context, IPropertyMappingService propertyMappingService)
        {
            Context = context;
            PropertyMappingService = propertyMappingService;
        }

        public abstract IQueryable<TData> GetEntities(QueryStringParameters queryStringParameters);
        public abstract IQueryable<TData> GetEntities(IEnumerable<Guid> entityIds, QueryStringParameters queryStringParameters);
        public abstract TData GetEntity(Guid entityId);
        public abstract void AddEntity(TData entity);
        public abstract void DeleteEntity(TData entity);
        public abstract void UpdateEntity(TData entity);
        public abstract bool Exists(Guid entityId);

        public bool Save()
        {
            return Context.SaveChanges() >= 0;
        }
    }
}