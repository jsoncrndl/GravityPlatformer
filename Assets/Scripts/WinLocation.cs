using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLocation : MonoBehaviour
{
    public string nextScene;
    private bool hasWon;

    private AudioSource winSound;

    private void Start()
    {
        winSound = GetComponent<AudioSource>();
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void SetHasWon()
    {
        if (!hasWon)
        {
            winSound.Play();
        }
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