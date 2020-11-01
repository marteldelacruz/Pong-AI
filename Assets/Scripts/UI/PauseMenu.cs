using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject PlayerVsPlayerBtn;
    public GameObject PlayerVsAiBtn;
    public GameObject AiVsAiBtn;

    public void SwitchPlayers()
    {
        bool isBtnShown = !PlayerVsPlayerBtn.activeSelf;
        PlayerVsPlayerBtn.SetActive(isBtnShown);
        PlayerVsAiBtn.SetActive(isBtnShown);
        AiVsAiBtn.SetActive(isBtnShown);
    }


    public void TrainAI()
    {
        Debug.Log("<size=20>[DEBUG]  Start training!</size>");
    }

    public void StartRecording()
    {
        Debug.Log("<size=20>[DEBUG]  Recording....</size>");
    }
}
