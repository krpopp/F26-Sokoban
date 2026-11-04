using UnityEngine;

public class MusicManage : MonoBehaviour
{
    //stores music manager that already exists in game
    private static MusicManage instance;

    private void Awake()
    {
        //checks if another already exists
        if (instance != null && instance != this)
        {
            //destroys duplicate musicmanager so songs dont overlap
            Destroy(gameObject);
            return;
        }
        //saves the musicmanager as main
        instance = this;
        //keeps maanger alive when switching rooms
        DontDestroyOnLoad(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
