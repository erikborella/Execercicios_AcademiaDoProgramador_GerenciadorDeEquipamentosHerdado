using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Dominios;

namespace GestaoEquipamentosHerdado.Controladores
{
    class ControladorChamado : Controlador
    {
        ControladorEquipamento controladorEquipamento;
        ControladorSolicitante controladarSolicitante;

        public ControladorChamado(int capacidadeDeRegistros, 
            ControladorEquipamento controladorEquipamento, ControladorSolicitante controladarSolicitante) : 
            base(capacidadeDeRegistros)
        {
            this.controladorEquipamento = controladorEquipamento;
            this.controladarSolicitante = controladarSolicitante;
        }

        public string RegistrarChamado(string titulo, string descricao, int equipamentoId, int solicitanteId)
        {
            Equipamento equipamento = controladorEquipamento.SelecionarEquipamentoPorId(equipamentoId);
            Solicitante solicitante = controladarSolicitante.SelecionarSolicitantePorId(solicitanteId);

            Chamado chamado = new Chamado(titulo, descricao, equipamento, solicitante);

            string mensagemValidacaoChamado = chamado.ValidarDados();

            if (mensagemValidacaoChamado != Chamado.CHAMADO_VALIDO)
                return mensagemValidacaoChamado;

            return RegistrarRegistro(chamado);
        }

        public string EditarChamado(int idSelecionado, string titulo, string descricao, int equipamentoId, int solicitanteId)
        {
            Equipamento equipamento = controladorEquipamento.SelecionarEquipamentoPorId(equipamentoId);
            Solicitante solicitante = controladarSolicitante.SelecionarSolicitantePorId(solicitanteId);
            Chamado chamado = new Chamado(idSelecionado, titulo, descricao, equipamento, solicitante);

            string mensagemValidacao = chamado.ValidarDados();

            if (mensagemValidacao != Chamado.CHAMADO_VALIDO)
                return mensagemValidacao;

            return EditarRegistro(chamado);
        }

        public string ExcluirChamado(int idSelecionado)
        {
            return ExcluirRegistro(new Chamado(idSelecionado));
        }

        public Chamado[] SelecionarTodosChamados()
        {
            object[] registros = SelecionarTodosRegistros();
            Chamado[] chamados = new Chamado[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                chamados[i] = (Chamado)registros[i];
            }

            return chamados;
        }

        public bool ChamadoTemEquipamento(int equipamentoId)
        {
            Chamado[] chamados = SelecionarTodosChamados();

            foreach (Chamado chamado in chamados)
            {
                if (chamado.Equipamento.Id == equipamentoId)
                    return true;
            }

            return false;
        }

        public bool ChamadoTemSolicitante(int solicitanteId)
        {
            Chamado[] chamados = SelecionarTodosChamados();

            foreach (Chamado chamado in chamados)
            {
                if (chamado.Solicitante.Id == solicitanteId)
                    return true;
            }

            return false;
        }

        public Chamado SelecionarChamadoPorId(int id)
        {
            Chamado[] chamados = SelecionarTodosChamados();

            foreach (Chamado chamado in chamados)
            {
                if (chamado.Id == id)
                    return chamado;
            }

            return null;
        }
    }
}
