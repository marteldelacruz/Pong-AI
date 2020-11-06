using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollision : MonoBehaviour
{
    public bool MuteAudio = true;

    /// <summary>
    ///     Detects the collision with the ball on a wall
    /// </summary>
    /// <param name="collision">  Current collider object.  </param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            collision.gameObject.GetComponent<BallMovement>().ChangeBallDirection(Vector3.zero);
            if (!MuteAudio)
                GetComponent<AudioSource>().Play(0);
        }
    }
}
