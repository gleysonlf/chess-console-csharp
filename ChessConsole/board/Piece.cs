using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.board
{
    abstract class Piece
    {
        public Position position { get; set; }
        public Color cor { get; protected set; }
        public int movementCount { get; protected set; }
        public Board brd { get; protected set; }

        public Piece(Board brd, Color cor)
        {
            this.position = null;
            this.brd = brd;
            this.cor = cor;
            this.movementCount = 0;
        }

        public void IncrementMovementCount()
        {
            movementCount++;
        }

        public void DecrementMovementCount()
        {
            movementCount--;
        }

        public bool ExistPossibleMovements()
        {
            bool[,] mat = possibleMovements();
            for (int i = 0; i < brd.lines; i++)
            {
                for (int j = 0; j < brd.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool PossibleMovements(Position pos)
        {
            return possibleMovements()[pos.line, pos.column];
        }

        public abstract bool[,] possibleMovements();
    }
}
