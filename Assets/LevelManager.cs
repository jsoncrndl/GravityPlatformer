using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string scene;
    public static LevelManager singleton;

    private void Awake()
    {
        scene = gameObject.scene.name;

        if (singleton == null)
        {
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
        else if (gameObject.scene.name != singleton.scene)
        {
            Debug.Log(gameObject.scene.name);
            Debug.Log(singleton.scene);

            Destroy(singleton.gameObject);
            DontDestroyOnLoad(gameObject);
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += Unfade;
    }

    private void Unfade(Scene scene, LoadSceneMode loadSceneMode)
    {
        Camera.main.GetComponent<CameraFunctions>().FadeIn();
    }

    public void ResetLevel()
    {
        StartCoroutine(ResetCoroutine(scene));
    }

    private IEnumerator ResetCoroutine(string scene)
    {
        Camera.main.GetComponent<CameraFunctions>().FadeOut();
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(scene);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= Unfade;
    }

    public void LoadLevel(string level)
    {
        StartCoroutine(ResetCoroutine(level));
    }
}
