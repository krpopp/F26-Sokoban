using UnityEngine;
using UnityEngine.SceneManagement;

public class BZ_RoomManager : MonoBehaviour
{
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
    }
    // Update is called once per frame
    void Update()
    {
        /*var Goal = GetComponent<BZ_Goal>();
        if (Goal.points >= 3)
        {
            SceneManager.LoadScene("BZ_Main1");
        }*/
    }
    
    public void ChangeLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
}
