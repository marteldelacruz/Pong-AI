using UnityEngine;

public class CollisionDetection : MonoBehaviour
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            // tag indicator
            switch (gameObject.tag)
            {
                case Tags.PLAYER:
                    collision.gameObject.GetComponent<BallMovement>().ChangeBallDirection(false);
                    collision.gameObject.GetComponent<BallMovement>().IncreaseBallSpeed();
                    break;
                case Tags.LEFT_WALL:
                    collision.gameObject.GetComponent<BallMovement>().UpdateScore(false);
                    break;
                case Tags.RIGHT_WALL:
                    collision.gameObject.GetComponent<BallMovement>().UpdateScore();
                    break;
                default:
                    collision.gameObject.GetComponent<BallMovement>().ChangeBallDirection();
                    break;
            }

            GetComponent<AudioSource>().Play(0);
        }
    }
}
