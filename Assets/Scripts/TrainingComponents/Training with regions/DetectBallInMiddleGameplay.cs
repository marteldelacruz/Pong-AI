using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Detects when the ball crosses the level's middle line
/// </summary>
public class DetectBallInMiddleGameplay : MonoBehaviour
{
    [Tooltip("Player1's component which handles AI")]
    public PlayerNerualNet PlayerNeuralNet;
    [Tooltip("Will send data to AI if true")]
    public bool IsAIPlaying = false;

    /// <summary>
    ///     Sends inputs to AI when ball detection
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (IsAIPlaying && other.gameObject.tag == Tags.BALL)
        {
            Vector2 dir = other.gameObject.GetComponent<BallMovement>().BallDirection,
                    localPos = other.gameObject.transform.localPosition;
            // only notify the IA when the ball goes towards it
            if (dir.x < 0)
            {
                Debug.Log("Pelota pasa por el centro");
                PlayerNeuralNet.Predict(new Vector3(dir.x, dir.y, localPos.y));
            }
        }
    }
}
