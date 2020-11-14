using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Data.Text;
using System;

namespace Multilayer_Backprop
{
    /// <summary>
    ///     Represents a neural network's layer
    /// </summary>
    [System.Serializable]
    public class Layer
    {
        public string Name;
        private int numNeurons, inputSize;
        public Matrix<float> W, Inputs, Outputs, OutputsPrime, Sensitivity;
        public static Random r = new Random();

        /// <summary>
        ///     Needed for XML serialization
        /// </summary>
        public Layer() { }

        public Layer(int neurons, int inputsDim)
        {
            Init(neurons, inputsDim);
        }

        /// <summary>
        ///     Initializes/Resets weight matrix to given fixed size, of random values
        /// </summary>
        /// <param name="neurons">      Number of neurons   </param>
        /// <param name="inputsDim">    Size of inputs <c>(Not including bias)</c></param>
        public void Init(int neurons, int inputsDim)
        {
            numNeurons = neurons;
            inputSize = inputsDim + 1;
            Init();
        }

        /// <summary>
        ///     Initializes/Resets weight matrix of this layer to random values
        /// </summary>
        public void Init()
        {
            // NxD matrix of this layer (N = num-neurons, D = num-weights), including bias
            W = Matrix<float>.Build.Dense(numNeurons, inputSize);

            for (int i = 0; i < W.RowCount; i++)
            {
                for (int j = 0; j < W.ColumnCount; j++)
                    W[i, j] = (float)r.NextDouble();
            }
        }

        /// <summary>
        ///     Saves this layer to given path
        /// </summary>
        /// <param name="basePath"> Should be a folder name <c>Assets/SavedNetworks/AIType/</c></param>
        public void Save(string basePath)
        {
            string fileName = basePath + Name + ".csv";
            DelimitedWriter.Write(fileName, W, ",");
        }

        /// <summary>
        ///     Loads this layer from given path
        /// </summary>
        /// <param name="basePath"> Should be a folder name <c>Assets/SavedNetworks/AIType/</c></param>
        public void Load(string basePath)
        {
            W = DelimitedReader.Read<float>(basePath + Name + ".csv", false, ",", false);
        }

        /// <summary>
        ///     Computes outputs of this layer
        /// </summary>
        /// <param name="inputs">   An <c>(M x D)</c> input matrix  </param>
        /// <returns>   Matrix result of computation    </returns>
        public Matrix<float> Compute(Matrix<float> inputs)
        {
            var M = Matrix<float>.Build;
            Matrix<float> dot; 

            Inputs = M.DenseOfMatrix(inputs);
            dot = W * Inputs;

            Func<float, float> sigmoid = (x_dot) => {
                return (float)SpecialFunctions.Logistic((double)x_dot);
            },
            sigmoidPrime = (x_dot) => {
                float yAux = (float)SpecialFunctions.Logistic((double)x_dot);
                return yAux * (1 - yAux);
            };

            Outputs = dot.Map(sigmoid, Zeros.Include);
            OutputsPrime = dot.Map(sigmoidPrime, Zeros.Include);

            return Outputs;
        }

        /// <summary>
        ///     Computes this <see cref="Layer"/>'s sensitivity
        /// </summary>
        /// <param name="next">     Next layer (sensitivity already computed)           </param>
        /// <param name="error">    
        ///                         <c>IF <paramref name="error"/> != null</c><br></br>
        ///                         Computes sensitivity using output layer's formula   
        /// </param>
        public void ComputeSensitivity(Layer next, Matrix<float> error = null)
        {
            if (error == null)
                ComputeMLayerSensitivity(next);
            else
                // Only happens with last layer
                ComputeLastLayerSensitivity(error);
        }

        /// <summary>
        ///     Updates weights using computed sensitivities
        /// </summary>
        /// <param name="eta">  Learning rate   </param>
        public void UpdateWeights(float eta)
        {
            W += -eta * Sensitivity * Inputs.Transpose();
        }

        /// <summary>
        ///     Computes last layer's sensitivity
        /// </summary>
        /// <param name="error">    The neural network's error  </param>
        private void ComputeLastLayerSensitivity(Matrix<float> error)
        {
            Sensitivity = -2 * error.PointwiseMultiply(OutputsPrime);
        }

        /// <summary>
        ///     Computes an m-Layer's sensitivity
        /// </summary>
        /// <param name="next"> Next layer (should have its sensitivity already)    </param>
        private void ComputeMLayerSensitivity(Layer next)
        {
            var M = Matrix<float>.Build;
            int neurons = W.RowCount;
            // Ignore bias' weight
            Matrix<float> next_W = next.W.RemoveColumn(next.W.ColumnCount - 1), 
                          jacobian = M.Dense(neurons, neurons, 0);

            for (int i = 0; i < neurons; i++)
                jacobian[i, i] = OutputsPrime[i, 0];

            //jacobian = next_W * jacobian;

            Sensitivity = jacobian * next_W.Transpose() * next.Sensitivity;
        }
    }
}
