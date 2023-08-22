using System;

namespace CrazyTank.Core
{
    public class GameOptimizer
    {
        public void Optimize()
        {
            GC.Collect(0, GCCollectionMode.Optimized);
        }
    }
}