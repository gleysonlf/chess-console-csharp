using System;
using System.Collections.Generic;
using System.Text;

namespace ChessConsole.board
{
    class Board
    {

        public int lines { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Board(int lines, int columns)
        {
            this.lines = lines;
            this.columns = columns;
            pieces = new Piece[lines, columns];
        }

        public Piece GetPiece(int line, int column)
        {
            return pieces[line, column];
        }

        public Piece GetPiece(Position pos)
        {
            return pieces[pos.line, pos.column];
        }

        public bool ExistPiece(Position pos)
        {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        public void PutPiece(Piece piece, Position pos)
        {
            if (ExistPiece(pos))
            {
                throw new BoardException("There is already a piece in that position!");
            }
            pieces[pos.line, pos.column] = piece;
            piece.position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if (GetPiece(pos) == null)
            {
                return null;
            }
            Piece aux = GetPiece(pos);
            aux.position = null;
            pieces[pos.line, pos.column] = null;
            return aux;
        }

        public bool IsValidPosition(Position pos)
        {
            if (pos.line < 0 || pos.line >= lines || pos.column < 0 || pos.column >= columns)
            {
                return false;
            }
            return true;
        }

        public void ValidatePosition(Position pos)
        {
            if (!IsValidPosition(pos))
            {
                throw new BoardException("Invalid position!");
            }
        }
    }
}
