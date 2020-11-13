using MathNet.Numerics.LinearAlgebra;
using System.Collections;
using UnityEngine;

namespace Multilayer_Backprop
{
    /// <summary>
    ///     Represents a neural network 
    /// </summary>
    public class NeuralNet
    {
        public Layer[] layers;
        private float ETA, MIN_ERROR;

        /// <summary>
        ///     Initializes perceptron with:<br></br> <c>eta = 0.5</c>
        /// </summary>
        /// <param name="td">   Training data's handler reference   </param>
        public NeuralNet()
        {
            ETA = 0.1f;
            MIN_ERROR = 0.01f;
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
        /// <param name="ballYDist"> Ball distance from player </param>
        /// <returns>   Float with the output value </returns>
        public float Compute(float ballYDist)
        {
            Matrix<float> y;
            Matrix<float> input = Matrix<float>.Build.DenseOfArray(new float[,] {
                { ballYDist , -1}
            });

            ForwardProp(input, out y);

            return y[0, 0];
        }

        /// <summary>
        /// Computes forward prop with given dataset and returns average error
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        public float ComputeDataset(Dataset dataset)
        {
            var M = Matrix<float>.Build;
            Matrix<float> input, output;

            // Append -1 constant
            input = dataset.Samples.Append(M.Dense(dataset.NumSamples, 1, -1));
            // compute all samples
            ForwardProp(input, out output);
            // error
            output = dataset.Desired - output.Transpose();
            // power of 2
            output = output.PointwisePower(2);
            // get average error of all samples' outputs
            return output.ColumnSums()[0] / dataset.NumSamples;
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
        ///     Starts neuralnet's learning algorithm
        /// </summary>
        public IEnumerator BackpropagationTraining(Matrix<float> inputs, Matrix<float> desireds)
        {
            int numIter = 0, numSamples = inputs.RowCount;
            bool isDoneTraining = numSamples == 0;
            float errorSum = 0, sum;
            var M = Matrix<float>.Build;
            Matrix<float> X = inputs.Append(M.Dense(numSamples, 1, -1)),
                          error, y, sample, desired;

            while (!isDoneTraining)
            {
                sum = 0;
                for (int i = 0; i < numSamples; i++)
                {
                    sample = X.Row(i).ToRowMatrix();
                    // desired to row-mayor matrix
                    desired = desireds.Row(i).ToRowMatrix().Transpose();

                    ForwardProp(sample, out y);
                    error = desired - y;
                    Backprop(error);
                    UpdateWeights();

                    error = error.PointwisePower(2);
                    sum += error.ColumnSums().Sum();
                }
                errorSum = sum / numSamples;
                if (numIter % 50 == 0)
                {   // Notify Ui of weight change
                    Debug.Log(
                        string.Format("<size=22><color=blue>EPOCH={0}</color>     <color=red>ERROR={1:0.000}</color></size>", numIter, errorSum)
                    );
                }
                numIter++;
                isDoneTraining = (errorSum < MIN_ERROR || numIter > 5000);
                yield return null;
            }
            Debug.Log(
                string.Format("<size=22>NEURAL-NET Done with <color=red>ERROR={0:0.000}</color> and <color=blue>EPOCH={1}</color></size>", errorSum, numIter)
            );
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
