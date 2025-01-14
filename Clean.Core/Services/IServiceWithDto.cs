﻿using Clean.Core.DTOs;
using Clean.Core.Models;
using System.Linq.Expressions;

namespace Clean.Core.Services
{
    public interface IServiceWithDto<Entity, Dto> where Entity : BaseEntity where Dto : class
    {
        Task<CustomResponseDTO<Dto>> GetByIdAsync(int id);

        Task<CustomResponseDTO<IEnumerable<Dto>>> GetAllAsync();

        Task<CustomResponseDTO<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression);

        Task<CustomResponseDTO<bool>> AnyAsync(Expression<Func<Entity, bool>> expression);

        Task<CustomResponseDTO<Dto>> AddAsync(Dto dto);

        Task<CustomResponseDTO<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dto);

        Task<CustomResponseDTO<NoContentDTO>> UpdateAsync(Dto dto);

        Task<CustomResponseDTO<NoContentDTO>> RemoveAsync(int id);

        Task<CustomResponseDTO<NoContentDTO>> RemoveRangeAsync(IEnumerable<int> ids);


    }
}
