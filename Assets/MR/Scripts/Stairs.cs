using UnityEngine;
using UnityEngine.SceneManagement;

public class Stairs : Block
{
    [Header("Stairs")]
    public string nextScene;
    private Vector2Int facingDir;
    private bool startedNextLvlSequence;

    private void Awake()
    {
        facingDir = new Vector2Int(Mathf.RoundToInt(transform.forward.x), Mathf.RoundToInt(transform.forward.z));
    }
    private void Update()
    {
        if (GameManager.Player.gridPos == gridPos + facingDir && !startedNextLvlSequence)
            NextLvl();
    }

    protected virtual void NextLvl()
    {
        SceneManager.LoadScene(nextScene);
        startedNextLvlSequence = true;
    }
}
