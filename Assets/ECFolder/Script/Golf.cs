using UnityEngine;
using UnityEngine.SceneManagement; 

public class Golf : Slidey
{
    AudioSource audioSource;
    bool finish = false;
    [SerializeField] int goalX;
    [SerializeField] int goalY;
    [SerializeField] Golf Othergolf1;
    
    
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
        //Debug.Log("X: " + transform.position.x + ", Y: " + transform.position.z);
    }

    private void checkGoal()
    {
        if (transform.position.x == goalX && transform.position.z == goalY)
        {

            Debug.Log("You got a goal!");
            Debug.Log("Playing goal audio: " + gameObject.name);
            audioSource.Play();
            finish = true;

        }
    }

    private void checkFinish()
    {
        if (!finish)
                return;

            // Check if all Golf objects have reached their goals
            if (Othergolf1 != null && !Othergolf1.getFinish())
                return;
            
            // Check if any Golf object is still playing audio
            if (audioSource.isPlaying)
                return;

            if (Othergolf1 != null && Othergolf1.audioSource.isPlaying)
                return;
            
            Debug.Log("Changing Scene");

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);
    }


    protected override void Slide(Vector2 _delta)
    {
        CheckMove(_delta);
        Debug.Log("X: " + transform.position.x + ", Y: " + transform.position.z);
    }
    
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public bool getFinish()
    {
        return finish;
    }
    
}
