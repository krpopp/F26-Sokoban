using UnityEngine;

public class Ladder : Block
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Trapdoor trapdoor;

    public SceneManager sceneManager;
    public override bool CheckMove(Vector2 _delta)
    {
        if (!trapdoor.IsCoveredByRug())
        {
            if (trapdoor.checkInteractWithLadder())
            {
                sceneManager.goToNextScene();
            }
        }

        return base.CheckMove(_delta);
    }
}
