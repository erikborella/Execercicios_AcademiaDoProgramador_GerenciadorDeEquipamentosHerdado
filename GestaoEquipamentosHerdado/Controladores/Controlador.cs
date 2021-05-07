using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentosHerdado.Controladores
{
    class Controlador
    {
        public static string SUCESSO_OPERACAO = "SUCESSO_OPERACAO";

        private object[] registros;

        protected Controlador(int capacidadeDeRegistros)
        {
            registros = new object[capacidadeDeRegistros];
        }

        protected string RegistrarRegistro(object registro)
        {
            int posicaoVazia = PegarPosicaoVazia();

            if (posicaoVazia == -1)
                return "Não tem mais espaço para registrar";

            registros[posicaoVazia] = registro;
            return SUCESSO_OPERACAO;
        }

        protected string EditarRegistro(object novoRegistro)
        {
            int posicaoDoRegistro = PegarPosicaoDoRegistro(novoRegistro);

            if (posicaoDoRegistro == -1)
                return "Registro não encontrado";

            registros[posicaoDoRegistro] = novoRegistro;
            return SUCESSO_OPERACAO;
        }

        protected string ExcluirRegistro(object registro)
        {
            int posicaDoRegistro = PegarPosicaoDoRegistro(registro);

            if (posicaDoRegistro == -1)
                return "Registro não encontrado";

            registros[posicaDoRegistro] = null;
            return SUCESSO_OPERACAO;
        }

        protected object[] SelecionarTodosRegistros()
        {
            object[] registrosSelecionados = new object[QuantidadeDeRegistros()];

            int count = 0;

            foreach (object registro in registros)
            {
                if (registro != null)
                {
                    registrosSelecionados[count] = registro;
                    count++;
                }
            }

            return registrosSelecionados;
        }

        #region metodo privados
        private int PegarPosicaoVazia()
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registros[i] == null)
                    return i;
            }

            return -1;
        }

        private int PegarPosicaoDoRegistro(object registro)
        {
            for (int i = 0; i < registros.Length; i++)
            {
                if (registro.Equals(registros[i]))
                    return i;
            }

            return -1;
        }

        private int QuantidadeDeRegistros()
        {
            int count = 0;

            foreach (object registro in registros)
            {
                if (registro != null)
                    count++;
            }

            return count;
        }
        #endregion
    }
}
