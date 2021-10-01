using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core.Cell
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

    }

    public class StandardCellStatus : BaseCellStatus
    {
        private StandardCellStatus(StandardCellStatusType type)
        {
            Type = type;

        }

        public StandardCellStatusType Type { get; set; }

        public override int Id => (int)Type;


        public override string ToString()
        {
            return Type.ToString();
        }

        public static StandardCellStatus Empty()
        {
            return new StandardCellStatus(StandardCellStatusType.EMPTY);
        }

        public static StandardCellStatus Player1()
        {
            return new StandardCellStatus(StandardCellStatusType.PLAYER1);
        }

        public static StandardCellStatus Player2()
        {
            return new StandardCellStatus(StandardCellStatusType.PLAYER2);
        }
    }


    public enum StandardCellStatusType
    {
        EMPTY = 0,
        PLAYER1 = 1,
        PLAYER2 = 2
    }


}
