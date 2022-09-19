using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using NailsSys.Application.ViewModels;
using NailsSys.Core.Models;

namespace NailsSys.Application.Queries.UsuarioQueries.ObterUsuarios
{
    public class ObterUsuariosQueries:IRequest<PaginationResult<UsuarioViewModel>>
    {
        public ObterUsuariosQueries(int page)
        {
            Page = page;
        }

        public int Page { get; set; }
    }
}