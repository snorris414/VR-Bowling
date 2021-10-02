using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CustomHandModelAnimator : AbstractHandAnimator
{
    private float thumbValue;
    private readonly float thumbMoveSpeed = 0.1f;

    private Animator animator;

    public CustomHandModelAnimator(GameObject spawnedGameObject, Animator animator) : base(spawnedGameObject)
    {
        this.animator = animator;
    }

    public override void UpdateHandAnimation(InputDevice targetDevice)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float indexValue))
        {
            animator.SetFloat("Index", indexValue);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float threeFingersValue))
        {
            animator.SetFloat("Three Fingers", threeFingersValue);
        }

        bool primaryTouchRetrieved = targetDevice.TryGetFeatureValue(CommonUsages.primaryTouch, out bool primaryTouched);
        bool secondaryTouchRetrieved = targetDevice.TryGetFeatureValue(CommonUsages.secondaryTouch, out bool secondaryTouched);
        bool joystickTouchRetrieved = targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out bool joystickTouched);
        if (primaryTouchRetrieved || secondaryTouchRetrieved || joystickTouchRetrieved)
        {
            if (primaryTouched || secondaryTouched || joystickTouched)
            {
                thumbValue += thumbMoveSpeed;
            }
            else
            {
                thumbValue -= thumbMoveSpeed;
            }

            thumbValue = Mathf.Clamp(thumbValue, 0, 1);


            animator.SetFloat("Thumb", thumbValue);
        }
    }

    public override void BeforeGameObjectActive(HandPresence handPresence)
    {
        handPresence.DestroyCustomHandModel(spawnedGameObject);
        spawnedGameObject = handPresence.SpawnCustomHandModel();
        this.animator = spawnedGameObject.GetComponent<Animator>();
        spawnedGameObject.SetActive(false);
    }
}
