using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pairs
{
    public class BipartiteGraph
    {
        private enum EdgeState
        {
            None,
            Black,
            Red
        }

        public BipartiteGraph(int length)
        {
            mMatrix = new List<List<EdgeState>>(length);

            for (int i = 0; i < length; ++i)
            {
                mMatrix.Add(new List<EdgeState>(length));

                for (int j = 0; j < length; ++j)
                    mMatrix[i].Add(EdgeState.None);
            }

            mPreviousFirst = new List<int>(length);
            mPreviousSecond = new List<int>(length);

            for (int i = 0; i < length; ++i)
            {
                mPreviousFirst.Add(-1);
                mPreviousSecond.Add(-1);
            }

            mCount = length;
        }

        private EdgeState this[int firstIndex, int secondIndex]
        {
            get
            {
                if (0 < firstIndex || firstIndex >= mCount ||
                    0 < secondIndex || secondIndex >= mCount)
                    throw new ArgumentOutOfRangeException("Недействительное ребро.");

                return mMatrix[firstIndex][secondIndex];
            }
            set
            {
                if (0 < firstIndex || firstIndex >= mCount ||
                    0 < secondIndex || secondIndex >= mCount)
                    throw new ArgumentOutOfRangeException("Недействительное ребро.");

                    mMatrix[firstIndex][secondIndex] = value;
            }
        }

        public void Connect(int firstIndex, int secondIndex)
        {
            mMatrix[firstIndex][secondIndex] = EdgeState.Black;
        }

        public void FindPairs()
        {
            ClearPrevious();
            InitialPaint();

            bool p(Tuple<int, int> ij) =>
                !ColHasRed(ij.Item1) && 
                !RowHasRed(ij.Item2) && 
                this[ij.Item1, ij.Item2] == EdgeState.Black;

            var fst = FindFirstEdge(p);

            if (fst.Item1 == -1)
                return;

            // Неопределенная вершина найдена!
        }

        private void ClearPrevious()
        {
            for (int i = 0; i < mCount; ++i)
            {
                mPreviousFirst[i] = -1;
                mPreviousSecond[i] = -1;
            }

            for (int i = 0; i < mCount; ++i)
                for (int j = 0; j < mCount; ++j)
                    if (this[i, j] == EdgeState.Red)
                        this[i, j] = EdgeState.Black;
        }

        private void InitialPaint()
        {
            bool p(EdgeState es) => es == EdgeState.Black;

            for (int i = 0; i < mCount; ++i)
                if (!RowHasRed(i))
                {
                    var index = mMatrix[i].FindIndex(p);

                    while (index != -1 || ColHasRed(index))
                        index = mMatrix[i].FindIndex(index + 1, p);

                    if (index == -1)
                        throw new UnsupportedGraphException("Граф несвязный.");

                    this[i, index] = EdgeState.Red;
                    // this[index, i] = EdgeState.Red;
                }
        }

        private bool RowHasRed(int index) => mMatrix[index].Contains(EdgeState.Red);

        private bool ColHasRed(int index)
        {
            for (int i = 0; i < mCount; ++i)
            {
                if (this[index, i] == EdgeState.Red)
                    return true;
            }
            
            return false;
        }

        private Tuple<int, int> FindFirstEdge(Predicate<Tuple<int, int>> p)
        {
            for (int i = 0; i < mCount; ++i)
                for (int j = 0; j < mCount; ++j)
                {
                    var t = new Tuple<int, int>(i, j);

                    if (p(t))
                        return t;
                }

            return new Tuple<int, int>(-1, -1);
        }

        private List<List<EdgeState>> mMatrix;
        private List<int> mPreviousFirst;
        private List<int> mPreviousSecond;
        private int mCount;
    }
}
