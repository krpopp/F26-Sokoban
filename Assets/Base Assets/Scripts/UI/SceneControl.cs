using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    [SerializeField] private bool scaleIn;
    [SerializeField] private string nextScene;
    [SerializeField] private Animator sceneAnimation;

    private bool sceneReady = true;
    
    void Start()
    {
        if(scaleIn)
        {
            sceneReady = false;
            sceneAnimation.SetBool("scale", true);
        }
    }

    public void CanInteract()
    {
        sceneReady = true;
        scaleIn = false;
    }
    
    public void AnimationTransition(string animName)
    {
        sceneAnimation.gameObject.GetComponent<SceneControl>().nextScene = nextScene;
        if(!sceneAnimation.GetBool(animName)) sceneAnimation.SetBool(animName, true);
    }
    
    public void ButtonToScene()
    {
        if(sceneReady) SceneManager.LoadScene(nextScene);
    }
}
