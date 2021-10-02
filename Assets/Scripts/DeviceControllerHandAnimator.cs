using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeviceControllerHandAnimator : AbstractHandAnimator
{
    public DeviceControllerHandAnimator(GameObject spawnedGameObject) : base(spawnedGameObject)
    {
    }

    public override void UpdateHandAnimation(InputDevice targetDevice)
    {
        // noop
    }
}
