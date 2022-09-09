using NailsSys.Core.Entities;
using Xunit;

namespace NailsSys.UnitsTests.Core.Entities
{
    public class AgendamentoTests
    {
        private DateTime _horario;
        private Agendamento _agendamento;

        public AgendamentoTests()
        {
            _horario = new DateTime(2022,9,7,8,0,0);
            _agendamento = new Agendamento(1, _horario, _horario, _horario.AddHours(2));    
        }
        [Fact]
        public void TestAlterarAgendamento()
        {
            //Act
            _agendamento.AlterarAgendamento(2, _horario, _horario.AddHours(1), _horario.AddHours(3));
            //Assert
            Assert.True(_agendamento.IdCliente == 2);
            Assert.True(_agendamento.InicioPrevisto == _horario.AddHours(1));
            Assert.True(_agendamento.TerminoPrevisto == _horario.AddHours(3));
        }

        [Fact]
        public void TestCancelarAgendamento()
        {        
            //Act
            _agendamento.CancelarAgendamento();
            //Assert
            Assert.True(_agendamento.Cancelado);
            Assert.False(_agendamento.AtendimentoRealizado);
        }
    }
}