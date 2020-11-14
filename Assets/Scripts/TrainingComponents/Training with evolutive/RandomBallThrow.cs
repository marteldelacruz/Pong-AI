using UnityEngine;

public class RandomBallThrow : MonoBehaviour
{
    [Header("Training settings")]
    [Tooltip("If true, will spawn respawn the ball on random positions")]
    public bool IsTraining = false;
    [Tooltip("Level height on Y axis")]
    public Vector2 Limits = new Vector2(-5.2f, 5.2f);

    //  Random instance
    private static System.Random Rand = new System.Random();
    // Reference to ball's movement
    private BallMovement bMovement;

    // Start is called before the first frame update
    void Start()
    {
        bMovement = GetComponent<BallMovement>();
    }

    /// <summary>
    ///     Computes random position of next ball
    /// </summary>
    public void RandomPosition()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = 0;
        pos.y = ((float)Rand.NextDouble() * Mathf.Abs(Limits.x - Limits.y)) + Limits.x ;
        gameObject.transform.position = pos;
        bMovement.Ydirection = ((float)Rand.NextDouble() * 2) - 1;
        bMovement.RestartBallPosition();
        bMovement.Xdirection = -bMovement.BallDirection.x;
    }

    /// <summary>
    ///     Positions the ball at some position
    /// </summary>
    public void Respawn()
    {
        if (IsTraining)
            RandomPosition();
        else
            bMovement.RestartBallPosition();
    }
}
