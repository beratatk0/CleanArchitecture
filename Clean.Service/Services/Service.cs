using Clean.Core.Repositories;
using Clean.Core.Services;
using Clean.Core.UnitOfWorks;
using Clean.Service.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IUnitOfWork unitOfWork, IGenericRepository<T> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<T> AddAsync(T item)
        {
            await _repository.AddAsync(item);
            await _unitOfWork.CommitASync();
            return item;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> items)
        {
            await _repository.AddRangeAsync(items);
            await _unitOfWork.CommitASync();
            return items;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct = await _repository.GetByIdAsync(id);
            if (hasProduct == null)
            {
                throw new NotFoundException($"{typeof(T).Name} not found");
            }
            return hasProduct;
        }

        public async Task RemoveAsync(T item)
        {
            _repository.Remove(item);
            await _unitOfWork.CommitASync();
        }

        public async Task RemoveRangeAsync(IEnumerable<T> items)
        {
            _repository.RemoveRange(items);
            await _unitOfWork.CommitASync();
        }

        public async Task UpdateAsync(T item)
        {
            _repository.Update(item);
            await _unitOfWork.CommitASync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
