using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;

namespace NailsSys.Application.Queries.UsuarioQueries.ObterUsuarios
{
    public class ObterUsuariosQueries:IRequest<IEnumerable<UsuarioViewModel>>
    {
        
    }
}