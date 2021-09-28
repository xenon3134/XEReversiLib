using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XEReversiLib.Core.Cell;

namespace XEReversiLib.Core.Board
{
    /// <summary>An interface for implementing a Reversi game board.</summary>
    public interface IBoard
    {
        /// <returns>Map of the id of ICellStatus and the number of cells in that status.<see cref="ICellStatus"/></returns>
        IDictionary<int, int> Count();
    }

    public abstract class GridBoard<C, S> where C : TwoDCell<S> where S : ICellStatus
    {

        public GridBoard()
        {
            CellMap = CreateCellMap();
        }

        public IDictionary<(int, int), C> CellMap { get; }


        public void UpdateCells(int x, int y, S status)
        {


        }

        protected abstract C GetEmptyCell();

        protected abstract IDictionary<(int, int), C> CreateCellMap();


    }


}
