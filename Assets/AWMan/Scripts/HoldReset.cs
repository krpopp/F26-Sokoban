using UnityEngine;

public class HoldReset : MonoBehaviour
{
    public RoomReset roomReset;

    public float resetHoldTime = 3f;
    private float holdTime = 0f;
    bool hasReset = false;
    
    private Renderer[] characterRenderers; 
    private Color[] originalColors;
    
    //audio
    public AudioSource audioSource;
    public AudioClip resetSound;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //finding character visible body parts
        characterRenderers = GetComponentsInChildren<Renderer>(true);
        //save their original colors
        originalColors = new Color[characterRenderers.Length];
        
        for (int i = 0; i < characterRenderers.Length; i++)
        {
            originalColors[i] = characterRenderers[i].material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (hasReset) return;
            holdTime += Time.deltaTime;

            //calculate how red character will be
            float progress = Mathf.Clamp01(holdTime/resetHoldTime);

            //gradually turns character red
            for (int i = 0; i < characterRenderers.Length; i++)
            {
                characterRenderers[i].material.color = Color.Lerp(originalColors[i],Color.red, progress);
            }
            
            //reset after 3 seconds
            if (holdTime >= resetHoldTime)
            {
                roomReset.ResetRoom();
                //plays oof sound
                if (audioSource != null && resetSound != null)
                {
                    audioSource.PlayOneShot(resetSound);
                }
                
                hasReset = true;
          
            }
        }
        else
        {
            //reset timer when r released
            holdTime = 0f;
            hasReset = false;

            //restore original colors
            for (int i = 0; i < characterRenderers.Length; i++)
            {
                characterRenderers[i].material.color = originalColors[i];
            }
        }
    }
}
