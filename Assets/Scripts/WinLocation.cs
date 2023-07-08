using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLocation : MonoBehaviour
{
    public string nextScene;

    public void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }
}