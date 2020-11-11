using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject PlayerVsPlayerBtn;
    public GameObject PlayerVsAiBtn;
    public GameObject AiVsAiBtn;

    public RegionCollision Region1;
    public RegionCollision Region2;
    public RegionCollision Region3;
    public RegionCollision Region4;
    public RegionCollision Region5;

    private bool IsRecordingDataset = false;

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
        IsRecordingDataset = !IsRecordingDataset;
        // Set flag for all regions
        Region1.IsRecording = Region2.IsRecording = Region3.IsRecording = 
            Region4.IsRecording = Region5.IsRecording = IsRecordingDataset;

        /*TODO*/
    }
}
