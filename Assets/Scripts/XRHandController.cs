using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public enum HandType
{
    Left,
    Right
}
public class XRHandController : MonoBehaviour
{
    public HandType handType;
    private float thumbMoveSpeed = 0.1f;

    private Animator animator;
    private InputDevice inputDevice;
    private float indexValue;
    private float threeFingersValue;
    private float thumbValue;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics controllerCharacteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        if (handType == HandType.Left)
        {
            controllerCharacteristics = controllerCharacteristics | InputDeviceCharacteristics.Left;
        }
        else
        {
            controllerCharacteristics = controllerCharacteristics | InputDeviceCharacteristics.Right;
        }

        List<InputDevice> allInputDevices = new List<InputDevice>();
        InputDevices.GetDevices(allInputDevices);
        Debug.Log(allInputDevices.Count);
        foreach(InputDevice inputDevice in allInputDevices)
        {
            Debug.Log(inputDevice.characteristics);
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, inputDevices);

        Debug.Log("Found Input Devices: " + inputDevices.Count);

        return inputDevices[0];
    }

    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out indexValue);
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out threeFingersValue);

        inputDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out bool primaryTouched);
        inputDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool secondaryTouched);
        inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool joystickTouched);

        if (primaryTouched || secondaryTouched || joystickTouched)
        {
            thumbValue += thumbMoveSpeed;
        }
        else
        {
            thumbValue -= thumbMoveSpeed;
        }

        thumbValue = Mathf.Clamp(thumbValue, 0, 1);

        animator.SetFloat("Index", indexValue);
        animator.SetFloat("Three Fingers", threeFingersValue);
        animator.SetFloat("Thumb", thumbValue);
    }
}
