using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DeviceControllerHandAnimator : AbstractHandAnimator
{
    public List<GameObject> controllerPrefabs;

    public DeviceControllerHandAnimator(InputDevice targetDevice) : base(targetDevice) { }
    public override GameObject getPrefab()
    {
        GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
        if (prefab == null)
        {
            prefab = controllerPrefabs[0];
        }
        return prefab;
    }

    public override void UpdateHandAnimation()
    {
        // noop
    }
}
