using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Controladores;
using GestaoEquipamentosHerdado.Dominios;

namespace GestaoEquipamentosHerdado.Telas
{
    class TelaChamado : TelaBase
    {
        private ControladorChamado controladorChamado;
        private ControladorEquipamento controladorEquipamento;
        private ControladorSolicitante controladarSolicitante;

        private TelaEquipamento telaEquipamento;
        private TelaSolicitante telaSolicitante;

        public TelaChamado(ControladorChamado controladorChamado, ControladorEquipamento controladorEquipamento, 
            ControladorSolicitante controladarSolicitante,
            TelaEquipamento telaEquipamento, TelaSolicitante telaSolicitante) : base("Tela de Chamados")
        {
            this.controladorChamado = controladorChamado;
            this.controladorEquipamento = controladorEquipamento;
            this.controladarSolicitante = controladarSolicitante;
            this.telaEquipamento = telaEquipamento;
            this.telaSolicitante = telaSolicitante;
        }

        public override void RegistrarRegistro()
        {
            ConfigurarTela("Registrar Chamado");

            if (NaoTemEquipamentoRegistrado())
            {
                ApresentarMensagem("Nenhum Equipamento registrado, por favor registre algum!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            if (NaoTemSolicitanteRegistrado())
            {
                ApresentarMensagem("Nenhum Solicitante registrado, por favor registre algum!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string titulo, descricao;
            int equipamentoId, solicitanteId;

            LerDadosChamado(out titulo, out descricao, out equipamentoId, out solicitanteId);

            string mensagemValidacao = controladorChamado.RegistrarChamado(titulo, descricao, equipamentoId, solicitanteId);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
            {
                ApresentarMensagem("Chamado registrado com sucesso", TiposMensagem.MENSAGEM_SUCESSO);
            }
            else
            {
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);
            }

            Pausar();
        }

        public override void EditarRegistro()
        {
            ConfigurarTela("Editar Chamado");

            if (NaoTemChamadoRegistrado())
            {
                ApresentarMensagem("Nenhum Chamado registrado!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string titulo, descricao;
            int equipamentoId, solicitanteId;

            LerDadosChamado(out titulo, out descricao, out equipamentoId, out solicitanteId);

            Console.Clear();

            MostrarRegistros();
            Console.Write("\nDigite o Id do chamado que deseja editar: ");
            int id = Leitores.LerInt();

            string mensagemValidacao = controladorChamado.EditarChamado(id, titulo, descricao, equipamentoId, solicitanteId);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
            {
                ApresentarMensagem("Chamado registrado com sucesso", TiposMensagem.MENSAGEM_SUCESSO);
            }
            else
            {
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);
            }

            Pausar();
        }

        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluir Chamado");

            MostrarRegistros();
            Console.Write("\nDigite o Id do chamado que deseja editar: ");
            int id = Leitores.LerInt();

            string mensagemValidacao = controladorChamado.ExcluirChamado(id);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
            {
                ApresentarMensagem("Chamado Excluido com sucesso!", TiposMensagem.MENSAGEM_SUCESSO);
            }
            else
            {
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);
            }

            Pausar();
        }

        public void MostrarRegistros() 
        {
            Chamado[] chamados = controladorChamado.SelecionarTodosChamados();

            if (chamados.Length == 0)
            {
                ApresentarMensagem("Nenhum Chamado registrado!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string template = "{0,-3} | {1,-20} | {2, -20} | {3, -15} | {4,-20} | {5, -20}";

            Console.WriteLine(template,
                "Id", "Titulo", "Descricao", "Dias Abertos", "Equipamento", "Solicitante");
            Console.WriteLine();

            foreach (Chamado chamado in chamados)
            {
                Console.WriteLine(template,
                    chamado.Id, chamado.Titulo, chamado.Descricao, chamado.DiasAbertos, 
                    chamado.Equipamento.Nome, chamado.Solicitante.Nome);
            }

            Console.WriteLine();
        }

        public override void VisualizarRegistros()
        {
            MostrarRegistros();
            Pausar();
        }

        #region metodo privados
        private void LerDadosChamado(out string titulo, out string descricao, out int equipamentoId, out int solicitanteId)
        {
            Console.Write("Digite o titulo do chamado: ");
            titulo = Console.ReadLine();

            Console.Write("Digite a descricao do chamado: ");
            descricao = Console.ReadLine();

            Console.Clear();

            telaEquipamento.MostrarRegistros();
            Console.Write("Digite o id do equipamento: ");
            equipamentoId = Leitores.LerInt();

            Console.Clear();

            telaSolicitante.MostrarRegistros();
            Console.Write("Digite o id do solicitante: ");
            solicitanteId = Leitores.LerInt();
        }

        private bool NaoTemSolicitanteRegistrado()
        {
            return controladarSolicitante.SelecionarTodosSolicitantes().Length == 0;
        }

        private bool NaoTemEquipamentoRegistrado()
        {
            return controladorEquipamento.SelecionarTodosEquipamentos().Length == 0;
        }

        private bool NaoTemChamadoRegistrado()
        {
            return controladorChamado.SelecionarTodosChamados().Length == 0;
        }
        #endregion
    }
}
