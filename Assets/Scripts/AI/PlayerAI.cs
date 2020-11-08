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
    public List<Vector2> BallPositions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        InitNeuralNet();
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
    /// 
    /// </summary>
    public void AIMovement()
    {
        BallPositions.Add(Ball.BallDirection);
        Desired = Ball.BallDirection.y - transform.position.y;
        y = Net.Compute(Ball.BallDirection);
        Error = (Desired - y) * (Desired - y);
        ErrorSum += Error;

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
        Net = new NeuralNet();
        Net.Init(2, Topology);
        ErrorSum = 0;
    }
}
