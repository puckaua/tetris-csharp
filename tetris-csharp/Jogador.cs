using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris_csharp;

namespace tetris_csharp
{
    internal class Jogador
    {
        public string nome;
        public int pontuacao;

        public string Nome
        {
            get;
            private set;
        }

        public int Pontuacao
        {
            get;
            set;
        }


        public Jogador(string nome)
        {
            Nome = nome;
            Pontuacao = 0;
        }



        public void SalvarPontuacao(string caminhoArquivo)
        {
            StreamWriter arq = new StreamWriter(caminhoArquivo, true);
            arq.WriteLine($"{Nome};{Pontuacao}");
            arq.Close();

        }

    }
}
