using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Tooltip("Player score text")]
    public TextMeshProUGUI PlayerText;

    int Score;

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
    }

    /// <summary>
    ///     Updates the score text shown on the top of the screen.
    ///     Each player has a text.
    /// </summary>
    public void UpdateScore()
    {
        Score++;

        if (PlayerText != null)
        {
            // current score text and new format    
            PlayerText.text = string.Format("{0:00}", Score);
        }
    }
}
