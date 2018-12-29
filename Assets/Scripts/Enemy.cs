using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] Transform parent;
    [SerializeField] int scoreForKilling; 
    ScoreBoard scoreBoard;  


    private void Start()
    {
        AddNonTriggerBoxCollider();
        Transform parent = FindObjectOfType<SpawnedAtRuntime>().transform;
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider myBoxCollider = gameObject.AddComponent<BoxCollider>();
        myBoxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        print("Particles collided with enemy" + gameObject.name);
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        GameObject fx = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        scoreBoard.ScoreHit(scoreForKilling);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
