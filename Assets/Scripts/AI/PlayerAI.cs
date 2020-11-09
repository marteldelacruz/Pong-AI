using MathNet.Numerics.LinearAlgebra;
using Multilayer_Backprop;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public BallMovement Ball;
    public int[] Topology = new int[] { 6, 1 };
    public NeuralNet Net;
    public float Desired, y, Error, ErrorSum;

    public Dataset dataset;

    private Vector3 initialPos;

    /// <summary>
    ///     Gets error average ot this <see cref="PlayerAI"/>'s neural network
    /// </summary>
    public float ErrorAvrg { get { return ErrorSum / dataset.NumSamples; } }

    // Start is called before the first frame update
    void Start()
    {
        InitNeuralNet();
    }

    private void OnEnable()
    {
        if (dataset == null)
            InitNeuralNet();

        // Restart dataset after been desabled
        dataset.Clear();
        transform.localPosition = initialPos;
        ErrorSum = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Ball.BallDirection.x < 0)
        {
            AIMovement();
        }
    }

    /// <summary>
    ///     Computes neural net's output and moves itself
    /// </summary>
    public void AIMovement()
    {
        if (Ball.transform.position.y == transform.position.y)
            Desired = 0;
        else
            Desired = Mathf.Sign(Ball.transform.position.y - transform.position.y);

        y = Net.Compute(Ball.BallDirection);
        Error = (Desired - y) * (Desired - y);
        ErrorSum += Error;

        dataset.AddSample(Ball.BallDirection, Desired);
        
        if (y > 0)
            GetComponent<PlayerControl>().MovePlayer();
        else if (y < 0)
            GetComponent<PlayerControl>().MovePlayer(false);
    }

    /// <summary>
    ///     Initializes the neural net of the current player
    ///     with the desired topology
    /// </summary>
    public void InitNeuralNet()
    {
        dataset = new Dataset();
        Net = new NeuralNet();
        Net.Init(2, Topology);
        ErrorSum = 0;

        initialPos = transform.localPosition;
    }
}
