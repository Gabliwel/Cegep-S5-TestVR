using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

// https://www.youtube.com/watch?v=4tW7XpAiuDg&t=72s&ab_channel=Valem

public class LocomationCustomController : MonoBehaviour
{
    [SerializeField] private XRController teleportator;
    [SerializeField] private XRRayInteractor uiInteractor;
    [SerializeField] private InputHelpers.Button button;
    [SerializeField] private float buffer = 0.15f;

    private bool enableTeleport { get; set; } = true;

    void Update()
    {
        //https://docs.unity3d.com/Packages/com.unity.xr.interaction.toolkit@0.9/api/UnityEngine.XR.Interaction.Toolkit.XRRayInteractor.html#UnityEngine_XR_Interaction_Toolkit_XRRayInteractor_TryGetHitInfo_Vector3__Vector3__System_Int32__System_Boolean__
        Vector3 position = new Vector3();
        Vector3 normal = new Vector3();
        int index = 0;
        bool validTarget = false;

        if (teleportator)
        {
            bool isOverUi = uiInteractor.TryGetHitInfo(out position, out normal, out index, out validTarget);
            teleportator.gameObject.SetActive(enableTeleport && IsActivated() && !isOverUi);
        }
    }

    private bool IsActivated()
    {
        InputHelpers.IsPressed(teleportator.inputDevice, button, out bool isActive, buffer);
        return isActive;
    }
}
