using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristics;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    public GameObject customHandModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private GameObject spawnedCustomHandModel;
    private Animator handAnimator;
    List<AbstractHandAnimator> handAnimators = new List<AbstractHandAnimator>();
    private int handAnimatorsIndexPointer = 0;
    private bool primaryButtonDownPrevious = false;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
    }

    void UpdateHandAnimation()
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

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        //InputDevices.GetDevices(devices);
        //InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            if (spawnedController == null)
            {
                GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
                if (prefab)
                {
                    spawnedController = Instantiate(prefab, transform);
                }
                else
                {
                    Debug.Log("Did not find corresponding controller model");
                    spawnedController = Instantiate(controllerPrefabs[0], transform);
                }
                spawnedController.SetActive(true);
                DeviceControllerHandAnimator deviceControllerHandAnimator = new DeviceControllerHandAnimator(spawnedController);
                handAnimators.Add(deviceControllerHandAnimator);
            }
        }

        if (spawnedHandModel == null)
        {
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
            spawnedHandModel.SetActive(false);
            ValemHandsAnimator valemHandsAnimator = new ValemHandsAnimator(spawnedHandModel, handAnimator);
            handAnimators.Add(valemHandsAnimator);
        }

        if (spawnedCustomHandModel == null)
        {
            spawnedCustomHandModel = Instantiate(customHandModelPrefab, transform);
            Animator customHandAnimator = spawnedCustomHandModel.GetComponent<Animator>();
            spawnedCustomHandModel.SetActive(false);
            CustomHandModelAnimator customHandModelAnimator = new CustomHandModelAnimator(spawnedCustomHandModel, customHandAnimator);
            handAnimators.Add(customHandModelAnimator);
        }
    }

    public void DestroyCustomHandModel(GameObject gameObject)
    {
        Destroy(gameObject);
    }
    public GameObject SpawnCustomHandModel()
    {
        return Instantiate(customHandModelPrefab, transform);
    }

    private bool SwitchControllers()
    {
        targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton);
        if (primaryButton && !primaryButtonDownPrevious) 
        {
            primaryButtonDownPrevious = true;
            return true;
        }

        primaryButtonDownPrevious = primaryButton;
        return false;
    }

    private void LoadNextModel()
    {
        if (handAnimators.Count > 1)
        {
            AbstractHandAnimator previousHandAnimator = null;
            AbstractHandAnimator currentHandAnimator = null;
            if (handAnimatorsIndexPointer < handAnimators.Count - 1)
            {
                previousHandAnimator = handAnimators[handAnimatorsIndexPointer];
                handAnimatorsIndexPointer++;
                currentHandAnimator = handAnimators[handAnimatorsIndexPointer];
            }
            else if (handAnimatorsIndexPointer == handAnimators.Count - 1)
            {
                previousHandAnimator = handAnimators[handAnimatorsIndexPointer];
                handAnimatorsIndexPointer = 0;
                currentHandAnimator = handAnimators[handAnimatorsIndexPointer];
            }
            previousHandAnimator.GetSpawnedGameObject().SetActive(false);
            currentHandAnimator.BeforeGameObjectActive(this);
            currentHandAnimator.GetSpawnedGameObject().SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            if (SwitchControllers())
            {
                LoadNextModel();
            }
            handAnimators[handAnimatorsIndexPointer].UpdateHandAnimation(targetDevice);
        }
    }
}
