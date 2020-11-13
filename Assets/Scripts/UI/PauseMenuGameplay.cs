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
        bool isHuman = Player1.GetComponent<PlayerControl>().UserInput;
        Player1.GetComponent<PlayerControl>().UserInput = !isHuman;
        LineInMiddle.IsAIPlaying = isHuman;
        Player1.GetComponent<PlayerNerualNet>().enabled = isHuman;
    }
}
