using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;



public class PlayerController : MonoBehaviour {
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float Speed = 4f;
#pragma warning disable IDE0044 // Add readonly modifier
    [Tooltip("In ms^-1")] [SerializeField] float xRange = 5f;
#pragma warning restore IDE0044 // Add readonly modifier
    [Tooltip("In ms^-1")] [SerializeField] float yRange = 3f;

    [SerializeField] GameObject[] guns;

    [Header("Screen Position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float controlPitchFactor = -20f;

    [Header("Control-throw based")]
    [SerializeField] float positionYawFactor = 5f;
    [SerializeField] float controlRollFactor = -20f;
    float xThrow, yThrow;
    bool isControlEnabled = true;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame

    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }
 
    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll  = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessTranslation()
    {
         xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * Speed * Time.deltaTime;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * Speed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            ActivateGuns();
        }
        else
        {
            DisActivateGuns();
        }
    }

    private void ActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    private void DisActivateGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }   

    void OnPlayerDeath()
    {
        isControlEnabled = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("We hit something!");
    }


}
