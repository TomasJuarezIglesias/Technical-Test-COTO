using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservaService
    {
        Task<ApiResponse<IEnumerable<ReservaDto>>> GetByDate(DateTime fecha);
        Task<ReservaDto> Create(ReservaCreateDto reserva);
    }
}