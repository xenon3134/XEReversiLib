using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core
{
    public class Board
    {
        public static Board CreateStandard()
        {
            return new Board(Space.D2, StandardCellStatus.EMPTY);
        }

        public Board(Space space, ICellStatus defaultStatus = null)
        {
            Space = space;
            DefaultStatus = defaultStatus;
        }

        public Space Space { get; }

        public ICellStatus DefaultStatus { get; }
        public Dictionary<Position, Cell> CellMap { get; } = new Dictionary<Position, Cell>();

        public Cell GetCell(ICellStatus defaultStatus = null, params int[] postionValues)
        {
            return GetCell(new Position(Space, postionValues), defaultStatus);
        }

        public Cell GetCell(ICellStatus defaultStatus = null, params (Axis axis, int value)[] points)
        {
            return GetCell(new Position(Space, points), defaultStatus);
        }

        public Cell GetCell(Position position, ICellStatus defaultStatus = null)
        {
            vaildateSpace(position.Space);
            Cell cell;
            return CellMap.TryGetValue(position, out cell) ? cell :
                defaultStatus != null ? Cell.Of(defaultStatus, position) :
                DefaultStatus != null ? Cell.Of(DefaultStatus, position) :
                throw new Exception("No status is specified.");
        }

        public void UpdateCell(ICellStatus status, params int[] postionValues)
        {
            UpdateCell(status, new Position(Space, postionValues));
        }

        public void UpdateCell(ICellStatus status, params (Axis axis, int value)[] points)
        {
            UpdateCell(status, new Position(Space, points));
        }

        public void UpdateCell(ICellStatus status, Position position)
        {
            Cell cell = GetCell(position, status);
            cell.Status = status;
            CellMap[position] = cell;
        }

        public void RemoveCell(params int[] postionValues)
        {
            RemoveCell(new Position(Space, postionValues));
        }

        public void RemoveCell(params (Axis axis, int value)[] points)
        {
            RemoveCell(new Position(Space, points));
        }

        public void RemoveCell(Position position)
        {
            vaildateSpace(position.Space);
            CellMap.Remove(position);
        }

        public void Clear()
        {
            CellMap.Clear();

        }

        public Dictionary<ICellStatus, int> Count()
        {
            return CellMap.Values.GroupBy(cell => cell.Status).ToDictionary(group => group.Key, group => group.Count());
        }

        public CellRow GetCellRow(Position start, Line line, Func<Cell, bool> endCondition = null, ICellStatus defaultStatus = null)
        {
            vaildateSpace(start.Space);
            vaildateSpace(line.Unit.Space);
            return new CellRow(start, line, this, endCondition, defaultStatus);
        }

        public CellRow GetCellRow(Position start, Position next, Func<Cell, bool> endCondition = null, ICellStatus defaultStatus = null)
        {
            vaildateSpace(start.Space);
            vaildateSpace(next.Space);
            return new CellRow(start, new Line(start, next), this, endCondition, defaultStatus);
        }

        public CellRows GetAllUnitCellRows(Position start, Func<Cell, bool> endCondition = null, ICellStatus defaultStatus = null)
        {
            vaildateSpace(start.Space);
            return new CellRows(Space.GetAllUnitLines().Select(line => new CellRow(start, line, this, endCondition, defaultStatus)));
        }

        private void vaildateSpace(Space space)
        {
            if (!Space.Equals(space))
            {
                throw new ArgumentException("space", $"It must be the same space as the board's space. board:{Space} argumet:{space}");
            }
        }

        public override string ToString()
        {
            return $"Board(Space:{Space}, DefaultStatus:{DefaultStatus})";
        }
    }

    public class CellRow : IEnumerable<Cell>
    {
        public CellRow(Position start, Line line, Board board, Func<Cell, bool> endCondition = null, ICellStatus defaultStatus = null)
        {
            Start = start;
            Line = line;
            Board = board;
            EndCondition = endCondition ?? (cell => false);
            DefaultStatus = defaultStatus;
        }

        public Position Start { get; }
        public Line Line { get; }

        public Board Board { get; }

        public Func<Cell, bool> EndCondition { get; }

        public ICellStatus DefaultStatus { get; }


        public IEnumerator<Cell> GetEnumerator()
        {
            return new CellRowEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    public class CellRowEnumerator : IEnumerator<Cell>
    {
        public CellRowEnumerator(CellRow cellRow)
        {

            CellRow = cellRow;
            _currentPosition = CellRow.Start;
        }

        private Position _currentPosition;

        public CellRow CellRow { get; }
        public Cell Current => CellRow.Board.GetCell(_currentPosition, CellRow.DefaultStatus);

        object IEnumerator.Current => Current;

        public void Dispose() { }

        public bool MoveNext()
        {
            _currentPosition += CellRow.Line.Unit;
            return !CellRow.EndCondition(Current);
        }

        public void Reset()
        {
            _currentPosition = CellRow.Start;
        }
    }

    public class CellRows : IEnumerable<Cell>
    {

        public CellRows(IEnumerable<CellRow> rows)
        {
            RowIEnumerator = rows.GetEnumerator();
        }

        public IEnumerator<CellRow> RowIEnumerator { get; }


        public IEnumerator<Cell> GetEnumerator()
        {
            return new CellRowsEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CellRowsEnumerator : IEnumerator<Cell>
    {

        public CellRowsEnumerator(CellRows cellRows)
        {

            CellRows = cellRows;
            if (CellRows.RowIEnumerator.MoveNext())
            {
                _currentCellEnumerator = CellRows.RowIEnumerator.Current.GetEnumerator();
            }
        }

        private IEnumerator<Cell> _currentCellEnumerator;

        public CellRows CellRows { get; }
        public Cell Current => _currentCellEnumerator.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            _currentCellEnumerator.Dispose();
        }

        public bool MoveNext()
        {
            if (_currentCellEnumerator is null)
            {
                return false;
            }
            bool result = _currentCellEnumerator.MoveNext();
            if (!result)
            {
                _currentCellEnumerator.Dispose();
                if (CellRows.RowIEnumerator.MoveNext())
                {
                    _currentCellEnumerator = CellRows.RowIEnumerator.Current.GetEnumerator();
                    result = MoveNext();
                }
                else
                {
                    result = false;
                }
            }
            return result;

        }

        public void Reset()
        {
            _currentCellEnumerator.Dispose();
            CellRows.RowIEnumerator.Reset();
            _currentCellEnumerator = CellRows.RowIEnumerator.Current.GetEnumerator();
        }
    }


}
