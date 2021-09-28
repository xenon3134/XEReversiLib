using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core.Cell
{
    public interface ICell<S> where S : ICellStatus
    {
        S Status { get; set; }
    }

    public class TwoDCell<S> : ICell<S> where S : ICellStatus
    {
        public TwoDCell(int x, int y)
        {
            Postion = (x, y);
        }

        public S Status { get; set; }
        public (int, int) Postion { get; }
    }

    public class StandardCell : TwoDCell<StandardCellStatus>
    {
        public StandardCell(int x, int y) : base(x, y) { }

    }
}
