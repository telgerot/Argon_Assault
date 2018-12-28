using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float levelTransitionTime = 3f;

    void Awake()
    {
        if (FindObjectsOfType<SceneLoader>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        RestartLevel();
    }

    public void RestartLevel() //Why the extra step?  Because CollisionHandler is calling this, and I can't figure out how to call IEnumerator's from outside yet lol
    {
        StartCoroutine(LoadFirstLevel());
    }

    IEnumerator LoadFirstLevel()
    {
        yield return new WaitForSeconds(levelTransitionTime);
        SceneManager.LoadScene(1);
    }
}


