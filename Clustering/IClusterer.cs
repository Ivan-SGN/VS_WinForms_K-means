using System;
using System.Collections.Generic;
using System.Text;
using LinearAalgebra;


namespace Clustering
{
    public interface IClusterer
    {
        IEnumerable<Cluster> Clusters { get; }
        void ClaculateData(List<IMathVector> vectors);
    }
}
