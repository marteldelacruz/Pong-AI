using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Translate options")]
    [Tooltip("Current player speed")]
    [SerializeField]
    private float Speed = 20f;
    [Header("Input")]
    [Tooltip("Allow keyboard input?")]
    public bool UserInput = true;
    [HideInInspector]
    public Vector3 MovementDir = Vector3.zero;

    private Rigidbody playerRigidbody = null;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
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
            else
                playerRigidbody.velocity = Vector3.zero;
        }
        else   
        {
            if (Input.GetKey(KeyCode.UpArrow))
                MovePlayer();
            else if (Input.GetKey(KeyCode.DownArrow))
                MovePlayer(false);
            else
                playerRigidbody.velocity = Vector3.zero;
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

        playerRigidbody.velocity = MovementDir * Speed;
    }
}
