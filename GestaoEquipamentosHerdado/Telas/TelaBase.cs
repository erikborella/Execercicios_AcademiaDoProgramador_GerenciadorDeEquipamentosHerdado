using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentosHerdado.Telas
{
    abstract class TelaBase
    {
        protected enum TiposMensagem
        {
            MENSAGEM_NORMAL,
            MENSAGEM_SUCESSO,
            MENSAGEM_ERRO
        };

        public enum Opcoes
        {
            REGISTRAR_REGISTRO,
            VISUALIZAR_REGISTROS,
            EXCLUIR_REGISTROS,
            EDITAR_REGISTROS,
            SAIR
        };

        private readonly string titulo;

        public TelaBase(string titulo)
        {
            this.titulo = titulo;
        }

        public string Titulo => titulo;

        public abstract void RegistrarRegistro();

        public abstract void EditarRegistro();

        public abstract void ExcluirRegistro();

        public abstract void VisualizarRegistros();

        public Opcoes PegarOpcao()
        {
            ApresentarMensagem(Titulo, TiposMensagem.MENSAGEM_SUCESSO);

            while (true)
            {
                Console.Write("\n1. Registrar\n" +
                    "2. Editar\n" +
                    "3. Excluir\n" +
                    "4. Visualizar\n" +
                    "5. Voltar\n" +
                    "Digite o que deseja fazer: ");

                int opcao = Leitores.LerInt();

                if (!OpcaoEstaCorreta(opcao))
                {
                    ApresentarMensagem("Opcao Invalida!", TiposMensagem.MENSAGEM_ERRO);
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        return Opcoes.REGISTRAR_REGISTRO;
                    case 2:
                        return Opcoes.EDITAR_REGISTROS;
                    case 3:
                        return Opcoes.EXCLUIR_REGISTROS;
                    case 4:
                        return Opcoes.VISUALIZAR_REGISTROS;
                    case 5:
                        return Opcoes.SAIR;
                }
            }
        }

        protected virtual bool OpcaoEstaCorreta(int opcao)
        {
            return opcao >= 1 && opcao <= 5;
        }

        protected void ConfigurarTela(string subtitulo)
        {
            ApresentarMensagem(subtitulo, TiposMensagem.MENSAGEM_NORMAL);
            Console.WriteLine();
        }

        protected void ApresentarMensagem(string mensagem, TiposMensagem tipoMensagem)
        {
            switch (tipoMensagem)
            {
                case TiposMensagem.MENSAGEM_NORMAL:
                    Console.ResetColor();
                    break;
                case TiposMensagem.MENSAGEM_SUCESSO:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case TiposMensagem.MENSAGEM_ERRO:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

            Console.WriteLine(mensagem);
            Console.ResetColor();
        }

        protected void Pausar()
        {
            Console.Write("Digite qualquer coisa para continuar: ");
            Console.ReadLine();
        }

    }
}
