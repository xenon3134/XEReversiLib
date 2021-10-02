using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEReversiLib.Core
{
    public interface IInitRule
    {
        void Initialize(Board board);
    }

    public class NormalInitRule : IInitRule
    {

        public static readonly NormalInitRule Standard = new NormalInitRule(Space.D2.GetPosition(4, 4), (StandardCellStatus.PLAYER1, StandardCellStatus.PLAYER2));

        public NormalInitRule(Position basePosition, (ICellStatus, ICellStatus) statusPair)
        {
            BasePosition = basePosition;
            StatusPair = statusPair;
        }

        public Position BasePosition { get; }
        public (ICellStatus, ICellStatus) StatusPair { get; }
        public void Initialize(Board board)
        {
            foreach (int i in Enumerable.Range(1, board.Space.Dimension))
            {
                board.UpdateCell(StatusPair.Item1, BasePosition);

                Position unit = new Position(board.Space, Enumerable.Range(0, i).Select(j => 1).ToArray());
                foreach (Position pos in unit.GetPermutationPositions())
                {
                    Position targetPosition = BasePosition + pos;
                    ICellStatus status = i % 2 == 0 ? StatusPair.Item1 : StatusPair.Item2;
                    board.UpdateCell(status, targetPosition);
                }
            }
        }
    }
}
