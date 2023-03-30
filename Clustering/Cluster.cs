using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;
using LinearAalgebra;

namespace Clustering
{
    public  class Cluster
    {
        private int _id;
        private IMathVector _clusterCenter;
        private List<IMathVector> _clusterVectors = new List<IMathVector>();

        public int GetId { get => _id; }
        public IMathVector GetCenter { get => _clusterCenter; }
        
        public List<IMathVector> GetClusters { get =>  _clusterVectors; }

        public void AddPoint(IMathVector vector)
        {
            _clusterVectors.Add(vector);
        }
        public Cluster(IMathVector clusterCenter, int id)
        {
            _id = id;
            _clusterCenter = clusterCenter;
        }

        public void ResetCenter()
        {
            double sumX = 0, sumY = 0;
            for(int i = 0; i < _clusterVectors.Count; i++)
            {
                sumX += _clusterVectors[i][0];
                sumY += _clusterVectors[i][1];
            }
            double[] newCentroid = { (sumX / _clusterVectors.Count), (sumY / _clusterVectors.Count) };
            var vec = new MathVector(newCentroid);
            _clusterCenter = vec;
            _clusterVectors.Clear();
        }
    }
}
