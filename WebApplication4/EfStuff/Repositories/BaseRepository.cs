using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication4.EfStuff.DbModel;

namespace WebApplication4.EfStuff.Repositories
{
    public abstract class BaseRepository<Template> where Template : BaseModel
    {
        protected WebContext _webContext;
        protected DbSet<Template> _dbSet;

        public BaseRepository(WebContext webContext)
        {
            _webContext = webContext;
            _dbSet = webContext.Set<Template>();
        }

        public virtual Template Get(long id)
        {
            return _dbSet.SingleOrDefault(x => x.Id == id);
        }

        public virtual List<Template> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual IQueryable<Template> GetAllQueryable()
        {
            return _dbSet;
        }

        public virtual void Save(Template model)
        {
            if (model.Id > 0)
            {
                _webContext.Update(model);
            }
            else
            {
                _dbSet.Add(model);
            }
            _webContext.SaveChanges();
        }

        public virtual void Remove(long id)
        {
            Remove(Get(id));
        }

        public virtual void Remove(Template model)
        {
            _dbSet.Remove(model);
            _webContext.SaveChanges();
        }

        public virtual void Remove(List<Template> models)
        {
            foreach (Template model in models)
            {
                _dbSet.Remove(model);
            }

            _webContext.SaveChanges();
        }

        public virtual int Count()
            => _dbSet.Count();

        public List<Template> GetForPagination(int perPage, int page)
            => _dbSet
            .Skip((page - 1) * perPage)
            .Take(perPage)
            .ToList();

        public IQueryable<Template> SortedBy(string sortingName, string filterString, string typeFilter, bool isDescending)
        {
            var table = Expression.Parameter(typeof(Template), "obj");
            var sortMember = Expression.Property(table, sortingName);

            var sortLambda = Expression.Lambda<Func<Template, object>>(Expression.Convert(sortMember, typeof(object)), table);

            var constName = Expression.Constant(filterString, typeof(string)); // 'good news'
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var filterMember = Expression.Property(table, typeFilter);
            var memberInObj = Expression.Convert(filterMember, typeof(object));
            var memberInString = Expression.Convert(memberInObj, typeof(string));
            var containsMethodExp = Expression.Call(memberInString, method, constName);

            var filterLambda = Expression.Lambda<Func<Template, bool>>(containsMethodExp, table);

            if (isDescending)
            {
                return _dbSet.Where(filterLambda).OrderByDescending(sortLambda);
            }
            else
            {
                return _dbSet.Where(filterLambda).OrderBy(sortLambda);
            }
        }
    }
}
