using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.ClienteQueries.ObterClientePorId
{
    public class ObterClientePorIdQueries:IRequest<ClienteViewModel>
    {
        public ObterClientePorIdQueries(int id)
        {
            Id = id;
        }

        public int Id { get; set; }    
    }
}