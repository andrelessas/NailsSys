using FluentValidation;
using NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento;

namespace NailsSys.Application.Validations
{
    public class AlterarAgendamentoCommandValidation:AbstractValidator<AlterarAgendamentoCommand>
    {
        public AlterarAgendamentoCommandValidation()
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