using System.Collections;
using System.Collections.Generic;
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
    /// <param name="collision"></param>

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            switch (gameObject.tag)
            {
                case "Player":
                    collision.gameObject.GetComponent<BallMovement>().ChangeBallDirection(false);
                    break;
                case "LeftWall":
                    collision.gameObject.GetComponent<BallMovement>().UpdateScore(false);
                    break;
                case "RightWall":
                    collision.gameObject.GetComponent<BallMovement>().UpdateScore();
                    break;
                default:
                    collision.gameObject.GetComponent<BallMovement>().ChangeBallDirection();
                    break;
            } 
        }
    }
}
