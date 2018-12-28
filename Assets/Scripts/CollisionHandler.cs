using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        print("Player hit something!");
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        explosion.SetActive(true);
        SendMessage("OnPlayerDeath"); //this freezes movement via PlayerController
        FindObjectOfType<SceneLoader>().RestartLevel();
    }
}
