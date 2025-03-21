using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rotas.Domain.Entities;
using Rotas.Domain.Exceptions;
using Rotas.Domain.Interfaces;

namespace Rotas.Domain.Services
{
    public class CadastroViagemService(IRepositoryCrud<Viagem> repository)
    {
        private readonly IRepositoryCrud<Viagem> _repository = repository;

        /// <summary>
        /// Cadastra uma nova viagem e retorna o ID gerado
        /// </summary>
        /// <param name="viagem"></param>
        /// <returns>Id (int)</returns>
        /// <exception cref="ValidacaoException"></exception>
        public async Task<int> AddViagemAsync(Viagem viagem)
        {
            var erros = new List<ValidationResult>();
            if (!Validator.TryValidateObject(viagem, new ValidationContext(viagem), erros, true))
            {
                var listaErros = erros.Select(e => e.ErrorMessage).ToList();
                var errosMsg = string.Join(", ", listaErros);

                throw new ValidacaoException(errosMsg);
            }

            return await _repository.CreateAsync(viagem);
        }

        /// <summary>
        /// Atualiza uma viagem
        /// </summary>
        /// <param name="viagem"></param>
        /// <returns>Task</returns>
        /// <exception cref="ValidacaoException"></exception>
        /// <exception cref="NaoEncontradoException"></exception>
        public async Task UpdateViagem(Viagem viagem)
        {
            if (viagem == null)
                throw new ArgumentNullException(nameof(viagem), "Objeto Viagem não pode ser nulo");

            var erros = new List<ValidationResult>();
            if (!Validator.TryValidateObject(viagem, new ValidationContext(viagem), erros, true))
            {
                StringBuilder errosMsg = new StringBuilder();
                foreach (var erro in erros)
                    errosMsg.AppendLine(erro.ErrorMessage);

                throw new ValidacaoException(errosMsg.ToString());
            }

            var viagemExistente = await _repository.GetByIdAsync(viagem.Id);
            if (viagemExistente == null)
                throw new NaoEncontradoException("Viagem não encontrada");

            await _repository.UpdateAsync(viagem);
        }

        /// <summary>
        /// Exclui uma viagem
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Task</returns>
        /// <exception cref="NaoEncontradoException"></exception>
        public async Task DeleteViagem(int id)
        {
            var viagemExistente = await _repository.GetByIdAsync(id);
            if (viagemExistente == null)
                throw new NaoEncontradoException("Viagem não encontrada");

            await _repository.DeleteAsync(id);
        }

        /// <summary>
        /// Retorna uma viagem pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Viagem</returns>
        /// <exception cref="NaoEncontradoException"></exception>
        /// <exception cref="ValidacaoException"></exception>
        public async Task<Viagem> GetByIdViagem(int id)
        {
            var viagem = await _repository.GetByIdAsync(id);
            if (viagem == null)
                throw new NaoEncontradoException("Viagem não encontrada");

            return viagem;
        }

        /// <summary>
        /// Retorna todas as viagens
        /// </summary>
        /// <returns>Viagem[]</returns>
        /// <exception cref="NaoEncontradoException"></exception>
        /// 
        public async Task<List<Viagem>> ObterViagens()
        {
            var viagens = await _repository.GetAllAsync();
            return viagens;
        }


    }
}