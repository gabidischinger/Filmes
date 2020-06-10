using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Application
{
    public interface IEntityCrudHandler<T>
    {
        Task<int> Inserir(T entity);

        Task<int> Alterar(int id, T entity, int userID);

        Task<int> Remover(int id, int userID);

        Task<T[]> Listar(int userID);

        Task<T> ObterUm(int id, int userID);
    }
}
