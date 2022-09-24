using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NailsSys.Core.Notificacoes
{
    public static class MensagensPaginacao
    {
        public static string PageNullVazio { get; } = 
            "Necessário informar a quantidade de páginas para que seja realizada a paginação dos dados.";
        public static string PageMaiorQueZero { get; } = 
            "A quantidade de páginas deve ser maior que zero.";
    }

    public static class MensagensItensAgendamento
    {
        public static string IdAgendamentoNullVazio { get; } =
            "Necessário informar o id Agendamento para obter os itens.";
        public static string IdAgendamentoMaiorQueZero { get; } =
            "O id Agendamento deve ser maior qur 0.";
        public static string ItemNullVazio { get; } =
            "Necessário informar o Item.";
        public static string ItemMaiorQueZero { get; } =
            "O Item deve ser maior que 0.";
        public static string QuantidadeNullVazio { get; } =
            "Necessário informar a quantidade do item.";
        public static string QuantidadeMaiorQueZero { get; } =
            "A Quantidade deve ser maior que 0.";
        public static string IdProdutoNullVazio { get; } =
            "Necessário informar o Id do Produto.";
        public static string IdProdutoMaiorQueZero { get; } =
            "Id Produto inválido, o Id Produto deve ser maior que 0.";

    }

    public static class MensagensProduto
    {
        public static string IdProdutoNullVazio { get; }
            = "Necessário informar o Id do produto.";
        public static string IdProdutoNaoInformadoParaBloqueioProduto { get; }
            = "Necessário informar o Id do produto que será descontinuado.";
        public static string IdProdutoMaiorQueZero { get; }
            = "O Id do produto deve ser maior que 0.";
        public static string DescricaoNullVazio { get; }
            = "Informe a descrição do produto.";
        public static string LimiteTamanhoDescricao { get; }
            = "A descrição do produto deve ter no máximo 50 caracteres.";
        public static string TipoProdutoNullVazio { get; }
            = "Necessário informar o tipo do produto.";
        public static string ValidarTipoProduto { get; }
            = "O tipo do produto deve ser S - Serviço ou P - Produto.";
        public static string PrecoNullVazio { get; }
            = "Informe o preço de venda do produto.";
        public static string PrecoMaiorQueZero { get; }
            = "O preço do produto deve ser maior que zero.";
    }
    
    public static class MensagensAgendamento
    {
        public static string ClienteNullVazio { get; }
            = "Necessário informar o Cliente que será atendido.";
        public static string DataAtendimentoNullVazia { get; }
            = "Necessário informar uma Data válida.";
        public static string DataAtendimentoInvalida { get; }
            = "A Data informada é inválida.";
        public static string HorarioNullVazio { get; }
            = "Necessário informar um horário válido.";
        public static string HorarioInvalido { get; }
            = "Horário informado é inválido.";
        public static string TerminoAtendimentoMaiorQueInicioDoAtendimento { get; }
            = "O término do agendamento não pode ser maior que o inicio do agendamento."; 
        public static string IdAgendamentoNaoInformadoCancelarAgendamento { get; }
            = "Para cancelar o agendamento, é necessário informar o Id do Agendamento."; 

    }

    public static class MensagensCliente
    {
        public static string NomeClienteNullVazio { get; }
            = "Necessário informar o nome do cliente.";
        public static string NomeClienteQuantidadeCaracteresSuperiorAoLimite { get; }
            = "O nome do cliente deve ter no máximo 50 caracteres.";
        public static string TelefoneInvalido { get; }
            = "Informe um telefone válido.";
        public static string IdClienteNaoInformadoAoBloquearCLiente { get; }
            = "Para bloquear o cliente, é necessário informar o Id do Cliente válido.";
    }

    public static class MensagensLogin
    {
        public static string LoginNullVazio { get; }
            = "Necessário informar o Id ou Login do usuário para acessar o sistema.";
        public static string SenhaNullVazio { get; }
            = "Necessário informar a senha de acesso ao sistema.";        
    }

    public static class MensagensUsuario
    {
        public static string NomeCompletoNullVazio { get; }    
            = "Necessário informar o nome completo.";
        public static string NomeCompletoCurtoOuLongo { get; }    
            = "O nome do usuário deve conter no mínimo 5 caracteres e no máximo 70 caracteres.";
        public static string LoginNullVazio { get; }    
            = "Necessário informar o login do usuário.";
        public static string LoginCurtoOuLongo { get; }    
            = "O login do usuário deve conter no mínimo 5 caracteres e no máximo 15 caracteres.";
        public static string SenhaNullVazio { get; }    
            = "Necessário informar a senha de acesso ao sistema.";
        public static string SenhaFraca { get; }    
            = "Senha deve conter pelo menos 8 caracteres, um número, uma letra maiúscula, uma minúscula, e um caractere especial";
        public static string CargoNullVazio { get; }    
            = "Necessário informar o cargo do usuário.";
        public static string CargoInvalido { get; }    
            = "Cargo do usuário inválido, o cargo deve ser Adminitrador, Gerente ou Atendente.";
    }
}