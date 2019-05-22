using System;

namespace GenericBoard.Exceptions
{
    public class BoardException : Exception
    {
        public BoardException(string msg) : base(msg)
        {
        }
    }
}