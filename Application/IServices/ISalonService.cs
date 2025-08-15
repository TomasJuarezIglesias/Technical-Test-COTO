using Application.Dtos;
using Domain.Entities;

namespace Application.IServices
{
    public interface ISalonService
    {
        Task<ApiResponse<IEnumerable<SalonDto>>> GetAll();
    }
}
