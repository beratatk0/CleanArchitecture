using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Models;
using Clean.Core.Repositories;
using Clean.Core.Services;
using Clean.Core.UnitOfWorks;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Service.Services
{
    public class ServiceWithDto<Entity, Dto> : IServiceWithDto<Entity, Dto> where Entity : BaseEntity where Dto : class
    {

        private readonly IGenericRepository<Entity> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public ServiceWithDto(IMapper mapper, IUnitOfWork unitOfWork, IGenericRepository<Entity> repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CustomResponseDTO<Dto>> AddAsync(Dto dto)
        {
            Entity newEntity = _mapper.Map<Entity>(dto);
            await _repository.AddAsync(newEntity);
            await _unitOfWork.CommitASync();
            var newDto = _mapper.Map<Dto>(dto);
            return CustomResponseDTO<Dto>.Success(StatusCodes.Status200OK, newDto);

        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dto)
        {
            var newEntities = _mapper.Map<IEnumerable<Entity>>(dto);
            await _repository.AddRangeAsync(newEntities);
            await _unitOfWork.CommitASync();
            var newDtos = _mapper.Map<IEnumerable<Dto>>(dto);
            return CustomResponseDTO<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, newDtos);
        }

        public async Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
        {
            bool anyEntity = await _repository.AnyAsync(expression);
            return CustomResponseDTO<bool>.Success(StatusCodes.Status200OK, anyEntity);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> GetAllAsync()
        {
            var entities = _repository.GetAll();
            var dto = _mapper.Map<IEnumerable<Dto>>(entities);
            return CustomResponseDTO<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<CustomResponseDTO<Dto>> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = _mapper.Map<Dto>(entity);
            return CustomResponseDTO<Dto>.Success(StatusCodes.Status200OK, dto);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Remove(entity);
            await _unitOfWork.CommitASync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> ids)
        {
            var entities = await _repository.Where(x => ids.Contains(x.Id)).ToListAsync();
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitASync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);

        }

        public async Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(Dto dto)
        {
            var entity = _mapper.Map<Entity>(dto);
            _repository.Update(entity);
            await _unitOfWork.CommitASync();
            return CustomResponseDTO<NoContentDTO>.Success(StatusCodes.Status204NoContent);
        }

        public async Task<CustomResponseDTO<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
        {
            var entities = await _repository.Where(expression).ToListAsync();
            var dtos = _mapper.Map<IEnumerable<Dto>>(entities);
            return CustomResponseDTO<IEnumerable<Dto>>.Success(StatusCodes.Status200OK, dtos);

        }
    }
}
