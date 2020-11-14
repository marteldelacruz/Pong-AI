using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGuard : MonoBehaviour
{
    public BallMovement Ball;
    public PlayerControl Player1;
    public PlayerControl Player2;

    private Bounds bounds;

    private void Start()
    {
        bounds = GetComponent<BoxCollider>().bounds;
    }

    private void LateUpdate()
    {
        if (!bounds.Contains(Ball.transform.position))
            Ball.RestartBallPosition();
        
        if (!bounds.Contains(Player1.transform.position))
            Player1.RestartPosition();

        if (!bounds.Contains(Player2.transform.position))
            Player2.RestartPosition();
    }
}
