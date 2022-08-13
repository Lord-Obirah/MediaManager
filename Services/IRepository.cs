using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MediaManager.Entities;
using MediaManager.Helpers;

namespace MediaManager.Services
{
    public interface IRepository<TData> where TData : IID
    {
        TData GetEntity(Guid entityId);
        IQueryable<TData> GetEntities(QueryStringParameters queryStringParameters);
        IQueryable<TData> GetEntities(IEnumerable<Guid> entityIds, QueryStringParameters queryStringParameters);
        void AddEntity(TData entity);
        void DeleteEntity(TData entity);
        void UpdateEntity(TData entity);
        bool Exists(Guid entityId);
        bool Save();
    }
}