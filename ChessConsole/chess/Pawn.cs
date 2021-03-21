using ChessConsole.board;

namespace ChessConsole.chess
{
    class Pawn : Piece
    {

        private ChessMatch game;

        public Pawn(Board brd, Color cor, ChessMatch game) : base(brd, cor)
        {
            this.game = game;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Position pos)
        {
            Piece p = brd.GetPiece(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Position pos)
        {
            return brd.GetPiece(pos) == null;
        }

        public override bool[,] possibleMovements()
        {
            bool[,] mat = new bool[brd.lines, brd.columns];

            Position pos = new Position(0, 0);

            if (cor == Color.White)
            {
                pos.SetValues(position.line - 1, position.column);
                if (brd.IsValidPosition(pos) && livre(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 2, position.column);
                Position p2 = new Position(position.line - 1, position.column);
                if (brd.IsValidPosition(p2) && livre(p2) && brd.IsValidPosition(pos) && livre(pos) && movementCount == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 1, position.column - 1);
                if (brd.IsValidPosition(pos) && existeInimigo(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line - 1, position.column + 1);
                if (brd.IsValidPosition(pos) && existeInimigo(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // #Lances en passant
                if (position.line == 3)
                {
                    Position esquerda = new Position(position.line, position.column - 1);
                    if (brd.IsValidPosition(esquerda) && existeInimigo(esquerda) && brd.GetPiece(esquerda) == game.passedPawn)
                    {
                        mat[esquerda.line - 1, esquerda.column] = true;
                    }
                    Position direita = new Position(position.line, position.column + 1);
                    if (brd.IsValidPosition(direita) && existeInimigo(direita) && brd.GetPiece(direita) == game.passedPawn)
                    {
                        mat[direita.line - 1, direita.column] = true;
                    }
                }
            }
            else
            {
                pos.SetValues(position.line + 1, position.column);
                if (brd.IsValidPosition(pos) && livre(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 2, position.column);
                Position p2 = new Position(position.line + 1, position.column);
                if (brd.IsValidPosition(p2) && livre(p2) && brd.IsValidPosition(pos) && livre(pos) && movementCount == 0)
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 1, position.column - 1);
                if (brd.IsValidPosition(pos) && existeInimigo(pos))
                {
                    mat[pos.line, pos.column] = true;
                }
                pos.SetValues(position.line + 1, position.column + 1);
                if (brd.IsValidPosition(pos) && existeInimigo(pos))
                {
                    mat[pos.line, pos.column] = true;
                }

                // #Lances en passant
                if (position.line == 4)
                {
                    Position esquerda = new Position(position.line, position.column - 1);
                    if (brd.IsValidPosition(esquerda) && existeInimigo(esquerda) && brd.GetPiece(esquerda) == game.passedPawn)
                    {
                        mat[esquerda.line + 1, esquerda.column] = true;
                    }
                    Position direita = new Position(position.line, position.column + 1);
                    if (brd.IsValidPosition(direita) && existeInimigo(direita) && brd.GetPiece(direita) == game.passedPawn)
                    {
                        mat[direita.line + 1, direita.column] = true;
                    }
                }
            }

            return mat;
        }

    }
}
