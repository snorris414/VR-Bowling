using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ValemHandsAnimator : AbstractHandAnimator
{
    public GameObject handModelPrefab;
    private Animator handAnimator;

    public ValemHandsAnimator(InputDevice targetDevice) : base(targetDevice) { }

    public override GameObject getPrefab()
    {
        return handModelPrefab;
    }

    public override void SetSpawnedGameObject(GameObject spawnedGameObject)
    {
        base.SetSpawnedGameObject(spawnedGameObject);
        handAnimator = spawnedGameObject.GetComponent<Animator>();
    }

    public override void UpdateHandAnimation()
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
