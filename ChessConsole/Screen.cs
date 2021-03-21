using System;
using System.Collections.Generic;
using ChessConsole.board;
using ChessConsole.chess;

namespace ChessConsole
{
    class Screen
    {
        public static void ShowGame(ChessMatch game)
        {
            ShowBoard(game.brd);
            Console.WriteLine();
            ShowCapturedPieces(game);
            Console.WriteLine();
            Console.WriteLine("Turn: " + game.turn);
            if (!game.finished)
            {
                Console.WriteLine("Waiting for the player : " + game.currentPlayer);
                if (game.check)
                {
                    Console.WriteLine("Check!");
                }
            }
            else
            {
                Console.WriteLine("Checkmate!");
                Console.WriteLine("Winner: " + game.currentPlayer);
            }
        }

        public static void ShowCapturedPieces(ChessMatch game)
        {
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            ShowPieces(game.CapturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Black: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ShowPieces(game.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void ShowPieces(HashSet<Piece> conjunto)
        {
            Console.Write("[ ");
            foreach (Piece piece in conjunto)
            {
                Console.Write(piece + " ");
            }
            Console.Write("]");
        }

        public static void ShowBoard(Board brd)
        {

            for (int i = 0; i < brd.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.columns; j++)
                {
                    ShowPiece(brd.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ShowBoard(Board brd, bool[,] posicoePossiveis)
        {

            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < brd.lines; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < brd.columns; j++)
                {
                    if (posicoePossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ShowPiece(brd.GetPiece(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static Square ReadSquare()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new Square(column, line);
        }

        public static void ShowPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.cor == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }


    }
}
