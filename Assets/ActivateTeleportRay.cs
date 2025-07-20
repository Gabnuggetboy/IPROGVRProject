
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportRay : MonoBehaviour
{
    public GameObject rightTeleportation;
    public GameObject leftTeleportation;
    public XRRayInteractor leftRay;
    public XRRayInteractor RightRay;
    public InputActionProperty rightActivate;
    public InputActionProperty leftActivate;
    public InputActionProperty rightcancel;
    public InputActionProperty leftcancel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool islefthovering = leftRay.TryGetHitInfo(out Vector3 leftpos, out Vector3 leftnormal, out int leftNumber, out bool leftvalid);
        bool isrighthovering = leftRay.TryGetHitInfo(out Vector3 rightpos, out Vector3 rightnormal, out int rightNumber, out bool rightvalid);
        rightTeleportation.SetActive(!isrighthovering && rightcancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
        leftTeleportation.SetActive(!islefthovering && leftcancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
    }
}
