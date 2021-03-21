using ChessConsole.board;

namespace ChessConsole.chess
{
    class King : Piece
    {

        private ChessMatch game;

        public King(Board brd, Color cor, ChessMatch game) : base(brd, cor)
        {
            this.game = game;
        }

        public override string ToString()
        {
            return "K";
        }

        private bool canMove(Position pos)
        {
            Piece p = brd.GetPiece(pos);
            return p == null || p.cor != cor;
        }

        private bool testeTorreParaRoque(Position pos)
        {
            Piece p = brd.GetPiece(pos);
            return p != null && p is Rook && p.cor == cor && p.movementCount == 0;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[brd.lines, brd.columns];

            Position pos = new Position(0, 0);

            // acima
            pos.SetValues(position.line - 1, position.column);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // ne
            pos.SetValues(position.line - 1, position.column + 1);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // direita
            pos.SetValues(position.line, position.column + 1);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // se
            pos.SetValues(position.line + 1, position.column + 1);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // abaixo
            pos.SetValues(position.line + 1, position.column);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // so
            pos.SetValues(position.line + 1, position.column - 1);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // esquerda
            pos.SetValues(position.line, position.column - 1);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }
            // no
            pos.SetValues(position.line - 1, position.column - 1);
            if (brd.IsValidPosition(pos) && canMove(pos))
            {
                mat[pos.line, pos.column] = true;
            }

            // #Lances Clastle
            if (movementCount == 0 && !game.check)
            {
                // #Lances Clastle Kingside
                Position posT1 = new Position(position.line, position.column + 3);
                if (testeTorreParaRoque(posT1))
                {
                    Position p1 = new Position(position.line, position.column + 1);
                    Position p2 = new Position(position.line, position.column + 2);
                    if (brd.GetPiece(p1) == null && brd.GetPiece(p2) == null)
                    {
                        mat[position.line, position.column + 2] = true;
                    }
                }
                // #Lances Clastle Queenside
                Position posT2 = new Position(position.line, position.column - 4);
                if (testeTorreParaRoque(posT2))
                {
                    Position p1 = new Position(position.line, position.column - 1);
                    Position p2 = new Position(position.line, position.column - 2);
                    Position p3 = new Position(position.line, position.column - 3);
                    if (brd.GetPiece(p1) == null && brd.GetPiece(p2) == null && brd.GetPiece(p3) == null)
                    {
                        mat[position.line, position.column - 2] = true;
                    }
                }
            }


            return mat;
        }
    }
}
