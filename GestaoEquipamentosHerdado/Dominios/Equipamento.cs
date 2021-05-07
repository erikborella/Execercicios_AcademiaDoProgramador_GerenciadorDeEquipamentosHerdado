using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Id;

namespace GestaoEquipamentosHerdado.Dominios
{
    class Equipamento
    {
        public static string EQUIPAMENTO_VALIDO = "EQUIPAMENTO_VALIDO";

        private int id;
        private string nome;
        private double preco;
        private DateTime dataFabricacao;
        private string fabricante;

        public Equipamento(string nome, double preco, DateTime dataFabricacao, string fabricante)
        {
            this.id = GeradorId.PegarProximoId(this);

            this.nome = nome;
            this.preco = preco;
            this.dataFabricacao = dataFabricacao;
            this.fabricante = fabricante;
        }

        public Equipamento(int id, string nome, double preco, DateTime dataFabricacao, string fabricante)
        {
            this.id = id;
            this.nome = nome;
            this.preco = preco;
            this.dataFabricacao = dataFabricacao;
            this.fabricante = fabricante;
        }

        public Equipamento(int id)
        {
            this.id = id;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public double Preco { get => preco; set => preco = value; }
        public DateTime DataFabricacao { get => dataFabricacao; set => dataFabricacao = value; }
        public string Fabricante { get => fabricante; set => fabricante = value; }

        public override bool Equals(object obj)
        {
            Equipamento e = (Equipamento)obj;

            return (e != null && e.Id == this.id);
        }

        public string ValidarDados()
        {
            string mensagemValidacao = "";

            if (string.IsNullOrEmpty(Nome))
                mensagemValidacao += "O Nome precisa ser informado\n";

            if (Nome.Length < 6)
                mensagemValidacao += "O Nome precisa ter no minimo 6 caracteres\n";

            if (DataFabricacao > DateTime.Now)
                mensagemValidacao += "A data de fabricação não pode ser no futuro\n";

            if (string.IsNullOrEmpty(Fabricante))
                mensagemValidacao += "O fabricante precisa ser informado\n";

            if (string.IsNullOrEmpty(mensagemValidacao))
                mensagemValidacao = Equipamento.EQUIPAMENTO_VALIDO;

            return mensagemValidacao;
        }
    }
}
