using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Id;

namespace GestaoEquipamentosHerdado.Dominios
{
    class Solicitante
    {
        public static string SOLICITANTE_VALIDO = "SOLICITANTE_VALIDO";

        private int id;
        private string nome;
        private string email;
        private int numeroTelefone;

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public int NumeroTelefone { get => numeroTelefone; set => numeroTelefone = value; }

        public Solicitante(string nome, string email, int numeroTelefone)
        {
            this.Id = GeradorId.PegarProximoId(this);

            this.Nome = nome;
            this.Email = email;
            this.NumeroTelefone = numeroTelefone;
        }

        public Solicitante(int id, string nome, string email, int numeroTelefone)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
            this.NumeroTelefone = numeroTelefone;
        }

        public Solicitante(int id)
        {
            this.Id = id;
        }

        public override bool Equals(object obj)
        {
            Solicitante s = (Solicitante)obj;

            return (s != null && s.Id == this.Id);
        }

        public string ValidarDados()
        {
            string mensagemValidacao = "";

            if (string.IsNullOrEmpty(Nome))
                mensagemValidacao += "O nome precisa ser informado\n";

            if (Nome.Length < 6)
                mensagemValidacao += "O nome precisa ter no minimo 6 caracteres\n";

            if (string.IsNullOrEmpty(Email))
                mensagemValidacao += "O email precisa ser informado\n";

            if (NumeroTelefone == 0)
                mensagemValidacao += "O telefone precisa ser informado\n";

            if (string.IsNullOrEmpty(mensagemValidacao))
                mensagemValidacao = SOLICITANTE_VALIDO;

            return mensagemValidacao;
        }
    }
}
