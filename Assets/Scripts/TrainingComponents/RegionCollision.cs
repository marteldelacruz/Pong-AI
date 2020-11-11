using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionCollision : MonoBehaviour
{
    public int Class;

    public bool IsRecording = false;

    public RegionCollision UpBrother;
    public RegionCollision DownBrother;

    [HideInInspector]
    public Vector2 BallPosition, BallDir;

    private List<float> positions;
    private List<Vector2> directions;
    private bool isColliding = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(Tags.BALL))
        {
            collision.gameObject.GetComponent<BallMovement>()
                        .ChangeBallDirection(Vector3.zero, false);
            collision.gameObject.GetComponent<BallMovement>().IncreaseBallSpeed();

            if (IsRecording && IsFirstCollision())
            {
                positions.Add(BallPosition.y);
                directions.Add(BallDir);
            }
        }
    }

    private bool IsFirstCollision()
    {
        bool isFirst = true;

        if (UpBrother != null)
            isFirst = !UpBrother.isColliding;

        if (isFirst && DownBrother != null)
            isFirst = !DownBrother.isColliding;

        return isFirst;
    }
}
