using ChessConsole.board;

namespace ChessConsole.chess
{
    class Knight : Piece
    {
        public Knight(Board brd, Color cor) : base(brd, cor)
        {
        }

        public override string ToString()
        {
            return "N";
        }

        private bool CanMove(Position pos)
        {
            Piece p = brd.GetPiece(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[brd.lines, brd.columns];

            Position pos = new Position(0, 0);

            pos.SetValues(position.line - 1, position.column - 2);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line - 2, position.column - 1);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line - 2, position.column + 1);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line - 1, position.column + 2);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 1, position.column + 2);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 2, position.column + 1);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 2, position.column - 1);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            pos.SetValues(position.line + 1, position.column - 2);
            if (brd.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            return mat;
        }
    }
}
