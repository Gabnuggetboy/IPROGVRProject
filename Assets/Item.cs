using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Item : MonoBehaviour
{
    public ItemObject item;

    private void Awake()
    {
        var grabInteractable = gameObject.GetComponent<XRGrabInteractable>();
        if (!grabInteractable)
        {
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();
            grabInteractable.interactionLayers = InteractionLayerMask.GetMask("Default");
        }
    }
}