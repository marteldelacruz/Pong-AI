using MathNet.Numerics.LinearAlgebra;
using Multilayer_Backprop;
using System;

public class DiferentialEvolution
{
    public int BestPlayerIndex;
    public float F = 1.6f;
    public double CR = 0.6f;
    public PlayerAI[] Individuals;
    public int[] Topology = new int[] { 6, 1 };
    public static Random r = new Random();

    /// <summary>
    ///     Generates 3 random number using a highly optimized function
    /// </summary>
    /// <param name="currentIndex"></param>
    private int[] GenerateRandomIndex(int currentIndex)
    {
        int[] indexes = new int[3];

        while (true)
        {
            indexes[0] = r.Next(0, Individuals.Length - 1);
            indexes[1] = r.Next(0, Individuals.Length - 1);
            indexes[2] = r.Next(0, Individuals.Length - 1);

            if (indexes[0] != indexes[1] && indexes[0] != indexes[2]
                && indexes[1] != indexes[2] && indexes[0] != currentIndex
                && indexes[1] != currentIndex && indexes[2] != currentIndex)
                break;
        }

        return indexes;
    }

    /// <summary>
    ///     Determinates which player has the best fitness
    /// </summary>
    private void GetBestPlayer()
    {
        float error = Individuals[0].ErrorSum;

        for (int i = 0; i < Individuals.Length; i++)
        {
            if (error > Individuals[i].ErrorSum)
            {
                error = Individuals[i].ErrorSum;
                BestPlayerIndex = i;
            }
        }
    }

    /// <summary>
    ///     Sets the new V values of U vector (weights)
    /// </summary>
    /// <param name="net">          The u vector net                        </param>
    /// <param name="index">        The current individual index            </param>
    /// <param name="layerIndex">   Layer index                             </param>
    /// <param name="indexes">      Three diferent indexes of individuals   </param>
    private void SetV(NeuralNet net, int index, int layerIndex, int[] indexes)
    {
        // curent u weights of layerindex
        var W = net.layers[layerIndex].W;

        // re-compute using the three diferent individuals
        W = Individuals[indexes[0]].Net.LayerGetW(layerIndex);
        W += F * (Individuals[indexes[1]].Net.LayerGetW(layerIndex) 
            - Individuals[indexes[2]].Net.LayerGetW(layerIndex));

        // swap process using random and CR value
        for (int i = 0; i < W.RowCount; i++)
            for (int j = 0; j < W.RowCount; j++)
                if (r.NextDouble() > CR)
                    W[i, j] = Individuals[index].Net.LayerGetW(layerIndex)[i,j];

        // reassing
        net.layers[layerIndex].W = W;
    }

    /// <summary>
    /// 
    /// </summary>
    private NeuralNet GenerateMutation(int index, int layer)
    {
        NeuralNet net = new NeuralNet();
        net.Init(2, Topology);
        var indexes = GenerateRandomIndex(index);

        SetV(net, index, 0, indexes);
        SetV(net, index, 1, indexes);


    }

    /// <summary>
    /// 
    /// </summary>
    public void Algorithm()
    {
        for (int i = 0; i < Individuals.Length; i++)
        {
            Matrix<float> vi = NewMutationLayer(i, 0);
        }
    }
}
