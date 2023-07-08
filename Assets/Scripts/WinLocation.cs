using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLocation : MonoBehaviour
{
    public string nextScene;
    private bool hasWon;

    private void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void SetHasWon()
    {
        hasWon = true;
    }

    public void Win()
    {
        if (hasWon)
        {
            NextLevel();
        }
    }
}