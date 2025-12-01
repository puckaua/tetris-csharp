using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris_csharp;

namespace tetris_csharp
{
    internal class Tetromino
    {
        public int[,] peca { get; private set; }
        public char tipo { get; private set; }

        public Tetromino(char tipo)
        {
            this.tipo = tipo;
            peca = ObterFormaInicial(tipo);
        }

        private int[,] ObterFormaInicial(char tipo)
        {
            switch (tipo)
            {
                case 'I':
                    return new int[,] { { 1, 1, 1 }, { 0, 0, 0 }, { 0, 0, 0 } };
                case 'L':
                    return new int[,] { { 1, 0, 0 }, { 1, 0, 0 }, { 1, 1, 0 } };
                case 'T':
                    return new int[,] { { 1, 1, 1 }, { 0, 1, 0 }, { 0, 1, 0 } };
                default:
                    return new int[3, 3];
            }
        }
        public void Rotacionar90H()
        {
            int[,] aux = new int[3, 3];
            int k;
            for (int i = 0; i < aux.GetLength(0); i++)
            {
                k = aux.GetLength(0) - 1;
                for (int j = 0; j < aux.GetLength(1); j++)
                {
                    aux[i, j] = peca[k, i];
                    k--;
                }
            }

            for (int i = 0; i < peca.GetLength(0); i++)
            {
                for (int j = 0; j < peca.GetLength(1); j++)
                {
                    peca[i, j] = aux[i, j];
                }
            }
        }

        public void Rotacionar90AH()
        {
            int[,] aux = new int[3, 3];
            int k = aux.GetLength(1) - 1;
            for (int i = 0; i < aux.GetLength(0); i++)
            {

                for (int j = 0; j < aux.GetLength(1); j++)
                {
                    aux[i, j] = peca[j, k];
                }
                k--;
            }

            for (int i = 0; i < peca.GetLength(0); i++)
            {
                for (int j = 0; j < peca.GetLength(1); j++)
                {
                    peca[i, j] = aux[i, j];
                }
            }
        }


    }
}
