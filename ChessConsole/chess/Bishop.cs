using ChessConsole.board;

namespace ChessConsole.chess
{
    class Bishop : Piece
    {

        public Bishop(Board brd, Color cor) : base(brd, cor)
        {
        }

        public override string ToString()
        {
            return "B";
        }

        private bool canMove(Position pos)
        {
            Piece p = brd.GetPiece(pos);
            return p == null || p.cor != cor;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[brd.lines, brd.columns];

            Position pos = new Position(0, 0);

            // NO
            pos.SetValues(position.line - 1, position.column - 1);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.SetValues(pos.line - 1, pos.column - 1);
            }

            // NE
            pos.SetValues(position.line - 1, position.column + 1);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.SetValues(pos.line - 1, pos.column + 1);
            }

            // SE
            pos.SetValues(position.line + 1, position.column + 1);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.SetValues(pos.line + 1, pos.column + 1);
            }

            // SO
            pos.SetValues(position.line + 1, position.column - 1);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.SetValues(pos.line + 1, pos.column - 1);
            }

            return mat;
        }
    }
}
