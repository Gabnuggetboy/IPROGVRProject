using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FridgeDoorTrigger : MonoBehaviour
{
    public Animator anim;
    private bool isOpen = false;

    private void Start()
    {
        XRSimpleInteractable interactable = GetComponent<XRSimpleInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelect);
        }
        else
        {
            Debug.LogError("XRSimpleInteractable component not found on " + gameObject.name);
        }
    }

    public void OnSelect(SelectEnterEventArgs args)
    {
        ActivateDoor();
    }

    public void ActivateDoor()
    {
        isOpen = !isOpen;
        anim.SetBool("Open", isOpen);
    }
}