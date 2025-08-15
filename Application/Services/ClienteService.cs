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
    public class ClienteService(IRepository<Cliente> _clienteRepository, IMapper _mapper) : IClienteService
    {
        public async Task<ApiResponse<IEnumerable<ClienteDto>>> GetAll()
        {
            var clientes = await _clienteRepository.GetAllAsync();
            var clientesMapped = _mapper.Map<IEnumerable<ClienteDto>>(clientes);
            return ApiResponse<IEnumerable<ClienteDto>>.SuccessResponse(clientesMapped);
        }
    }
}
