using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Controladores;
using GestaoEquipamentosHerdado.Dominios;

namespace GestaoEquipamentosHerdado.Telas
{
    class TelaEquipamento : TelaBase
    {
        private ControladorEquipamento controladorEquipamento;
        private ControladorChamado controladorChamado;

        public TelaEquipamento(ControladorEquipamento controladorEquipamento, ControladorChamado controladorChamado) 
            : base("Tela de equipamentos")
        {
            this.controladorEquipamento = controladorEquipamento;
            this.controladorChamado = controladorChamado;
        }

        public override void RegistrarRegistro()
        {
            ConfigurarTela("Registrar Equipamento");

            string nome, fabricante;
            double precoAquisicao;
            DateTime dataFabricacao;

            LerDadosEquipamento(out nome, out precoAquisicao, out dataFabricacao, out fabricante);

            string mensagemValidacao = controladorEquipamento.RegistrarEquipamento(
                nome, precoAquisicao, dataFabricacao, fabricante);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
            {
                ApresentarMensagem("Equipamento registrado com successo!", TiposMensagem.MENSAGEM_SUCESSO);
            } else
            {
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);
            }

            Pausar();
        }

        public void MostrarRegistros()
        {
            Equipamento[] equipamentos = controladorEquipamento.SelecionarTodosEquipamentos();

            if (equipamentos.Length == 0)
            {
                ApresentarMensagem("Nenhum Equipamento registrado!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string template = "{0,-3} | {1,-20} | {2,-20} | {3,-20} | {4,-20}";

            Console.WriteLine(template,
                "Id", "Nome", "Preco de Aquisicao", "Data de Fabricacao", "Fabrincante");
            Console.WriteLine();

            foreach (Equipamento equipamento in equipamentos)
            {
                Console.WriteLine(template,
                    equipamento.Id, equipamento.Nome, equipamento.Preco,
                    equipamento.DataFabricacao, equipamento.Fabricante);
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
            ConfigurarTela("Editar Equipamento");

            if (NaoTemEquipamentoRegistrado())
            {
                ApresentarMensagem("Nenhum Equipamento registrado!", TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string nome, fabricante;
            double precoAquisicao;
            DateTime dataFabricacao;

            LerDadosEquipamento(out nome, out precoAquisicao, out dataFabricacao, out fabricante);

            MostrarRegistros();
            Console.Write("\nDigite o Id do equipamento que deseja editar: ");
            int id = Leitores.LerInt();

            string mensagemValidacao = controladorEquipamento.EditarEquipamento(
                id, nome, precoAquisicao, dataFabricacao, fabricante);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
            {
                ApresentarMensagem("Equipamento Editado com sucesso!", TiposMensagem.MENSAGEM_SUCESSO);
            }
            else
            {
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);
            }

            Pausar();
        }

        public override void ExcluirRegistro()
        {
            ConfigurarTela("Excluir equipamento");

            MostrarRegistros();
            Console.Write("\nDigite o Id do equipamento que deseja editar: ");
            int id = Leitores.LerInt();

            if (controladorChamado.ChamadoTemEquipamento(id))
            {
                ApresentarMensagem("Este Equipamento ainda tem chamados abertos, resolva todos para poder exclui-lo", 
                    TiposMensagem.MENSAGEM_ERRO);
                Pausar();
                return;
            }

            string mensagemValidacao = controladorEquipamento.ExcluirEquipamento(id);

            if (mensagemValidacao == Controlador.SUCESSO_OPERACAO)
            {
                ApresentarMensagem("Equipamento Excluido com sucesso!", TiposMensagem.MENSAGEM_SUCESSO);
            } else
            {
                ApresentarMensagem(mensagemValidacao, TiposMensagem.MENSAGEM_ERRO);
            }

            Pausar();
        }

        #region metodo privados
        private void LerDadosEquipamento(out string nome, out double precoAquisicao, 
            out DateTime dataFabricacao, out string fabricante)
        {
            Console.Write("Digite o nome do equipamento: ");
            nome = Console.ReadLine();

            Console.Write("Digite o preco de aquisicao: ");
            precoAquisicao = Leitores.LerDouble();

            Console.Write("Digite a data de fabricacao: ");
            dataFabricacao = Leitores.LerData();

            Console.Write("Digite a fabricante: ");
            fabricante = Console.ReadLine();
        }

        private bool NaoTemEquipamentoRegistrado()
        {
            return controladorEquipamento.SelecionarTodosEquipamentos().Length == 0;
        }
        #endregion
    }
}
