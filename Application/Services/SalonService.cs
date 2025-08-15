using Application.Dtos;
using Application.IServices;
using AutoMapper;
using Domain.Entities;
using Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SalonService(IRepository<Salon> _salonRepository, IMapper _mapper) : ISalonService
    {
        public async Task<ApiResponse<IEnumerable<SalonDto>>> GetAll()
        {
            var salones = await _salonRepository.GetAllAsync();
            var salonesMapped = _mapper.Map<IEnumerable<SalonDto>>(salones);
            return ApiResponse<IEnumerable<SalonDto>>.SuccessResponse(salonesMapped);
        }
    }
}
