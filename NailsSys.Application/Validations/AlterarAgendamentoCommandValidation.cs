using FluentValidation;
using NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento;

namespace NailsSys.Application.Validations
{
    public class AlterarAgendamentoCommandValidation : AbstractValidator<AlterarAgendamentoCommand>
    {
        public AlterarAgendamentoCommandValidation()
        {
            RuleFor(x => x.IdCliente)
                .NotNull()
                .WithMessage("Necessário informar o Cliente que será atendido.")
                .NotEmpty()
                .WithMessage("Necessário informar o Cliente que será atendido.");

            RuleFor(x => x.DataAtendimento)
                .NotNull()
                .WithMessage("Necessário informar uma Data válida.")
                .NotEmpty()
                .WithMessage("Necessário informar uma Data válida.")
                .Must(ValidarData)
                .WithMessage("Data inválida.");

            RuleFor(x => x.InicioPrevisto)
                .NotNull()
                .WithMessage("Necessário informar um horário válido.")
                .NotEmpty()
                .WithMessage("Necessário informar um horário válido.")
                .Must(ValidarHorario)
                .WithMessage("Horário inválido.");

            RuleFor(x => x.TerminoPrevisto)
                .NotNull()
                .WithMessage("Necessário informar um horário válido.")
                .NotEmpty()
                .WithMessage("Necessário informar um horário válido.")
                .Must(ValidarHorario)
                .WithMessage("Horário inválido.");

            RuleFor(x => x)
                .Must(horario => ValidarHorario(horario.InicioPrevisto,horario.TerminoPrevisto))
                .WithMessage("O término do agendamento não pode ser maior que o inicio do agendamento.");
        }
        public bool ValidarData(DateTime data)
            => data.Date > DateTime.Now.Date;
        public bool ValidarHorario(DateTime horario)
            => horario > DateTime.Now;
        public bool ValidarHorario(DateTime inicioPrevisto, DateTime terminoPrevisto)
            => terminoPrevisto > inicioPrevisto;
    }
}