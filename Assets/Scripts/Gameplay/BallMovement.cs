using MathNet.Numerics.LinearAlgebra;
using TMPro;
using UnityEngine;


public class BallMovement : MonoBehaviour
{
    [Tooltip("Ball speed")]
    public float Speed = 7f;
    [Tooltip("Ball initial position 3D Vector")]
    public Vector3 InitialPosition = new Vector3(0, 1, 0);


    public Vector3 BallDirection;
    private float MAX_SPEED = 10;

    public float Ydirection { set { BallDirection.y = value; } }
    public float Xdirection { set { BallDirection.x = value; } }
    
    // Start is called before the first frame update
    void Start()
    {
        // ball initial position
        gameObject.transform.localPosition = InitialPosition;
        // new direction
        BallDirection = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.localPosition += BallDirection * Time.deltaTime * Speed;
    }

    /// <summary>
    ///     Changes the ball direction
    /// </summary>
    /// <param name="isYAxis">  Indicates if the Y axis is going to change. </param>
    public void ChangeBallDirection(Vector3 playerFrictionDir, bool isYAxis = true)
    {
        if (isYAxis)
            //  Occurs for walls
            BallDirection.y *= -1;
        else
        {
            // Occurs for players
            BallDirection.x *= -1;
            if (playerFrictionDir.y != 0)
                BallDirection.y = Mathf.Sign(playerFrictionDir.y);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void BigCuellarChangeBallDirection()
    {
        BallDirection.x *= -1;
    }

    /// <summary>
    /// 
    /// </summary>
    public void RestartBallPosition()
    {
        gameObject.transform.localPosition = InitialPosition;
        BallDirection.x *= -1;
    }

    /// <summary>
    ///     Increases the ball speed by 0.2
    ///     We need to determinate if the speed increases when
    ///     a player hits the ball or when a player scores
    /// </summary>
    public void IncreaseBallSpeed()
    {
        // increase speed
        Speed = Mathf.Clamp(0.2f + Speed,2, MAX_SPEED);
    }
}
