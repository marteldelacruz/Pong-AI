using UnityEngine;
using TMPro;

public class TextButton : MonoBehaviour
{
    public Color HighlightedColor;

    private TextMeshProUGUI text;
    private Color oldColor;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        oldColor = text.color;
    }

    /// <summary>
    ///     Changes text color to given color
    /// </summary>
    public void Highlight()
    {
        text.color = HighlightedColor;
    }

    /// <summary>
    ///     Resets color to normal
    /// </summary>
    public void Unhighlight()
    {
        text.color = oldColor;
    }
}
