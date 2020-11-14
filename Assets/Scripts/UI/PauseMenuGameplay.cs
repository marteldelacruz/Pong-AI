using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuGameplay : MonoBehaviour
{
    [Header("UI's general buttons")]
    public GameObject PlayerVsPlayerBtn;
    public GameObject PlayerVsAiBtn;
    public GameObject AiVsAiBtn;

    [Header("Players' game objects")]
    public GameObject Player1;
    public GameObject Player2;

    [Header("Level's props")]
    public DetectBallInMiddleGameplay LineInMiddle;

    /// <summary>
    ///     Alternates buttons' visibility
    /// </summary>
    public void SwitchPlayersButtons()
    {
        bool isBtnShown = !PlayerVsPlayerBtn.activeSelf;

        PlayerVsPlayerBtn.SetActive(isBtnShown);
        PlayerVsAiBtn.SetActive(isBtnShown);
        AiVsAiBtn.SetActive(isBtnShown);
    }

    /// <summary>
    ///     Enables/disables AI in Player1's reference
    /// </summary>
    public void AIvsHuman()
    {
        // disables user's input
        Player1.GetComponent<PlayerControl>().UserInput = false;

        // enable AI
        LineInMiddle.IsAIPlaying = true;
        Player1.GetComponent<PlayerNerualNet>().enabled = true;

        Debug.Log("IAvsPlayer => ACTIVADO");
    }

    /// <summary>
    ///     Enables/disables AI in player 1 and 2
    /// </summary>
    public void AIvsAI()
    {
        // disables user's input
        Player1.GetComponent<PlayerControl>().UserInput = false;
        Player2.GetComponent<PlayerControl>().UserInput = false;

        // enable AI
        LineInMiddle.IsAIPlaying = true;
        Player1.GetComponent<PlayerNerualNet>().enabled = true;
        Player2.GetComponent<PlayerAI>().enabled = true;
        Debug.Log("IAvsIA => ACTIVATED");
    }
}
