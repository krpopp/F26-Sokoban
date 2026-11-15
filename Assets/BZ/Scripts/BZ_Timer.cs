using UnityEngine;
using TMPro;
public class BZ_Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] AudioSource lose;
    bool hasPlayed = false;

    float timer = 0f;
    public float waitTime = 3f;
    bool hasLost = false;
    public BZ_RoomManager roomManager;
    public BZ_Goal goal;
    
    void Start()
    {
        lose = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0 && hasPlayed == false)
        {
            remainingTime = 0;
            //LOSE
            GameOver();
            timerText.color = Color.red;
        }
        int minutes =Mathf.FloorToInt(remainingTime / 60);
        int seconds =Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        
        if (hasLost)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime)
            {
                timer = 0f;
                roomManager.Restart();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            roomManager.Restart();
            goal.hasWon = false;
        }

        if (goal.hasWon)
        {
            Debug.Log("YOOO");
            Destroy(gameObject);
        }
    }
    
    void GameOver()
    {
        if (!hasPlayed)
        {
            lose.Play();
            hasPlayed = true;
            hasLost = true;
        }
    }
}
