﻿using TMPro;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [Tooltip("Ball speed")]
    public Vector2 Speed = new Vector2(1, 1);
    [Tooltip("Ball initial position 3D Vector")]
    public Vector3 InitialPosition = new Vector3(0, 0, -8);
    [Tooltip("Player one score text")]
    public TextMeshProUGUI Player1Text;
    [Tooltip("Player two score text")]
    public TextMeshProUGUI Player2Text;

    private Vector3 BallDirection;

    // Start is called before the first frame update
    void Start()
    {
        // ball initial position
        gameObject.transform.position = InitialPosition;
        // new direction
        BallDirection = new Vector3(1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += BallDirection * Time.deltaTime * 2;
    }

    /// <summary>
    ///     Changes the ball direction
    /// </summary>
    /// <param name="isYAxis">  Indicates if the Y axis is going to change. </param>
    public void ChangeBallDirection(bool isYAxis=true)
    {
        if (isYAxis)
            BallDirection.y *= -1;
        else
            BallDirection.x *= -1;
    }

    /// <summary>
    ///     Updates the score text shown on the top of the screen.
    ///     Each player has a text.
    /// </summary>
    /// <param name="isPlayer1">    Indicates if the player one has scored.   </param>
    public void UpdateScore(bool isPlayer1=true)
    {
        // current score text and new format
        string text = (isPlayer1) ? Player1Text.text : Player2Text.text;
        text = string.Format("{0:00}", int.Parse(text) + 1);

        // ball initial position
        gameObject.transform.position = InitialPosition;
        ChangeBallDirection(false);

        // update score text
        if (isPlayer1)
            Player1Text.text = text;
        else
            Player2Text.text = text;
    }
}
