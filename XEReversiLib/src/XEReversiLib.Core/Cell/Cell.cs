using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core
{
    public class Cell
    {
        private Cell(Position position, ICellStatus status)
        {
            Position = position;
            Status = status;
        }

        public Position Position { get; }
        public ICellStatus Status { get; set; }

        public static Cell Of(ICellStatus status, Position position)
        {
            return new Cell(position, status);

        }

        public static Cell Of(ICellStatus status, Space space, params int[] values)
        {
            return new Cell(new Position(space, values), status);

        }

        public static Cell Of(ICellStatus status, Space space, params (Axis axis, int value)[] points)
        {
            return new Cell(new Position(space, points), status);

        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Cell otherCell = (Cell)obj;
                return Position.Equals(otherCell.Position) && Status.Equals(otherCell.Status);
            }
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode() + Status.GetHashCode();
        }

        public override string ToString()
        {
            return $"Cell({Position}, {Status})";
        }

    }
}
