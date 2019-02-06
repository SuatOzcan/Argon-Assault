using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour {

    [Tooltip("In seconds.")][SerializeField] float levelLoadDelay = 1f;
    [Tooltip("FX prefab is on player.")][SerializeField] GameObject deathFx;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFx.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {    
        print("Player is dead.");
        SendMessage("OnPlayerDeath");
    }
    private void ReloadScene() {
        SceneManager.LoadScene(1);
    }
}
