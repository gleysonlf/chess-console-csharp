using ChessConsole.board;

namespace ChessConsole.chess
{
    class Rook : Piece
    {

        public Rook(Board brd, Color cor) : base(brd, cor)
        {
        }

        public override string ToString()
        {
            return "T";
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

            // acima
            pos.SetValues(position.line - 1, position.column);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.line = pos.line - 1;
            }

            // abaixo
            pos.SetValues(position.line + 1, position.column);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.line = pos.line + 1;
            }

            // direita
            pos.SetValues(position.line, position.column + 1);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.column = pos.column + 1;
            }

            // esquerda
            pos.SetValues(position.line, position.column - 1);
            while (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
                if (brd.GetPiece(pos) != null && brd.GetPiece(pos).cor != cor)
                {
                    break;
                }
                pos.column = pos.column - 1;
            }

            return mat;
        }

    }
}
