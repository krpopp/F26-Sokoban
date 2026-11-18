using UnityEngine;

public class DisableOnEnable : MonoBehaviour
{
    public GameObject[] objs;

    private void OnEnable()
    {
        foreach(GameObject gameObj in objs)
            gameObj.SetActive(false);
    }
}
