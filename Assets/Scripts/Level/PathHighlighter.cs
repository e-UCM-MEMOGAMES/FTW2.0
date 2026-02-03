using UnityEngine;

public class PathHighlighter : MonoBehaviour
{
    [SerializeField]
    GameObject highlight;

    public void ActivateHighlight(bool activate)
    {
        highlight.SetActive(activate);
    }
}
