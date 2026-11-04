using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Slidey : Block
{
    [Header("Win Condition")]
    public Vector2Int goalPos = new Vector2Int(0, 5);
    public GameObject winText;//hide until win satisfied
    public AudioSource winSound;
    public float nextLevelDelay = 2f;//after 2 sec jump to next scene

    private bool won = false;

    protected override void FinishMove()
    {
        base.FinishMove();
        //stop sliding if win
        if (!won && gridPos == goalPos)
        {
            won = true;
            if (winText != null) winText.SetActive(true);
            if (winSound != null) winSound.Play();
            Debug.Log("You win!");
            StartCoroutine(LoadNextLevel());
            return;
        }

        Slide(moveChange);
    }
    
    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(nextLevelDelay);

        int next = SceneManager.GetActiveScene().buildIndex + 1;
        if (next < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(next);
    }

    protected virtual void Slide(Vector2 _delta)
    {
        CheckMove(_delta);
    }
}