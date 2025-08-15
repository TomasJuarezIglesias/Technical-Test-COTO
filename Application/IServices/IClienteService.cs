using Application.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IClienteService
    {
        Task<ApiResponse<IEnumerable<ClienteDto>>> GetAll();
    }
}
