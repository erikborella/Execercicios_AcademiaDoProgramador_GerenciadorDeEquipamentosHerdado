using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Controladores;
using GestaoEquipamentosHerdado.Dominios;

namespace GestaoEquipamentosHerdado.Telas
{
    class TelaSolicitante : TelaBase
    {
        private ControladorSolicitante controladarSolicitante;
        private ControladorChamado controladorChamado;

        public TelaSolicitante(ControladorSolicitante controladarSolicitante, ControladorChamado controladorChamado)
            : base("Tela de solicitantes")
        {
            this.controladarSolicitante = controladarSolicitante;
            this.controladorChamado = controladorChamado;
        }

        public override void RegistrarRegistro()
        {
            ConfigurarTela("Registrar Solicitante");

            string nome, email;
            int numeroTelefone;

            LerDadosSolicitante(out nome, out email, out numeroTelefone);

            string mensagemValidacao = controladarSolicitante.RegistrarSolicitante(nome, email, numeroTelefone);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
                ApresentarMensagem("Solicitante registrado com sucesso!", TiposMensagem.MENSAGEM_SUCESSO);
            else
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);

            Pausar();
        }

        public void MostrarRegistros()
        {
            Solicitante[] solicitantes = controladarSolicitante.SelecionarTodosSolicitantes();
            
            if (solicitantes.Length == 0)
            {
                ApresentarMensagem("Nenhum solicitante registrado!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string template = "{0, -3} | {1, -20} | {2, -20} | {3, -10}";

            Console.WriteLine(template,
                "Id", "Nome", "Email", "Telefone");
            Console.WriteLine();

            foreach(Solicitante solicitante in solicitantes)
            {
                Console.WriteLine(template,
                    solicitante.Id, solicitante.Nome, 
                    solicitante.Email, solicitante.NumeroTelefone);
            }

            Console.WriteLine();
        }

        public override void VisualizarRegistros()
        {
            MostrarRegistros();
            Pausar();
        }

        public override void EditarRegistro()
        {
            ConfigurarTela("Editar Solicitante");

            if (controladarSolicitante.SelecionarTodosSolicitantes().Length == 0)
            {
                ApresentarMensagem("Nao tem Solicitante registrado!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string nome, email;
            int numeroTelefone;

            LerDadosSolicitante(out nome, out email, out numeroTelefone);

            Console.Clear();

            MostrarRegistros();
            Console.Write("\nDigite o Id do solicitante que deseja editar: ");
            int id = Leitores.LerInt();

            string mensagemValidacao = controladarSolicitante.EditarSolicitante(id, nome, email, numeroTelefone);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
                ApresentarMensagem("Solicitante Editado com sucesso!", TiposMensagem.MENSAGEM_SUCESSO);
            else
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);

            Pausar();
        }

        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluir Solicitante");

            MostrarRegistros();
            Console.Write("\nDigite o Id do solicitante que deseja excluir: ");
            int id = Leitores.LerInt();

            if (controladorChamado.ChamadoTemSolicitante(id))
            {
                ApresentarMensagem("Este Solicitante ainda tem chamados abertos, resolva todos para poder exclui-lo",
                    TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string mensagemValidacao = controladarSolicitante.ExcluirSolicitante(id);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
                ApresentarMensagem("Solicitante excluido com sucesso!", TiposMensagem.MENSAGEM_SUCESSO);
            else
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);

            Pausar();
        }


        #region metodo privados
        private void LerDadosSolicitante(out string nome, out string email, out int numeroTelefone)
        {
            Console.Write("Digite o nome do solicitante: ");
            nome = Console.ReadLine();

            Console.Write("Digite o email do solicitante: ");
            email = Console.ReadLine();

            Console.Write("Digite o numero de telefone do Solicitante: ");
            numeroTelefone = Leitores.LerInt();
        }
        #endregion
    }
}
