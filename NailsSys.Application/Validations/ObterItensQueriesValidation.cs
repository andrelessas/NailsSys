using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens;

namespace NailsSys.Application.Validations
{
    public class ObterItensQueriesValidation:AbstractValidator<ObterItensQueries>
    {
        public ObterItensQueriesValidation()
        {
            RuleFor(x=>x.IdAgendamento)
                .NotNull().WithMessage("Necessário informar o id Agendamento para obter os itens.")
                .NotEmpty().WithMessage("Necessário informar o id Agendamento para obter os itens.")
                .GreaterThan(0).WithMessage("O id Agendamento deve ser maior qur 0.");
            
            RuleFor(x=>x.Page)
                .NotNull().WithMessage("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.")
                .NotEmpty().WithMessage("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.")
                .GreaterThan(0).WithMessage("Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.");
        }
    }
}