using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public abstract class AbstractHandAnimator
{
    protected InputDevice targetDevice;
    protected GameObject spawnedGameObject;

    public AbstractHandAnimator(InputDevice targetDevice)
    {
        this.targetDevice = targetDevice;
    }

    public virtual void SetSpawnedGameObject(GameObject spawnedGameObject)
    {
        this.spawnedGameObject = spawnedGameObject;
    }

    public GameObject GetSpawnedGameObject()
    {
        return spawnedGameObject;
    }

    public abstract GameObject getPrefab();

    public abstract void UpdateHandAnimation();
    //public abstract void ActivateGameObject();
    //public abstract void DeactivateGameObject();
}
