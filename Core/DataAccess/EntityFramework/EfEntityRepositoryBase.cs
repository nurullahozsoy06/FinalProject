using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
               where TEntity : class ,IEntity, new()
               where TContext: DbContext,new()
    {
        public void Add(TEntity entity)
        {
            // IDisposable desenini uygulayan Context nesnesi, işi bittiğinde bellekten atılması için using bloğuna alınır.
            using (TContext context = new TContext())
            {
                // Gelen nesneyi Entity Framework takibine (Tracking) dahil ediyoruz.
                var addedEntity = context.Entry(entity);

                // Nesnenin durumunu "Eklenecek" (Added) olarak işaretliyoruz. 
                // Bu, Unit of Work yapısında INSERT sorgusunun hazırlanmasını sağlar.
                addedEntity.State = EntityState.Added;

                // Yapılan tüm değişiklikleri tek bir transaction altında veritabanına yansıtırız.
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList() // select*from products
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
