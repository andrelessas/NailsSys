using FluentValidation;
using NailsSys.Application.Commands.AgendamentoCommands.AlterarAgendamento;
using NailsSys.Core.Notificacoes;

namespace NailsSys.Application.Validations
{
    public class AlterarAgendamentoCommandValidation : AbstractValidator<AlterarAgendamentoCommand>
    {
        public AlterarAgendamentoCommandValidation()
        {
            RuleFor(x => x.IdCliente)
                .NotNull()
                .WithMessage(MensagensAgendamento.ClienteNullVazio)
                .NotEmpty()
                .WithMessage(MensagensAgendamento.ClienteNullVazio);

            RuleFor(x => x.DataAtendimento)
                .NotNull()
                .WithMessage(MensagensAgendamento.DataAtendimentoNullVazia)
                .NotEmpty()
                .WithMessage(MensagensAgendamento.DataAtendimentoNullVazia)
                .Must(ValidarData)
                .WithMessage(MensagensAgendamento.DataAtendimentoInvalida);

            RuleFor(x => x.InicioPrevisto)
                .NotNull()
                .WithMessage(MensagensAgendamento.HorarioNullVazio)
                .NotEmpty()
                .WithMessage(MensagensAgendamento.HorarioNullVazio)
                .Must(ValidarHorario)
                .WithMessage(MensagensAgendamento.HorarioInvalido);

            RuleFor(x => x.TerminoPrevisto)
                .NotNull()
                .WithMessage(MensagensAgendamento.HorarioNullVazio)
                .NotEmpty()
                .WithMessage(MensagensAgendamento.HorarioNullVazio)
                .Must(ValidarHorario)
                .WithMessage(MensagensAgendamento.DataAtendimentoInvalida);

            RuleFor(x => x)
                .Must(horario => ValidarHorario(horario.InicioPrevisto,horario.TerminoPrevisto))
                .WithMessage(MensagensAgendamento.TerminoAtendimentoMaiorQueInicioDoAtendimento);
        }
        public bool ValidarData(DateTime data)
            => data.Date > DateTime.Now.Date;
        public bool ValidarHorario(DateTime horario)
            => horario > DateTime.Now;
        public bool ValidarHorario(DateTime inicioPrevisto, DateTime terminoPrevisto)
            => terminoPrevisto > inicioPrevisto;
    }
}