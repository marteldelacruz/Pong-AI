using MathNet.Numerics.LinearAlgebra;
using Multilayer_Backprop;
using System;

public class DiferentialEvolution
{
    public float F = 1.6f;
    public double CR = 0.6f;
    public PlayerAI[] Individuals;
    public int[] Topology;
    public static Random r = new Random();

    public void Init(PlayerAI[] individuals)
    {
        Individuals = individuals;
    }

    /// <summary>
    ///     Generates 3 random number using a highly optimized function
    /// </summary>
    /// <param name="currentIndex"></param>
    private int[] GenerateRandomIndex(int currentIndex)
    {
        int[] indexes = new int[3];
        bool repeated = true;

        while (repeated)
        {
            indexes[0] = r.Next(0, Individuals.Length - 1);
            indexes[1] = r.Next(0, Individuals.Length - 1);
            indexes[2] = r.Next(0, Individuals.Length - 1);

            repeated = (indexes[0] == indexes[1] || indexes[0] == indexes[2] 
                || indexes[1] == indexes[2] || indexes[0] == currentIndex
                || indexes[1] == currentIndex || indexes[2] == currentIndex);
        }

        return indexes;
    }

    /// <summary>
    ///     Determinates which player has the best fitness
    /// </summary>
    public int GetBestPlayer()
    {
        int bestPlayer = 0;
        float error = Individuals[0].ErrorSum;

        for (int i = 0; i < Individuals.Length; i++)
        {
            if (error > Individuals[i].ErrorSum)
            {
                error = Individuals[i].ErrorSum;
                bestPlayer = i;
            }
        }
        return bestPlayer;
    }

    /// <summary>
    ///     Mutates Weight matrix of <paramref name="net"/> on given layer's index
    /// </summary>
    /// <param name="net">              Neural net to mutate                    </param>
    /// <param name="currIndividual">   The current individual index            </param>
    /// <param name="layerIndex">       Layer index                             </param>
    /// <param name="indexes">          Three diferent indexes of individuals   </param>
    private void Mutate(NeuralNet net, int currIndividual, int layerIndex, int[] indexes)
    {
        Matrix<float> W, 
            X = Individuals[currIndividual].Net.LayerGetW(layerIndex),
            x1 = Individuals[indexes[0]].Net.LayerGetW(layerIndex),
            x2 = Individuals[indexes[2]].Net.LayerGetW(layerIndex),
            x3 = Individuals[indexes[1]].Net.LayerGetW(layerIndex);

        // compute "V" using the three diferent individuals
        W = x1 + F * (x2 - x3);

        // compute "U" & swap values using random and CR value
        for (int i = 0; i < W.RowCount; i++)
            for (int j = 0; j < W.ColumnCount; j++)
                if (r.NextDouble() > CR)
                    //  Set original value of individual
                    W[i, j] = X[i, j];

        // reassing
        net.layers[layerIndex].W = W;
    }

    /// <summary>
    ///     Generates a mutated <see cref="NeuralNet"/> based on the given individual
    /// </summary>
    private NeuralNet GenerateMutation(int currIndividual)
    {
        NeuralNet mutatedNet = new NeuralNet();
        mutatedNet.Init(2, Topology);

        // find three random individuals
        var indices = GenerateRandomIndex(currIndividual);

        // mutate layer 0
        Mutate(mutatedNet, currIndividual, 0, indices);
        // mutate layer 1
        Mutate(mutatedNet, currIndividual, 1, indices);

        return mutatedNet;
    }

    /// <summary>
    ///     Start algorithm
    /// </summary>
    public void Algorithm()
    {
        float y;
        Topology = Individuals[0].Topology;

        for (int i = 0; i < Individuals.Length; i++)
        {
            var mutatedNet = GenerateMutation(i);
            // prepare dataset
            Individuals[i].dataset.Build();
            // compute
            y = mutatedNet.ComputeDataset(Individuals[i].dataset);
            // if error improved
            if (y < Individuals[i].ErrorAvrg)
            {
                // update with mutated net
                Individuals[i].Net = mutatedNet;
                // done like this to print correct error average later...
                Individuals[i].ErrorSum = y * Individuals[i].dataset.NumSamples;
            }
        }
    }
}
