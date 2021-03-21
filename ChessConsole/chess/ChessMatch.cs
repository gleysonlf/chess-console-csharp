using System;
using System.Collections.Generic;
using ChessConsole.board;

namespace ChessConsole.chess
{
    class ChessMatch
    {
        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;
        public Board brd { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }
        public bool check { get; private set; }
        public Piece passedPawn { get; private set; }

        public ChessMatch()
        {
            brd = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            finished = false;
            check = false;
            passedPawn = null;
            pieces = new HashSet<Piece>();
            captured = new HashSet<Piece>();
            StartPositions();
        }

        public Piece MovePiece(Position origin, Position destiny)
        {
            Piece piece = brd.RemovePiece(origin);
            piece.IncrementMovementCount();
            Piece capturedPiece = brd.RemovePiece(destiny);
            brd.PutPiece(piece, destiny);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }

            // #Lances Clastle Kingside
            if (piece is King && destiny.column == origin.column + 2)
            {
                Position origemT = new Position(origin.line, origin.column + 3);
                Position destinoT = new Position(origin.line, origin.column + 1);
                Piece T = brd.RemovePiece(origemT);
                T.IncrementMovementCount();
                brd.PutPiece(T, destinoT);
            }

            // #Lances Clastle Queenside
            if (piece is King && destiny.column == origin.column - 2)
            {
                Position origemT = new Position(origin.line, origin.column - 4);
                Position destinoT = new Position(origin.line, origin.column - 1);
                Piece T = brd.RemovePiece(origemT);
                T.IncrementMovementCount();
                brd.PutPiece(T, destinoT);
            }

            // #Lances en passant
            if (piece is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == null)
                {
                    Position posP;
                    if (piece.cor == Color.White)
                    {
                        posP = new Position(destiny.line + 1, destiny.column);
                    }
                    else
                    {
                        posP = new Position(destiny.line - 1, destiny.column);
                    }
                    capturedPiece = brd.RemovePiece(posP);
                    captured.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMovement(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = brd.RemovePiece(destiny);
            p.DecrementMovementCount();
            if (capturedPiece != null)
            {
                brd.PutPiece(capturedPiece, destiny);
                captured.Remove(capturedPiece);
            }
            brd.PutPiece(p, origin);

            // #Lances Clastle Kingside
            if (p is King && destiny.column == origin.column + 2)
            {
                Position origemT = new Position(origin.line, origin.column + 3);
                Position destinoT = new Position(origin.line, origin.column + 1);
                Piece T = brd.RemovePiece(destinoT);
                T.DecrementMovementCount();
                brd.PutPiece(T, origemT);
            }

            // #Lances Clastle Queenside
            if (p is King && destiny.column == origin.column - 2)
            {
                Position origemT = new Position(origin.line, origin.column - 4);
                Position destinoT = new Position(origin.line, origin.column - 1);
                Piece T = brd.RemovePiece(destinoT);
                T.DecrementMovementCount();
                brd.PutPiece(T, origemT);
            }

            // #Lances en passant
            if (p is Pawn)
            {
                if (origin.column != destiny.column && capturedPiece == passedPawn)
                {
                    Piece peao = brd.RemovePiece(destiny);
                    Position posP;
                    if (p.cor == Color.White)
                    {
                        posP = new Position(3, destiny.column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.column);
                    }
                    brd.PutPiece(peao, posP);
                }
            }
        }

        public void ExecuteMovement(Position origin, Position destiny)
        {
            Piece capturedPiece = MovePiece(origin, destiny);

            if (IsInCheck(currentPlayer))
            {
                UndoMovement(origin, destiny, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            Piece p = brd.GetPiece(destiny);

            // #Lances Promotion
            if (p is Pawn)
            {
                if ((p.cor == Color.White && destiny.line == 0) || (p.cor == Color.Black && destiny.line == 7))
                {
                    p = brd.RemovePiece(destiny);
                    pieces.Remove(p);
                    Piece dama = new Queen(brd, p.cor);
                    brd.PutPiece(dama, destiny);
                    pieces.Add(dama);
                }
            }

            if (IsInCheck(GetEnemy(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (IsInCheckmate(GetEnemy(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                ChangePlayer();
            }

            // #Lances En passant
            if (p is Pawn && (destiny.line == origin.line - 2 || destiny.line == origin.line + 2))
            {
                passedPawn = p;
            }
            else
            {
                passedPawn = null;
            }

        }

        public void ValidateOriginPosition(Position pos)
        {
            if (brd.GetPiece(pos) == null)
            {
                throw new BoardException("There is no piece in the chosen origin position!");
            }
            if (currentPlayer != brd.GetPiece(pos).cor)
            {
                throw new BoardException("A piece of chosen origin is not yours!");
            }
            if (!brd.GetPiece(pos).ExistPossibleMovements())
            {
                throw new BoardException("There are no possible movements for the chosen piece of origin!");
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!brd.GetPiece(origin).PossibleMovements(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        private void ChangePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        public HashSet<Piece> CapturedPieces(Color cor)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in captured)
            {
                if (piece.cor == cor)
                {
                    aux.Add(piece);
                }
            }
            return aux;
        }

        public HashSet<Piece> AvailablePieces(Color cor)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece piece in pieces)
            {
                if (piece.cor == cor)
                {
                    aux.Add(piece);
                }
            }
            aux.ExceptWith(CapturedPieces(cor));
            return aux;
        }

        private Color GetEnemy(Color cor)
        {
            if (cor == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece GetKing(Color cor)
        {
            foreach (Piece piece in AvailablePieces(cor))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool IsInCheck(Color cor)
        {
            Piece king = GetKing(cor);
            if (king == null)
            {
                throw new BoardException("Error! There is no " + cor +" king on the board!");
            }
            foreach (Piece piece in AvailablePieces(GetEnemy(cor)))
            {
                bool[,] mat = piece.possibleMovements();
                if (mat[king.position.line, king.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsInCheckmate(Color cor)
        {
            if (!IsInCheck(cor))
            {
                return false;
            }
            foreach (Piece piece in AvailablePieces(cor))
            {
                bool[,] mat = piece.possibleMovements();
                for (int i = 0; i < brd.lines; i++)
                {
                    for (int j = 0; j < brd.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = piece.position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MovePiece(origin, destiny);
                            bool testeXeque = IsInCheck(cor);
                            UndoMovement(origin, destiny, capturedPiece);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void PutNewPiece(char column, int line, Piece piece)
        {
            brd.PutPiece(piece, new Square(column, line).toPosicao());
            pieces.Add(piece);
        }

        private void StartPositions()
        {
            PutNewPiece('a', 1, new Rook(brd, Color.White));
            PutNewPiece('b', 1, new Knight(brd, Color.White));
            PutNewPiece('c', 1, new Bishop(brd, Color.White));
            PutNewPiece('d', 1, new Queen(brd, Color.White));
            PutNewPiece('e', 1, new King(brd, Color.White, this));
            PutNewPiece('f', 1, new Bishop(brd, Color.White));
            PutNewPiece('g', 1, new Knight(brd, Color.White));
            PutNewPiece('h', 1, new Rook(brd, Color.White));
            PutNewPiece('a', 2, new Pawn(brd, Color.White, this));
            PutNewPiece('b', 2, new Pawn(brd, Color.White, this));
            PutNewPiece('c', 2, new Pawn(brd, Color.White, this));
            PutNewPiece('d', 2, new Pawn(brd, Color.White, this));
            PutNewPiece('e', 2, new Pawn(brd, Color.White, this));
            PutNewPiece('f', 2, new Pawn(brd, Color.White, this));
            PutNewPiece('g', 2, new Pawn(brd, Color.White, this));
            PutNewPiece('h', 2, new Pawn(brd, Color.White, this));

            PutNewPiece('a', 8, new Rook(brd, Color.Black));
            PutNewPiece('b', 8, new Knight(brd, Color.Black));
            PutNewPiece('c', 8, new Bishop(brd, Color.Black));
            PutNewPiece('d', 8, new Queen(brd, Color.Black));
            PutNewPiece('e', 8, new King(brd, Color.Black, this));
            PutNewPiece('f', 8, new Bishop(brd, Color.Black));
            PutNewPiece('g', 8, new Knight(brd, Color.Black));
            PutNewPiece('h', 8, new Rook(brd, Color.Black));
            PutNewPiece('a', 7, new Pawn(brd, Color.Black, this));
            PutNewPiece('b', 7, new Pawn(brd, Color.Black, this));
            PutNewPiece('c', 7, new Pawn(brd, Color.Black, this));
            PutNewPiece('d', 7, new Pawn(brd, Color.Black, this));
            PutNewPiece('e', 7, new Pawn(brd, Color.Black, this));
            PutNewPiece('f', 7, new Pawn(brd, Color.Black, this));
            PutNewPiece('g', 7, new Pawn(brd, Color.Black, this));
            PutNewPiece('h', 7, new Pawn(brd, Color.Black, this));
        }
    }
}
