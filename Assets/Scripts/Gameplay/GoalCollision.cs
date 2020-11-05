using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollision : MonoBehaviour
{
    public ScoreManager ScoreManager;
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            // tag indicator
            ScoreManager.UpdateScore();
            // restart position
            collision.GetComponent<RandomBallThrow>().Respawn();

            if (!MuteAudio)
                GetComponent<AudioSource>().Play(0);
        }
    }
}
