using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using NailsSys.Application.Commands.AgendamentoCommands.NovoAgendamento;

namespace NailsSys.Application.Validations
{
    public class NovoAgendamentoCommandValidation:AbstractValidator<NovoAgendamentoCommand>    
    {
        public NovoAgendamentoCommandValidation()
        {           
            RuleFor(x=>x.DataAtendimento)
                .NotNull()
                .NotEmpty()
                .Must(ValidarData)
                .WithMessage("Data inválida.");
            
            RuleFor(x=>x.InicioPrevisto)
                .NotNull()
                .NotEmpty()
                .Must(ValidarData)
                .WithMessage("Horário inválido.");
            
            RuleFor(x=>x.TerminoPrevisto)
                .NotNull()
                .NotEmpty()
                .Must(ValidarData)
                .WithMessage("Horário inválido.");
        }
        public bool ValidarData(DateTime data)
            => data.Date < DateTime.Now.Date;        
    }
}