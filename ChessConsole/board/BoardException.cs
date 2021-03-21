using System;

namespace ChessConsole.board
{
    class BoardException : Exception
    {

        public BoardException(string msg) : base(msg)
        {
        }
    }
}
