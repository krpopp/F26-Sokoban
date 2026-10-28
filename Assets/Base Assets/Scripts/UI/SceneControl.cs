using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour
{

    [SerializeField] private Animator sceneAnimation;

    public void AnimationTransition(string animName)
    {
        sceneAnimation.SetBool(animName, true);
    }
    
    public void ButtonToScene(string toLoad)
    {
        SceneManager.LoadScene(toLoad);
    }
}
