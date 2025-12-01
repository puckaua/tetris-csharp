using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris_csharp;

namespace tetris_csharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Digite seu nome: ");
            string nome = Console.ReadLine();
            Jogador jogador = new Jogador(nome);
            Jogo jogo = new Jogo(jogador);

            while (true)
            {
                jogo.MostrarTabuleiro();
                ConsoleKey tecla = Console.ReadKey(true).Key;
                jogo.ProcessarComando(tecla);
                if (jogo.QuebrarLoop)
                    break;
            }
        }
    }
}


