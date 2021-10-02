using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ValemHandsAnimator : AbstractHandAnimator
{
    public GameObject handModelPrefab;
    private Animator handAnimator;

    public ValemHandsAnimator(GameObject spawnedGameObject, Animator handAnimator) : base(spawnedGameObject)
    {
        this.handAnimator = handAnimator;
    }

    public override void UpdateHandAnimation(InputDevice targetDevice)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }
}
