using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject PlayerVsPlayerBtn;
    public GameObject PlayerVsAiBtn;
    public GameObject AiVsAiBtn;

    public RegionCollision MainRegion;

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
        MainRegion.BeginTraining();
    }

    public void StartRecording()
    {
        IsRecordingDataset = !IsRecordingDataset;

        if (IsRecordingDataset)
            Debug.Log("<size=20>[DEBUG]  Recording....</size>");
        else
            Debug.Log("<size=20>[DEBUG]  Stopped recording</size>");

        // Set flag for all regions
        MainRegion.IsRecording = IsRecordingDataset;
    }

    public void ReadDatasetFromFile()
    {
        MainRegion.ReadDatasetFromFile();
    }
}
