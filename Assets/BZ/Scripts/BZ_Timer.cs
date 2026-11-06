using UnityEngine;
using TMPro;
public class BZ_Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] AudioSource lose;
    bool hasPlayed = false;

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
    }
    
    void GameOver()
    {
        lose.Play();
        hasPlayed = true;
    }
}
