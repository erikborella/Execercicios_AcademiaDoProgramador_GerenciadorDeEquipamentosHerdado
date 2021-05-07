using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Id;

namespace GestaoEquipamentosHerdado.Dominios
{
    class Chamado
    {
        public static string CHAMADO_VALIDO = "CHAMADO_VALIDO";

        private int id;
        private string titulo;
        private string descricao;
        private DateTime dataAbertura;
        private Equipamento equipamento;
        private Solicitante solicitante;

        public Chamado(string titulo, string descricao, Equipamento equipamento, Solicitante solicitante)
        {
            this.id = GeradorId.PegarProximoId(this);
            this.titulo = titulo;
            this.descricao = descricao;
            this.dataAbertura = DateTime.Now;
            this.equipamento = equipamento;
            this.solicitante = solicitante;
        }

        public Chamado(int id, string titulo, string descricao, Equipamento equipamento, Solicitante solicitante)
        {
            this.id = id;
            this.titulo = titulo;
            this.descricao = descricao;
            this.dataAbertura = DateTime.Now;
            this.equipamento = equipamento;
            this.solicitante = solicitante;
        }

        public Chamado(int id)
        {
            this.id = id;
        }

        public override bool Equals(object obj)
        {
            Chamado c = (Chamado)obj;
            return (c != null && c.Id == Id);
        }

        public int Id { get => id; set => id = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Descricao { get => descricao; set => descricao = value; }
        public DateTime DataAbertura { get => dataAbertura; set => dataAbertura = value; }
        public Equipamento Equipamento { get => equipamento; set => equipamento = value; }
        public Solicitante Solicitante { get => solicitante; set => solicitante = value; }

        public int DiasAbertos
        {
            get
            {
                TimeSpan diferencaDatas = DataAbertura - DateTime.Now;
                return Convert.ToInt32(diferencaDatas.TotalDays);
            }
        }

        public string ValidarDados()
        {
            string mensagemValidacao = "";

            if (string.IsNullOrEmpty(Titulo))
                mensagemValidacao += "O titulo é obrigatorio\n";

            if (string.IsNullOrEmpty(Descricao))
                mensagemValidacao += "A descricricao é obrigatorio\n";

            if (Equipamento == null)
                mensagemValidacao += "O equipamento é obrigatorio\n";

            if (solicitante == null)
                mensagemValidacao += "O solicitante é obrigatorio\n";

            if (string.IsNullOrEmpty(mensagemValidacao))
                mensagemValidacao = CHAMADO_VALIDO;

            return mensagemValidacao;
        }
    }
}
