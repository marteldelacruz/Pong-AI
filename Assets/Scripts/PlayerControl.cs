using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [HideInInspector]
    private float MAX_UP = 3.7f;
    [HideInInspector]
    private float MIN_DOWN = -3.7f;
    [HideInInspector]
    private Vector3 MinRecoil = new Vector3(0, 0.05f, 0);

    [Tooltip("Current player speed")]
    private float Speed = 2f;

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
    ///     Determinates if the player is out of bounds based on
    ///     its Y coord. There is a Min and Max value for this.
    /// </summary>
    /// <returns>   True if the player is out of bounds. </returns>
    private bool IsPlayerOutOfBounds()
    {
        Vector3 currentPos = gameObject.transform.position;

        if (currentPos.y > MAX_UP || currentPos.y < MIN_DOWN)
            return true;

        return false;
    }

    /// <summary>
    ///     Moves the current player up or down
    /// </summary>
    /// <param name="isUp"> Are we moving up?   </param>
    private void MovePlayer(bool isUp=true)
    {
        Vector3 recoil = (isUp) ? -MinRecoil : MinRecoil;
        Vector3 dir = (isUp) ? Vector3.up : Vector3.down;
        dir *= Time.deltaTime * Speed;

        if (!IsPlayerOutOfBounds())
            gameObject.transform.position += dir;
        else
            gameObject.transform.position += recoil;
    }
}
