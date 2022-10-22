using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.AtendimentoQueries.ObterAtendimentoPorId
{
    public class ObterAtendimentoPorIDQueries:IRequest<AtendimentoViewModel>
    {
        public ObterAtendimentoPorIDQueries(int id)
        {
            Id = id;
        }
        public int Id { get; set; }

    }
}