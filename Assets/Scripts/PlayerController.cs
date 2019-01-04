using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float xSpeed = 10f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 10f;
    [Tooltip("In m")] [SerializeField] float xRange = 4f;
    [Tooltip("In m")] [SerializeField] float yRange = 3f;
    [SerializeField] GameObject[] guns;

    [Header("Screen Position-related Movement Config")]
    [SerializeField] float positionPitchFactor = -2.5f;
    [SerializeField] float positionYawFactor = 3.5f;
    [SerializeField] float positionRollFactor = 5f;

    [Header("Control Input-related Movement Config")]
    [SerializeField] float controlPitchFactor = -5f;
    [SerializeField] float controlRollFactor = -45f;
    float xThrow;
    float yThrow;
    bool controlsFrozen = false;

    void Update()
    {
        if (controlsFrozen == false)
        {
            CalculatePlayerLocation();
            CalculatePlayerRotation();
            ProcessFiring();
        }
    }

    void CalculatePlayerLocation()
    {
        CalculateXPosition();
        CalculateYPosition();
    }
    void CalculateXPosition()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal"); //Gets Horizontal input from player
        float xOffset = xThrow * xSpeed * Time.deltaTime; //figures out the speed of change per frame
        float rawXPos = transform.localPosition.x + xOffset; // figures out where the player should be after applying the offset
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); // makes sure the player doesn't fly off the screen

        transform.localPosition = new Vector3(clampedXPos, transform.localPosition.y, transform.localPosition.z); //moves the player
    }

    void CalculateYPosition()
    {
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime;
        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(transform.localPosition.x, clampedYPos, transform.localPosition.z);
    }

    void CalculatePlayerRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void OnPlayerDeath()
    {
        print("Movement stopped"); //This is a string-referenced message from CollisionHandler when player hits something
        controlsFrozen = true;
        FindObjectOfType<ScoreBoard>().ResetScore();
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire1"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {         
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
