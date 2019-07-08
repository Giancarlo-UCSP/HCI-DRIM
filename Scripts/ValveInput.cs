/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ValveInput : MonoBehaviour
{
    public SteamVR_TrackedObject _trackedObject = null;
    public SteamVR_Controller.Device _device;

    // Start is called before the first frame update
    void Awake()
    {
        _trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        _device = SteamVR_Controller.Input((int)_trackedObject.index);

        #region
        if (_device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger down");
        }

        if (_device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger up");
        }

        Vector2 triggerValue = _device.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger);
        #endregion

        #region
        if (_device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger down");
        }

        if (_device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            print("Trigger up");
        }

        Vector2 touchValue = _device.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad);
        #endregion
    }
}*/
