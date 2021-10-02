using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core
{
    /// <summary>Interface for cell status.</summary>
    public interface ICellStatus
    {
        /// <summary>An id that can uniquely identify the cell status.</summary>
        int Id { get; }
    }


    public abstract class BaseCellStatus : ICellStatus
    {
        public abstract int Id { get; }

        protected abstract string Name { get; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return Id == ((StandardCellStatus)obj).Id;
            }
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public override string ToString()
        {
            return Name;
        }

    }

    public class StandardCellStatus : BaseCellStatus
    {
        public static readonly StandardCellStatus EMPTY = new StandardCellStatus(0, "EMPTY");
        public static readonly StandardCellStatus PLAYER1 = new StandardCellStatus(1, "PLAYER1");
        public static readonly StandardCellStatus PLAYER2 = new StandardCellStatus(2, "PLAYER2");
        public static readonly List<StandardCellStatus> VALUES = new List<StandardCellStatus> { EMPTY, PLAYER1, PLAYER2 };

        private StandardCellStatus(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public override int Id { get; }
        protected override string Name { get; }


        public static StandardCellStatus of(int id)
        {
            return VALUES.Find(status => status.Id == id) ?? throw new ArgumentOutOfRangeException("id", $"invalid id: {id}");
        }
    }
}
