using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBallInMiddle : MonoBehaviour
{
    public RegionCollision Region1;
    public RegionCollision Region2;
    public RegionCollision Region3;
    public RegionCollision Region4;
    public RegionCollision Region5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.BALL)
        {
            Vector2 dir = other.gameObject.GetComponent<BallMovement>().BallDirection;
            
            Region1.BallPosition = Region2.BallPosition = Region3.BallPosition = 
                Region4.BallPosition = Region5.BallPosition = other.gameObject.transform.localPosition;
        
            Region1.BallDir = Region2.BallDir = Region3.BallDir = 
                Region4.BallDir = Region5.BallDir = dir;
        }
    }
}
