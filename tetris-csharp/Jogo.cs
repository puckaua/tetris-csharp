using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tetris_csharp;

namespace tetris_csharp
{
    internal class Jogo
    {
        private int[,] tabuleiro = new int[20, 10];
        private Jogador jogador;
        private Tetromino tetrominoAtual;
        private int posX, posY;
        private Random random = new Random();
        private bool quebrarLoop = false;


        public Jogo(Jogador jogador)
        {
            this.jogador = jogador;
            NovoTetromino(out quebrarLoop);
        }

        public bool QuebrarLoop
        {
            get { return quebrarLoop; }
            set { quebrarLoop = value; }
        }

        public void NovoTetromino(out bool quebrarLoop)
        {
            quebrarLoop = false;
            char[] tipos = { 'I', 'L', 'T' };
            tetrominoAtual = new Tetromino(tipos[random.Next(3)]);
            posX = 0;
            posY = 3;
            if (Colisao(posX, posY, tetrominoAtual.peca))
            {
                Console.WriteLine("GAME OVER, BRO!");
                jogador.SalvarPontuacao("scores.txt");
                quebrarLoop = true;

            }
        }

        private bool Colisao(int x, int y, int[,] peca)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (peca[i, j] == 1)
                    {
                        int PosicaoRealTabuleiroX = x + i;
                        int PosicaoRealTabuleiroY = y + j;
                        if (PosicaoRealTabuleiroX < 0 || PosicaoRealTabuleiroX >= 20 || PosicaoRealTabuleiroY < 0 || PosicaoRealTabuleiroY >= 10 || tabuleiro[PosicaoRealTabuleiroX, PosicaoRealTabuleiroY] == 1)
                            return true;
                    }
            return false;
        }

        private void FixarTetromino()
        {
            int pontosPeca;

            if (tetrominoAtual.tipo == 'I')
            {
                pontosPeca = 3;
            }
            else if (tetrominoAtual.tipo == 'L')
            {
                pontosPeca = 4;
            }
            else
            {
                pontosPeca = 5;
            }
            jogador.Pontuacao += pontosPeca;

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tetrominoAtual.peca[i, j] == 1)
                        tabuleiro[posX + i, posY + j] = 1;

            VerificarLinhasCompletas();
            NovoTetromino(out quebrarLoop);
        }

        private void VerificarLinhasCompletas()
        {
            int linhasCompletas = 0;
            for (int i = 0; i < 20; i++)
            {
                bool completa = true;
                for (int j = 0; j < 10; j++)
                    if (tabuleiro[i, j] == 0)
                        completa = false;

                if (completa)
                {
                    linhasCompletas++;
                    for (int k = i; k > 0; k--)
                        for (int j = 0; j < 10; j++)
                            tabuleiro[k, j] = tabuleiro[k - 1, j];
                    for (int j = 0; j < 10; j++)
                        tabuleiro[0, j] = 0;
                }
            }
            jogador.Pontuacao += linhasCompletas * 300;
            if (linhasCompletas > 1)
                jogador.Pontuacao += 100;
        }

        public void MostrarTabuleiro()
        {
            Console.Clear();
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    bool parteDoTetromino = false;
                    for (int x = 0; x < 3; x++)
                        for (int y = 0; y < 3; y++)
                            if (tetrominoAtual.peca[x, y] == 1 &&
                                i == posX + x && j == posY + y)
                                parteDoTetromino = true;

                    Console.Write(parteDoTetromino || tabuleiro[i, j] == 1 ? "[]" : " .");
                }
                Console.WriteLine();
            }
            Console.WriteLine($"\nPontuação: {jogador.Pontuacao}");
        }

        public void ProcessarComando(ConsoleKey tecla)
        {
            switch (tecla)
            {
                case ConsoleKey.LeftArrow:
                    if (!Colisao(posX, posY - 1, tetrominoAtual.peca))
                        posY--;
                    break;
                case ConsoleKey.RightArrow:
                    if (!Colisao(posX, posY + 1, tetrominoAtual.peca))
                        posY++;
                    break;
                case ConsoleKey.DownArrow:
                    if (!Colisao(posX + 1, posY, tetrominoAtual.peca))
                        posX++;
                    else FixarTetromino();
                    break;
                case ConsoleKey.Z:
                    tetrominoAtual.Rotacionar90AH();
                    if (Colisao(posX, posY, tetrominoAtual.peca))
                        tetrominoAtual.Rotacionar90H();
                    break;
                case ConsoleKey.X:
                    tetrominoAtual.Rotacionar90H();
                    if (Colisao(posX, posY, tetrominoAtual.peca))
                        tetrominoAtual.Rotacionar90AH();
                    break;
            }
        }

    }
}
