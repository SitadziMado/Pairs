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

        public void Connect(int firstIndex, int secondIndex)
        {
            mMatrix[firstIndex][secondIndex] = EdgeState.Black;
        }

        public void FindPairs()
        {
            for (int i = 0; i < mCount; ++i)
            {
                if (!mMatrix[i].Contains(EdgeState.Red))
                {

                }
            }
        }

        private void ClearPrevious()
        {
            for (int i = 0; i < mCount; ++i)
            {
                mPreviousFirst[i] = -1;
                mPreviousSecond[i] = -1;
            }
        }

        private List<List<EdgeState>> mMatrix;
        private List<int> mPreviousFirst;
        private List<int> mPreviousSecond;
        private int mCount;
    }
}
