using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Queries.ItemAgendamentoQueries.ObterItens;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class ObterItensQueriesValidation:AbstractValidator<ObterItensQueries>
    {
        public ObterItensQueriesValidation()
        {
            RuleFor(x=>x.IdAgendamento)
                .NotNull().WithMessage(MensagensItensAgendamento.IdAgendamentoNullVazio)
                .NotEmpty().WithMessage(MensagensItensAgendamento.IdAgendamentoNullVazio)
                .GreaterThan(0).WithMessage(MensagensItensAgendamento.IdAgendamentoMaiorQueZero);
            
            RuleFor(x=>x.Page)
                .NotNull().WithMessage(MensagensPaginacao.PageNullVazio)
                .NotEmpty().WithMessage(MensagensPaginacao.PageNullVazio)
                .GreaterThan(0).WithMessage(MensagensPaginacao.PageMaiorQueZero);
        }
    }
}