using System;
using ChessConsole.board;
using ChessConsole.chess;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                ChessMatch game = new ChessMatch();

                while (!game.finished)
                {

                    try
                    {
                        Console.Clear();
                        Screen.ShowGame(game);

                        Console.WriteLine();
                        Console.Write("Origin: ");
                        Position origin = Screen.ReadSquare().toPosicao();
                        game.ValidateOriginPosition(origin);

                        bool[,] posicoesPossiveis = game.brd.GetPiece(origin).possibleMovements();

                        Console.Clear();
                        Screen.ShowBoard(game.brd, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destiny: ");
                        Position destiny = Screen.ReadSquare().toPosicao();
                        game.ValidateDestinyPosition(origin, destiny);

                        game.ExecuteMovement(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Screen.ShowGame(game);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
