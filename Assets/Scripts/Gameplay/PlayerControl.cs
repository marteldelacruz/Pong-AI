using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Tooltip("Current player speed")]
    private float Speed = 10f;

    [HideInInspector]
    public Vector3 MovementDir = Vector3.zero;

    public bool UserInput = true;
    
    // Update is called once per frame
    void Update()
    {
        if (UserInput)
        {
            MovementDir.y = 0;
            DeterminatePlayerMovement();
        }
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
        MovementDir = (isUp) ? Vector3.up : Vector3.down;
        MovementDir *= Time.deltaTime * Speed;

        gameObject.transform.position += MovementDir;
    }
}
