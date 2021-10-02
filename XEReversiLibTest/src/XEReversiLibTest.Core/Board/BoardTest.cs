using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using XEReversiLib.Core;

namespace XEReversiLibTest.Core
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestGetCellRow()
        {
            Board board = Board.CreateStandard();
            board.UpdateCell(StandardCellStatus.PLAYER1, 2, 1);
            board.UpdateCell(StandardCellStatus.PLAYER2, 4, 1);

            List<Cell> result = board.GetCellRow(Space.D2.GetPosition(1, 1), new Line(Space.D2.GetPosition(1, 0)), cell => IsEnd(cell)).ToList();
            List<Cell> excepect = new List<Cell> {
                Cell.Of(StandardCellStatus.PLAYER1, Space.D2.GetPosition(2,1)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(3,1)),
                Cell.Of(StandardCellStatus.PLAYER2, Space.D2.GetPosition(4,1)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(5,1)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(6,1)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(7,1)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(8,1)),
            };

            CollectionAssert.AreEquivalent(excepect, result);
        }

        [TestMethod]
        public void TestGetAllUnitCellRows()
        {
            Board board = Board.CreateStandard();

            List<Cell> result = board.GetAllUnitCellRows(Space.D2.GetPosition(4, 4), cell => IsEnd(cell)).ToList();
            List<Cell> excepect = new List<Cell> {
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(1,4)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(2,4)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(3,4)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(5,4)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(6,4)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(7,4)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(8,4)),


                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(4,1)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(4,2)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(4,3)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(4,5)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(4,6)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(4,7)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(4,8)),


                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(1,1)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(2,2)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(3,3)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(5,5)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(6,6)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(7,7)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(8,8)),

                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(1,7)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(2,6)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(3,5)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(5,3)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(6,2)),
                Cell.Of(StandardCellStatus.EMPTY, Space.D2.GetPosition(7,1)),
            };

            CollectionAssert.AreEquivalent(excepect, result);
        }

        private bool IsEnd(Cell cell)
        {
            return cell.Position.GetValues().Any(value => value < 1 || value > 8);
        }
    }
}
