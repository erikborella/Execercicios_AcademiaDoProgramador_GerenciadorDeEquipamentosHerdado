using System;
using System.Collections.Generic;
using System.Text;

using GestaoEquipamentosHerdado.Controladores;

namespace GestaoEquipamentosHerdado.Telas
{
    class TelaPrincipal
    {
        TelaEquipamento telaEquipamento;
        TelaSolicitante telaSolicitante;
        TelaChamado telaChamado;

        public TelaPrincipal(ControladorEquipamento controladorEquipamento, ControladorSolicitante controladarSolicitante,
            ControladorChamado controladorChamado)
        {
            this.telaEquipamento = new TelaEquipamento(controladorEquipamento, controladorChamado);
            this.telaSolicitante = new TelaSolicitante(controladarSolicitante, controladorChamado);
            this.telaChamado = new TelaChamado(controladorChamado, controladorEquipamento, controladarSolicitante,
                telaEquipamento, telaSolicitante);
        }

        public TelaBase PegarTela()
        {
            while (true)
            {
                Console.Write("1. Tela de Equipamentos\n" +
                    "2. Tela de Solicitantes\n" +
                    "3. Tela de Chamados\n" +
                    "4. Sair\n" +
                    "Digite o que deseja fazer: ");

                int opcao = Leitores.LerInt();

                if (opcao < 1 || opcao > 4)
                {
                    ImprimirErro("Opcao incorreta");
                }

                switch (opcao)
                {
                    case 1:
                        return telaEquipamento;
                    case 2:
                        return telaSolicitante;
                    case 3:
                        return telaChamado;
                    case 4:
                        return null;
                }
            }
        }

        private void ImprimirErro(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
    }
}
