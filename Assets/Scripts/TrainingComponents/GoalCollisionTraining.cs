using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCollisionTraining : MonoBehaviour
{
    public GameObject ThisInidivial;
    public TrainingHandler TrainingHandler;
    public bool MuteAudio = true;

    /// <summary>
    ///     Detects the collision with the ball and scores a point to the player's score
    /// </summary>
    /// <param name="other">  Current collider object.  </param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.BALL))
        {
            // restart position
            other.GetComponent<RandomBallThrow>().Respawn();
            TrainingHandler.DisabledPlayers++;
            ThisInidivial.SetActive(false);

            if (!MuteAudio)
                GetComponent<AudioSource>().Play(0);
        }
    }
}
