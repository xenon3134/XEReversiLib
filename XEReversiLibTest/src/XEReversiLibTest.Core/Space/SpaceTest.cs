using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using XEReversiLib.Core;

namespace XEReversiLibTest.Core
{
    [TestClass]
    public class SpaceTest
    {
        [TestMethod]
        public void TestGetAllDirectionLines()
        {
            List<Line> result = new List<Line>(Space.D2.GetAllDirectionLines(1));
            List<Line> excepect = new List<Line> { Space.D2.GetLine(1, 0), Space.D2.GetLine(-1, 0), Space.D2.GetLine(0, 1), Space.D2.GetLine(0, -1) };

            CollectionAssert.AreEquivalent(excepect, result);

            result = new List<Line>(Space.D2.GetAllDirectionLines(1, 2));
            excepect = new List<Line> { Space.D2.GetLine(1, 2), Space.D2.GetLine(-1, 2), Space.D2.GetLine(1, -2), Space.D2.GetLine(-1, -2),
                Space.D2.GetLine(2, 1), Space.D2.GetLine(2, -1), Space.D2.GetLine(-2, 1), Space.D2.GetLine(-2, -1) };

            CollectionAssert.AreEquivalent(excepect, result);

            result = new List<Line>(Space.D3.GetAllDirectionLines(1));
            excepect = new List<Line> { Space.D3.GetLine(1, 0, 0), Space.D3.GetLine(-1, 0, 0), Space.D3.GetLine(0, 1, 0), Space.D3.GetLine(0, -1, 0),
                Space.D3.GetLine(0, 0, 1), Space.D3.GetLine(0, 0, -1)};

            CollectionAssert.AreEquivalent(excepect, result);
        }


        [TestMethod]
        public void TestGetAllUnitLines()
        {
            List<Line> result = new List<Line>(Space.D2.GetAllUnitLines());
            List<Line> excepect = new List<Line> { Space.D2.GetLine(1, 0), Space.D2.GetLine(-1, 0), Space.D2.GetLine(0, 1), Space.D2.GetLine(0, -1),
                Space.D2.GetLine(1, 1), Space.D2.GetLine(-1, 1), Space.D2.GetLine(1, -1), Space.D2.GetLine(-1, -1)};

            CollectionAssert.AreEquivalent(excepect, result);
        }
    }
}
