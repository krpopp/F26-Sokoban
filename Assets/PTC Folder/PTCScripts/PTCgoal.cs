using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Vector3 = UnityEngine.Vector3;

public class PTCgoal : MonoBehaviour
{
    [Header("Stuff for Goal")]
    [SerializeField] GameObject requiredBlock;
    [SerializeField] Vector2Int gridPos;
    
    [Header("Win Screen")]
    [SerializeField] GameObject winScreen;
    [SerializeField] Player player;
    
    [Header("Audio")]
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioSource audioSource;
    
    // [Header("Goal Detector")]
    // [SerializeField] float stopDelay = 0.2f;
    
    private Block blockScript;
    private bool hasWon = false;

    // Vector3 lastPosition;
    // Vector2Int lastGridPos;
    // float stoppedTimer = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winScreen.SetActive(false);
        
        blockScript = requiredBlock.GetComponent<Slidey>();
        
        // lastPosition = requiredBlock.transform.position;
        // lastGridPos = blockScript.gridPos;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (hasWon) return;
        
        
        Vector3 currentPosition = requiredBlock.transform.position;
        
        // bool isMoving = Vector3.Distance(currentPosition, lastPosition) > 0.001f || Vector3.Distance(currentPosition, blockScript.targetPos) > 0.01f;

        if (blockScript.State != Block.MoveStates.idle) return;

        // if (isMoving)
        // {
        //     stoppedTimer = 0f;
        // }
        // else
        // {
        //     stoppedTimer += Time.deltaTime;
        // }
        //
        // lastPosition = currentPosition;
        // lastGridPos = blockScript.gridPos;

        // if (stoppedTimer < stopDelay) return;
        
        if (blockScript.gridPos == gridPos)
        {
            Cell cell = requiredBlock.transform.parent.GetComponent<Cell>();

            if (cell != null && cell.ContainObj == requiredBlock)
            {
                Win();
            }
        }
    }

    void Win()
    {
        hasWon = true;

        Debug.Log("omg they won");

        player.enabled = false;
        
        audioSource.PlayOneShot(winSound);
        
        winScreen.SetActive(true);
    }

    public void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        SceneManager.LoadScene(nextScene);
    }

    public void RestartLevel()
    {
        Debug.Log("Restart Button Clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
