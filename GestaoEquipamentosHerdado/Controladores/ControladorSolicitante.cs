using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Dominios;

namespace GestaoEquipamentosHerdado.Controladores
{
    class ControladorSolicitante : Controlador
    {
        public ControladorSolicitante(int capacidadeDeRegistros) : base(capacidadeDeRegistros)
        {
        }

        public string RegistrarSolicitante(string nome, string email, int numeroTelefone)
        {
            Solicitante solicitante = new Solicitante(nome, email, numeroTelefone);

            string mensagemValidacaoSolicitante = solicitante.ValidarDados();

            if (mensagemValidacaoSolicitante != Solicitante.SOLICITANTE_VALIDO)
                return mensagemValidacaoSolicitante;

            return RegistrarRegistro(solicitante);
        }

        public string EditarSolicitante(int idSelecionado, string nome, string email, int numeroTelefone)
        {
            Solicitante novoSolicitante = new Solicitante(idSelecionado, nome, email, numeroTelefone);

            string mensagemValidacaoSolicitante = novoSolicitante.ValidarDados();

            if (mensagemValidacaoSolicitante != Solicitante.SOLICITANTE_VALIDO)
                return mensagemValidacaoSolicitante;

            return EditarRegistro(novoSolicitante);
        }

        public string ExcluirSolicitante(int idSelecionado)
        {
            return ExcluirRegistro(new Solicitante(idSelecionado));
        }

        public Solicitante[] SelecionarTodosSolicitantes()
        {
            object[] registros = SelecionarTodosRegistros();
            Solicitante[] solicitantes = new Solicitante[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                solicitantes[i] = (Solicitante)registros[i];
            }

            return solicitantes;
        }

        public Solicitante SelecionarSolicitantePorId(int id)
        {
            Solicitante[] solicitantes = SelecionarTodosSolicitantes();

            foreach(Solicitante solicitante in solicitantes)
            {
                if (solicitante.Id == id)
                    return solicitante;
            }

            return null;
        }
    }
}
