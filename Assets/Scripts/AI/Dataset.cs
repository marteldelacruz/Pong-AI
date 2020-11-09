using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MathNet.Numerics.LinearAlgebra;

public class Dataset
{
    public Matrix<float> Desired, Samples;

    private List<Vector2> directions;
    private List<float> desireds;

    public int NumSamples { get { return Samples.RowCount; } }

    public Dataset() 
    {
        directions = new List<Vector2>();
        desireds = new List<float>();
    }

    /// <summary>
    ///     Adds a new training sample
    /// </summary>
    /// <param name="dir">      Ball's direction    </param>
    /// <param name="desired">  Desired value       </param>
    public void AddSample(Vector2 dir, float desired)
    {
        directions.Add(dir);
        desireds.Add(desired);
    }

    /// <summary>
    ///     Builds matrices of this dataset
    /// </summary>
    public void Build()
    {
        var M = Matrix<float>.Build;
        int i = 0;
        Samples = M.Dense(directions.Count, 2, 0);
        Desired = M.Dense(directions.Count, 1, 0);

        foreach(Vector<float> row in Samples.EnumerateRows())
        {
            row[0] = directions[i].x;
            row[1] = directions[i].y;
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
        directions.Clear();
    }
}
