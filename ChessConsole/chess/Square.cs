using System;
using ChessConsole.board;

namespace ChessConsole.chess
{
    class Square
    {
        public char column { get; set; }
        public int line { get; set; }

        public Square(char column, int line)
        {
            this.column = Char.ToLower(column);
            if (line < 0 || line > 8)
            {
                throw new BoardException("Invalid line to position!");
            }
            this.line = line;
        }

        public Position toPosicao()
        {
            int col = column - 'a';
            if (col < 0 || col > 7)
            {
                throw new BoardException("Invalid column to position!");
            }
            return new Position(8 - line, col);
        }

        public override string ToString()
        {
            return "" + column + line;
        }
    }
}
