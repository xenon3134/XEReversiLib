using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XEReversiLib.Core.Common;

namespace XEReversiLib.Core.Space
{
    public class Space
    {
        public static readonly Space D1 = new Space(new List<Axis> { Axis.X });
        public static readonly Space D2 = new Space(new List<Axis> { Axis.X, Axis.Y });
        public static readonly Space D3 = new Space(new List<Axis> { Axis.X, Axis.Y, Axis.Z });
        public static readonly Space D4 = new Space(new List<Axis> { Axis.W, Axis.X, Axis.Y, Axis.Z });

        public Space(List<Axis> axes)
        {
            Axes = new SortedSet<Axis>(axes);
        }
        public SortedSet<Axis> Axes { get; }

        public int Dimension { get { return Axes.Count(); } }

        public Position GetPosition(params int[] values)
        {
            return new Position(this, values);
        }

        public Line GetLine(params int[] unit)
        {
            return new Line(GetPosition(unit));
        }

        public HashSet<Line> GetAllDirectionLines(params int[] unit)
        {
            Position basePosition = GetPosition(unit);
            HashSet<Position> units = basePosition.GetPermutationPositions();
            foreach (Axis axis in Axes)
            {
                HashSet<Position> mirrorUnits = units.Select(u => u.GetMirrorPosition(axis)).ToHashSet();
                units = units.Concat(mirrorUnits).ToHashSet();
            }
            return units.Select(u => new Line(u)).ToHashSet();
        }

        public HashSet<Line> GetAllUnitLines()
        {
            return Enumerable.Range(1, Dimension).SelectMany(i =>
            {
                List<int> unit = Enumerable.Range(0, i).Select(j => 1).ToList();
                return GetAllDirectionLines(unit.ToArray());

            }).ToHashSet();
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Space otherSpace = (Space)obj;
                return Axes.Count == otherSpace.Axes.Count && !Axes.Except(otherSpace.Axes).Any();
            }
        }

        public override int GetHashCode()
        {
            return Axes.GetHashCode();
        }

        public override string ToString()
        {
            return $"Space({string.Join(",", Axes.Select(axis => axis.Name))})";
        }

        public static Space operator +(Space a, Space b)
        {
            List<Axis> newAxes = new List<Axis>(a.Axes);
            newAxes.AddRange(b.Axes);
            return new Space(newAxes);
        }

    }


    public class Axis : IComparable
    {
        public static readonly Axis X = new Axis("x");
        public static readonly Axis Y = new Axis("y");
        public static readonly Axis Z = new Axis("z");
        public static readonly Axis W = new Axis("w");

        public Axis(string name)
        {
            Name = name;
        }
        public string Name { get; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Axis otherAxis = obj as Axis;
            if (otherAxis != null)
                return Name.CompareTo(otherAxis.Name);
            else
                throw new ArgumentException("Axis");
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return Name == ((Axis)obj).Name;
            }
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return $"axis-{Name}";
        }
    }

    public class Position
    {

        public Position(Space space, params int[] values)
        {
            Space = space;

            int i = 0;
            AxisVauleMap = new Dictionary<Axis, int>();
            foreach (Axis axis in space.Axes)
            {

                AxisVauleMap.Add(axis, i < values.Length ? values[i] : 0);
                i++;
            }
        }

        public Space Space { get; }
        public Dictionary<Axis, int> AxisVauleMap { get; }

        public int GetValue(Axis axis)
        {
            int value;
            return AxisVauleMap.TryGetValue(axis, out value) ? value : 0;
        }

        public List<int> GetValues()
        {
            return AxisVauleMap.Values.ToList();
        }


        public HashSet<Position> GetPermutationPositions()
        {
            return Utils.GetPermutation(GetValues()).Select(values => new Position(Space, values)).ToHashSet();

        }

        public Position GetMirrorPosition(params Axis[] axisArray)
        {
            Dictionary<Axis, int> newValueMap = new Dictionary<Axis, int>(AxisVauleMap);
            foreach (Axis axis in axisArray)
            {
                newValueMap[axis] = GetValue(axis) * (-1);
            }
            return new Position(Space, newValueMap.Values.ToArray());
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Position otherPosition = (Position)obj;
                return AxisVauleMap.Count == otherPosition.AxisVauleMap.Count
                    && !AxisVauleMap.Except(otherPosition.AxisVauleMap).Any();
            }
        }

        public override int GetHashCode()
        {
            return AxisVauleMap.Select((pair, index) => index * 31 + pair.Value).Aggregate((sum, next) => sum += next);
        }

        public override string ToString()
        {
            return $"Position({string.Join(",", AxisVauleMap.Select(pair => $"{pair.Key.Name}:{pair.Value}"))})";
        }

        public static Position operator +(Position a, Position b)
        {
            Space newSpace = a.Space + b.Space;

            IList<int> values = new List<int>();
            foreach (Axis axis in newSpace.Axes)
            {
                values.Add(a.GetValue(axis) + b.GetValue(axis));
            }
            return new Position(newSpace, values.ToArray());
        }

        public static Position operator -(Position a, Position b)
        {
            Space newSpace = a.Space + b.Space;

            IList<int> values = new List<int>();
            foreach (Axis axis in newSpace.Axes)
            {
                values.Add(a.GetValue(axis) - b.GetValue(axis));
            }
            return new Position(newSpace, values.ToArray());
        }
    }

    public class Line
    {

        public Line(Position unit)
        {
            Unit = unit;
        }
        public Position Unit { get; }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return Unit.Equals(((Line)obj).Unit);
            }
        }

        public override int GetHashCode()
        {
            return Unit.GetHashCode();
        }

        public override string ToString()
        {
            return $"Line({Unit})";
        }

        public static Line operator +(Line a, Line b)
        {
            return new Line(a.Unit + b.Unit);
        }

        public static Line operator -(Line a, Line b)
        {
            return new Line(a.Unit - b.Unit);
        }
    }
}
