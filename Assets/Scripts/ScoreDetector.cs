using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreDetector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    ///     Detects the collision with the ball on a wall
    /// </summary>
    /// <param name="collision">  Current collider object.  </param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            // tag indicator
            switch (gameObject.tag)
            {
                case Tags.LEFT_WALL:
                    collision.gameObject.GetComponent<BallMovement>().UpdateScore(false);
                    break;
                case Tags.RIGHT_WALL:
                    collision.gameObject.GetComponent<BallMovement>().UpdateScore();
                    break;
            }

            GetComponent<AudioSource>().Play();
        }
    }
}
