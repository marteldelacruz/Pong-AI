using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Tooltip("Current player speed")]
    private float Speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DeterminatePlayerMovement();
    }

    /// <summary>
    ///     Determinates which player is going to move
    /// </summary>
    private void DeterminatePlayerMovement()
    {
        if (gameObject.name == Tags.PLAYER_1)
        {
            if (Input.GetKey(KeyCode.W))
                MovePlayer();
            else if (Input.GetKey(KeyCode.S))
                MovePlayer(false);
        }
        else   
        {
            if (Input.GetKey(KeyCode.UpArrow))
                MovePlayer();
            else if (Input.GetKey(KeyCode.DownArrow))
                MovePlayer(false);
        }
    }

    /// <summary>
    ///     Moves the current player up or down
    /// </summary>
    /// <param name="isUp"> Are we moving up?   </param>
    private void MovePlayer(bool isUp=true)
    {
        Vector3 dir = (isUp) ? Vector3.up : Vector3.down;
        dir *= Time.deltaTime * Speed;

        gameObject.transform.position += dir;
    }
}
