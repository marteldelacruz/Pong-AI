using MathNet.Numerics.LinearAlgebra;
using UnityEngine;

namespace Multilayer_Backprop
{
    /// <summary>
    ///     Represents a neural network 
    /// </summary>
    public class NeuralNet
    {
        public Layer[] layers;
        private float ETA;

        /// <summary>
        ///     Initializes perceptron with:<br></br> <c>eta = 0.5</c>
        /// </summary>
        /// <param name="td">   Training data's handler reference   </param>
        public NeuralNet()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Matrix<float> LayerGetW(int index)
        {
            return layers[index].W;
        }

        /// <summary>
        ///     Computes the output of the given position of the ball
        /// </summary>
        /// <param name="ballDirection"> Current ball direction </param>
        /// <returns>   Float with the output value </returns>
        public float Compute(Vector2 ballDirection)
        {
            Matrix<float> y;
            Matrix<float> input = Matrix<float>.Build.DenseOfArray(new float[,] {
                { ballDirection.x, ballDirection.y , -1}
            });

            ForwardProp(input, out y);

            return y[0, 0];
        }

        /// <summary>
        ///     Builds a new neural network based on the given topology
        /// </summary>
        /// <param name="inputSize">    Size of the Input layer (training sample's dimension size)  </param>
        /// <param name="topology">     Number of neurons in each layer                             </param>
        public void Init(int inputSize, int[] topology)
        {
            int dim = inputSize; // x1 and x2 (bias is added within Layer)

            layers = new Layer[topology.Length];

            for (int i = 0; i < topology.Length; i++)
            {
                layers[i] = new Layer(topology[i], dim);
                layers[i].Name = 
                    (i < topology.Length - 1) ? "Layer " + (i + 1).ToString() : "Output layer";
                dim = topology[i];
            }
        }

        /// <summary>
        ///     Computes the input through all layers
        /// </summary>
        /// <param name="x">        Input vector (training sample)              </param>
        /// <param name="y">        Matrix containing neural netowrk's output   </param>
        private void ForwardProp(Matrix<float> x, out Matrix<float> y)
        {
            var M = Matrix<float>.Build;
            Matrix<float> input = x.Transpose(), aux; // To row-mayor (Nx1)
            foreach(Layer layer in layers)
            {
                input = layer.Compute(input);
                // keep adding bias constant
                aux = M.Dense(input.RowCount + 1, input.ColumnCount, -1);
                aux.SetSubMatrix(0, 0, input);
                input = aux;
            }
            // final outputs (removes bias constant)
            y = input.RemoveRow(input.RowCount - 1);
        }

        /// <summary>
        ///     Computes backpropagation on the neural network
        /// </summary>
        /// <param name="error">    The error matrix <c>(Yd - Y)</c></param>
        private void Backprop(Matrix<float> error)
        {
            int i = layers.Length - 1;

            // Compute on output layer
            layers[i].ComputeSensitivity(null, error);
            // Compute on the rest...
            for (int j = i - 1; j > -1; j--)
                layers[j].ComputeSensitivity(layers[j + 1]);
        }

        /// <summary>
        ///     Updates weight matrix of each <see cref="Layer"/> of this network
        /// </summary>
        private void UpdateWeights()
        {
            foreach (Layer layer in layers)
                layer.UpdateWeights(ETA);
        }
    }
}
