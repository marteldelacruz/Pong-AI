using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{
    [Tooltip("Game menu UI")]
    public GameObject MenuUI;

    // Start is called before the first frame update
    void Start()
    {
        MenuUI.SetActive(false);
        ShowMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowMenu();
        }
    }

    private void ShowMenu()
    {
        if (MenuUI.activeSelf)
            Time.timeScale = 1;
        else
            Time.timeScale = 0;

        MenuUI.SetActive(!MenuUI.activeSelf);
    }
}
