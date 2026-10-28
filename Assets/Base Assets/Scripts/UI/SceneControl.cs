using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    [SerializeField] private bool scaleIn;
    
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
    }
    
    public void AnimationTransition(string animName)
    {
        sceneAnimation.SetBool(animName, true);
    }
    
    public void ButtonToScene(string toLoad)
    {
        if(sceneReady) SceneManager.LoadScene(toLoad);
    }
}
