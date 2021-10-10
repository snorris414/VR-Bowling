using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WizardStaff : MonoBehaviour
{
    private float speed = 8;
    public GameObject bowlingBall;
    public Transform launchPoint;
    private GameObject spawnedBowlingBall;
    public void SpawnObject()
    {
        spawnedBowlingBall = Instantiate(bowlingBall, launchPoint.position, launchPoint.rotation);
        //Vector3 staffPosition = gameObject.transform.position;
        //Rigidbody rigidbody = obj.GetComponent<Rigidbody>();
        //rigidbody.AddForce(staffPosition * 5, ForceMode.Impulse);
    }

    public void ShootObject()
    {
        spawnedBowlingBall.GetComponent<Rigidbody>().velocity = speed * spawnedBowlingBall.transform.up;
        spawnedBowlingBall = null;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedBowlingBall != null)
        {
            spawnedBowlingBall.transform.position = launchPoint.transform.position;
        }
    }
}
