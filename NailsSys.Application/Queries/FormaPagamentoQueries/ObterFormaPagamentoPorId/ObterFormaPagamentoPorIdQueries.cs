using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.FormaPagamentoQueries.ObterFormaPagamentoPorId
{
    public class ObterFormaPagamentoPorIdQueries:IRequest<FormaPagamentoViewModel>
    {
        public ObterFormaPagamentoPorIdQueries(int id)
        {
            Id = id;
        }

        public int Id { get; set; }   
    }
}