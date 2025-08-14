using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FridgeDoorInteractable : MonoBehaviour
{
    public Animator anim;
    private bool isOpen = false;
    private XRSimpleInteractable interactable;

    void Start()
    {
        // Get the XR Simple Interactable component
        interactable = GetComponent<XRSimpleInteractable>();
        
        // Add listener for the select event (trigger press)
        interactable.selectEntered.AddListener(OnSelect);
        
        // Ensure animator is assigned
        if (anim == null)
        {
            anim = GetComponent<Animator>();
        }
    }

    private void OnSelect(SelectEnterEventArgs args)
    {
        // Toggle door state
        isOpen = !isOpen;
        anim.SetBool("Open", isOpen);
    }

    // Optional: Clean up listeners when the object is destroyed
    void OnDestroy()
    {
        interactable.selectEntered.RemoveListener(OnSelect);
    }
}