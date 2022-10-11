using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.AtendimentoCommands.NovoAtendimento;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class NovoAtendimentoCommandValidation:AbstractValidator<NovoAtendimentoCommand>
    {
        public NovoAtendimentoCommandValidation()
        {
            RuleFor(x=>x.IdCliente)
                .NotEmpty().WithMessage(MensagensAtendimento.IdClienteNullVazio)
                .NotNull().WithMessage(MensagensAtendimento.IdClienteNullVazio)
                .GreaterThan(0).WithMessage(MensagensAtendimento.IdClienteMaiorQueZero);
                
            RuleFor(x=>x.IdFormaPagamento)
                .NotEmpty().WithMessage(MensagensAtendimento.IdFormaPgtoNullVazio)
                .NotNull().WithMessage(MensagensAtendimento.IdFormaPgtoNullVazio)
                .GreaterThan(0).WithMessage(MensagensAtendimento.IdFormaPgtoMaiorQueZero);
            
            RuleFor(x=>x.DataAtendimento)
                .NotEmpty().WithMessage(MensagensAtendimento.DataAtendimentoNullVazio)
                .NotNull().WithMessage(MensagensAtendimento.DataAtendimentoNullVazio);
    
            RuleFor(x=>x.InicioAtendimento)
                .NotEmpty().WithMessage(MensagensAtendimento.InicioAtendimentoNullVazio)
                .NotNull().WithMessage(MensagensAtendimento.InicioAtendimentoNullVazio);
            
            RuleFor(x=>x.TerminoAtendimento)
                .NotEmpty().WithMessage(MensagensAtendimento.TerminoAtendimentoNullVazio)
                .NotNull().WithMessage(MensagensAtendimento.TerminoAtendimentoNullVazio);
        }
    }
}