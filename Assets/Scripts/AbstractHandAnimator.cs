using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public abstract class AbstractHandAnimator
{
    protected InputDevice targetDevice;
    protected GameObject spawnedGameObject;

    public AbstractHandAnimator(GameObject spawnedGameObject)
    {
        this.spawnedGameObject = spawnedGameObject;
    }

    public GameObject GetSpawnedGameObject()
    {
        return spawnedGameObject;
    }
    public abstract void UpdateHandAnimation(InputDevice targetDevice);
    public virtual void BeforeGameObjectActive(HandPresence handPresence)
    {
        // noop
    }
    //public abstract void ActivateGameObject();
    //public abstract void DeactivateGameObject();
}
