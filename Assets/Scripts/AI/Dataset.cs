using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class Dataset
{
    public Matrix<float> Desired, Samples;

    private List<float> samples;
    private List<float> desireds;

    public int NumSamples { get { return Samples.RowCount; } }

    public Dataset() 
    {
        samples = new List<float>();
        desireds = new List<float>();
    }

    /// <summary>
    ///     Adds a new training sample
    /// </summary>
    /// <param name="dir">      Ball's direction    </param>
    /// <param name="desired">  Desired value       </param>
    public void AddSample(float yDist, float desired)
    {
        samples.Add(yDist);
        desireds.Add(desired);
    }

    /// <summary>
    ///     Builds matrices of this dataset
    /// </summary>
    public void Build()
    {
        var M = Matrix<float>.Build;
        int i = 0;
        Samples = M.Dense(samples.Count, 1, 0);
        Desired = M.Dense(samples.Count, 1, 0);

        foreach(Vector<float> row in Samples.EnumerateRows())
        {
            row[0] = samples[i];
            i++;
        }
        i = 0;
        foreach(Vector<float> row in Desired.EnumerateRows())
        {
            row[0] = desireds[i];
            i++;
        }
    }

    /// <summary>
    ///     Clears all training samples
    /// </summary>
    public void Clear()
    {
        desireds.Clear();
        samples.Clear();
    }
}
