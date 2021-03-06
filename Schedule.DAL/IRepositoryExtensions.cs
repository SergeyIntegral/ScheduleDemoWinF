﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Schedule.DAL
{
    public static class IRepositoryExtensions
    {
        public static IQueryable<T> GetAll<T, TProperty>(this IRepository<T> repository,
                                                          Expression<Func<T, TProperty>> path)
            where T : class, IDomainObject
        {
            var query = repository.GetAll();
            var dbSet = query as IDbSet<T>;

            if (dbSet != null)
            {
                query = dbSet.Include(path);
            }

            return query;
        }

        public static IQueryable<T> GetAll<T, TProperty1, TProperty2>(this IRepository<T> repository,
                                                                        Expression<Func<T, TProperty1>> path1,
                                                                        Expression<Func<T, TProperty2>> path2)
            where T : class, IDomainObject
        {
            var query = repository.GetAll();
            var dbSet = query as IDbSet<T>;

            if (dbSet != null)
            {
                query = dbSet
                    .Include(path1)
                    .Include(path2);
            }

            return query;
        }

        public static IQueryable<T> GetAll<T, TProperty1, TProperty2, TProperty3>(this IRepository<T> repository,
                                                                        Expression<Func<T, TProperty1>> path1,
                                                                        Expression<Func<T, TProperty2>> path2,
                                                                        Expression<Func<T, TProperty3>> path3)
            where T : class, IDomainObject
        {
            var query = repository.GetAll();
            var dbSet = query as IDbSet<T>;

            if (dbSet != null)
            {
                query = dbSet
                    .Include(path1)
                    .Include(path2)
                    .Include(path3);
            }

            return query;
        }

        public static IQueryable<T> GetAll<T>(this IRepository<T> repository, params string[] paths)
            where T : class, IDomainObject
        {
            var query = repository.GetAll();
            var dbSet = query as IDbSet<T>;

            if (dbSet != null && !paths.IsNullOrEmpty())
            {
                foreach (string path in paths)
                {
                    query = dbSet.Include(path);
                }
            }

            return query;
        }
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }
    }
}
