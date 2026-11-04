using UnityEngine;
using UnityEngine.SceneManagement; 

public class Golf : Slidey
{
    AudioSource audioSource;
    bool finish = false;
    [SerializeField] int goalX;
    [SerializeField] int goalY;
    
    
    private new void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
    }

    protected void Update()
    {
        checkFinish();
    }
    protected override void FinishMove()
    {
        base.FinishMove();
        Slide(moveChange);
        checkGoal();
        //Debug.Log("X: " + transform.position.x + ", Y: " + transform.position.y);
    }

    private void checkGoal()
    {
        if (transform.position.x == goalX && transform.position.y == goalY)
        {
            Debug.Log("You got a goal!");
            audioSource.Play();
            finish = true;
        }
    }

    private void checkFinish()
    {
        if (!audioSource.isPlaying && finish)
        {
            LoadSceneByName("ECScene 2");
        }
    }
    
    protected override void Slide(Vector2 _delta)
    {
        CheckMove(_delta);
    }
    
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
