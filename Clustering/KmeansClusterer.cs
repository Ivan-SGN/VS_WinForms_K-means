using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using LinearAalgebra;

namespace Clustering
{
    public class KmeansClusterer : IClusterer
    {
        private List<Cluster> _clusters = new List<Cluster>();
        private int _clusterCnt;
        private Random _random = new Random();
        IEnumerable<Cluster> IClusterer.Clusters => _clusters;
        public KmeansClusterer(int cnt)
        {
            _clusterCnt = cnt;
            _clusters = new List<Cluster>(cnt);
        }
        
       
        public void InitClusters(List<IMathVector> data)
        {
            for (int i = 0; i < _clusterCnt; i++)
            {
                _clusters.Add(new Cluster(data[i], i));
            }
        }

        Cluster DetermineClusterMembership(IMathVector vector)
        {
            double[] distancesToClusters = new double[_clusterCnt];
            for(int i = 0; i < _clusterCnt; i++)
            {
                distancesToClusters[i] = _clusters[i].GetCenter.CalcDistance(vector);
            }
            return _clusters[FindMinValue(distancesToClusters)];
        }

        public void FillClusters(IMathVector vector)
        {
            foreach (var cluster in _clusters)
            {
                if (cluster == DetermineClusterMembership(vector))
                    cluster.AddPoint(vector);
            }
        }
        public int FindMinValue(double[] arr)
        {
            int res = 0;
            double min = arr[0];
            for(int i = 1; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    res = i;
                }
            }
            return res;
        }
        void IClusterer.ClaculateData(List<IMathVector> vectors)
        {
            List<IMathVector> centroids = new List<IMathVector>(_clusterCnt);
            for (int i = 0; i < _clusterCnt; i++)
            {
                centroids.Add(vectors[_random.Next(0, vectors.Count)]);
            }
            InitClusters(centroids); 
            bool isChanged = true;
            int cnt = 0;
            IMathVector prevCentroid = new MathVector(2);
            while (isChanged)
            {
                for (int j = 0; j < vectors.Count; j++)
                {
                    FillClusters(vectors[j]);
                }
                foreach (var cluster in _clusters)
                {
                    prevCentroid = cluster.GetCenter;
                    cluster.ResetCenter();
                    if (cluster.GetCenter.CalcDistance(prevCentroid) < 1)
                        cnt++;
                }
                if (cnt == _clusterCnt)
                    isChanged = false;
                cnt = 0;
            }
        }
    }
}