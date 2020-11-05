using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuellarPlayer : MonoBehaviour
{
    public bool MuteAudio;

    // Start is called before the first frame update
    void Start()
    {
        MuteAudio = true;
    }

    /// <summary>
    ///     Detects the collision with the ball on a wall
    /// </summary>
    /// <param name="collision">  Current collider object.  </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            collision.gameObject.GetComponent<BallMovement>()
                        .BigCuellarChangeBallDirection();
            collision.gameObject.GetComponent<BallMovement>().IncreaseBallSpeed();

            if (!MuteAudio)
                GetComponent<AudioSource>().Play(0);
        }
    }
}
