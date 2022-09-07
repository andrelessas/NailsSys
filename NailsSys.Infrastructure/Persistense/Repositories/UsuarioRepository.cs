using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NailsSys.Core.Entities;
using NailsSys.Core.Interfaces;
using NailsSys.Data.Repository;
using NailsSys.Infrastructure.Context;

namespace NailsSys.Infrastructure.Persistense.Repositories
{
    public class UsuarioRepository:Repository<Usuario>,IUsuarioRepository
    {
        public UsuarioRepository(NailsSysContext context)
            :base(context)
        {
            
        }

        public async Task<Usuario> ObterUsuarioPorIdLoginSenha(int Id, string login, string senhaHash)
        {
            return await _context.Usuario.FirstOrDefaultAsync(u => (u.Id == Id || u.Login == login) && u.Senha == senhaHash);
        }
    }
}