using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Dominios;

namespace GestaoEquipamentosHerdado.Controladores
{
    class ControladorEquipamento : Controlador
    {
        public ControladorEquipamento(int capacidadeDeRegistros) : base(capacidadeDeRegistros)
        {
        }

        public string RegistrarEquipamento(string nome, double preco, DateTime dataFabricacao, string fabricante)
        {
            Equipamento equipamento = new Equipamento(nome, preco, dataFabricacao, fabricante);

            string mensagemValidacaoEquipamento = equipamento.ValidarDados();

            if (mensagemValidacaoEquipamento != Equipamento.EQUIPAMENTO_VALIDO)
                return mensagemValidacaoEquipamento;

            return RegistrarRegistro(equipamento);
        }

        public string EditarEquipamento(int idSelecionado, string nome, double preco, DateTime dataFabricacao, string fabricante)
        {
            Equipamento novoEquipamento = new Equipamento(idSelecionado, nome, preco, dataFabricacao, fabricante);

            string mensagemValidacao = novoEquipamento.ValidarDados();

            if (mensagemValidacao != Equipamento.EQUIPAMENTO_VALIDO)
                return mensagemValidacao;

            return EditarRegistro(novoEquipamento);
        }

        public string ExcluirEquipamento(int idSelecionado)
        {
            return ExcluirRegistro(new Equipamento(idSelecionado));
        }

        public Equipamento[] SelecionarTodosEquipamentos()
        {
            object[] registros = SelecionarTodosRegistros();
            Equipamento[] equipamentos = new Equipamento[registros.Length];

            for (int i = 0; i < registros.Length; i++)
            {
                equipamentos[i] = (Equipamento)registros[i];
            }

            return equipamentos;
        }

        public Equipamento SelecionarEquipamentoPorId(int id)
        {
            Equipamento[] equipamentos = SelecionarTodosEquipamentos();

            foreach (Equipamento equipamento in equipamentos)
            {
                if (equipamento.Id == id)
                    return equipamento;
            }

            return null;
        }
    }
}
