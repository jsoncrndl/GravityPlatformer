using UnityEngine;

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
        LevelManager.singleton.LoadLevel(nextScene);
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