using UnityEngine;

public class RandomBallThrow : MonoBehaviour
{
    public bool IsTraining = false;
    public Vector2 Limits = new Vector2(-5.2f, 5.2f);
    public static System.Random Rand = new System.Random();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /// <summary>
    /// 
    /// </summary>
    public void RandomPosition()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = 0;
        pos.y = ((float)Rand.NextDouble() * Mathf.Abs(Limits.x - Limits.y)) + Limits.x ;
        gameObject.transform.position = pos;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Respawn()
    {
        if (IsTraining)
            RandomPosition();
        else
            GetComponent<BallMovement>().RestartBallPosition();
    }
}
