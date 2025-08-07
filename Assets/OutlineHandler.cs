using UnityEngine;

[RequireComponent(typeof(Outline))]
public class OutlineHandler : MonoBehaviour
{
    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public void ShowOutline(Color color)
    {
        if (outline != null)
        {
            outline.OutlineColor = color;
            outline.enabled = true;
        }
    }

    public void HideOutline()
    {
        if (outline != null)
        {
            outline.enabled = false;
        }
    }
}