﻿using GenericBoard.Entities.Enums;

namespace GenericBoard.Entities
{
    public class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int QuantityOfMoves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece()
        {
        }

        public Piece(Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
            QuantityOfMoves = 0;
        }

        public void IncreaseQuantityOfMoves()
        {
            QuantityOfMoves++;
        }
    }
}
