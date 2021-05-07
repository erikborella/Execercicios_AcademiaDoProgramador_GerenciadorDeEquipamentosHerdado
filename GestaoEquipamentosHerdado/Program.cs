using System;

using GestaoEquipamentosHerdado.Dominios;
using GestaoEquipamentosHerdado.Controladores;
using GestaoEquipamentosHerdado.Telas;


namespace GestaoEquipamentosHerdado
{
    class Program
    {
        static ControladorEquipamento controladorEquipamento = new ControladorEquipamento(100);
        static ControladorSolicitante controladarSolicitante = new ControladorSolicitante(100);
        static ControladorChamado controladorChamado = new ControladorChamado(100, controladorEquipamento, controladarSolicitante);

        static TelaPrincipal telaPrincipal = new TelaPrincipal(controladorEquipamento, controladarSolicitante, controladorChamado);

        static void Main(string[] args)
        {
            bool noMenu = true;

            while (noMenu)
            {
                Console.Clear();

                TelaBase tela = telaPrincipal.PegarTela();

                if (tela == null)
                    noMenu = false;
                else
                    ExecutarTela(tela);
            }
        }

        private static void ExecutarTela(TelaBase tela)
        {
            bool noMenu = true;

            while(noMenu)
            {
                Console.Clear();

                TelaBase.Opcoes opcao = tela.PegarOpcao();

                Console.Clear();

                switch (opcao)
                {
                    case TelaBase.Opcoes.REGISTRAR_REGISTRO:
                        tela.RegistrarRegistro();
                        break;
                    case TelaBase.Opcoes.EDITAR_REGISTROS:
                        tela.EditarRegistro();
                        break;
                    case TelaBase.Opcoes.EXCLUIR_REGISTROS:
                        tela.ExcluirRegistro();
                        break;
                    case TelaBase.Opcoes.VISUALIZAR_REGISTROS:
                        tela.VisualizarRegistros();
                        break;
                    case TelaBase.Opcoes.SAIR:
                        noMenu = false;
                        break;
                }
            }
        }
    }
}
