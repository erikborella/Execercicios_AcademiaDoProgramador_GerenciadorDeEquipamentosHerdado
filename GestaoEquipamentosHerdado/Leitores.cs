using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoEquipamentosHerdado
{
    class Leitores
    {

        public static int LerInt()
        {
            while (true)
            {
                try
                {
                    int num = Convert.ToInt32(Console.ReadLine());

                    return num;
                } catch (Exception)
                {
                    ImprimirErro("Digite um numero!");
                }
            }
        }

        public static double LerDouble()
        {
            while (true)
            {
                try
                {
                    double n = Convert.ToDouble(Console.ReadLine());

                    return n;
                }
                catch (Exception)
                {
                    ImprimirErro("Digite um numero!");
                    continue;
                }
            }
        }

        public static DateTime LerData()
        {
            while (true)
            {
                try
                {
                    string dataStr = Console.ReadLine();
                    DateTime data = DateTime.Parse(dataStr);

                    return data;
                }
                catch (Exception)
                {
                    ImprimirErro("Digite uma data no formato dd/mm/aaaa");
                    continue;
                }
            }

        }

        private static void ImprimirErro(string mensagem)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(mensagem);
            Console.ResetColor();
        }
    }
}
