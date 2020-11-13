using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBallInMiddle : MonoBehaviour
{
    public RegionCollision MainRegion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.BALL)
        {
            Vector2 dir = other.gameObject.GetComponent<BallMovement>().BallDirection;

            Debug.Log("Pelota pasa por el centro");

            MainRegion.BallPosition = other.gameObject.transform.localPosition;

            MainRegion.BallDir = dir;
        }
    }
}
