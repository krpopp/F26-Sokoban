using UnityEngine;

public class PTCswitchManager : MonoBehaviour
{
    [Header("Stuff for Switch")]
    [SerializeField] Player player;
    [SerializeField] Vector2Int switchGridPos;
    
    [Header("Stuff for Snow")]
    [SerializeField] PTCsnow[] snowBlocks;
    
    [Header("Visuals")]
    [SerializeField] Color onColor;
    [SerializeField] Color offColor;
    [SerializeField] Renderer switchRenderer;
    
    private bool isOn = false;
    private bool wasOnSwitch = false;

    void Start()
    {
        switchRenderer.material.color = offColor;
    }

    // Update is called once per frame
    void Update()
    {
        bool onSwitch = player.State == Block.MoveStates.idle && player.gridPos == switchGridPos;

        if (onSwitch && !wasOnSwitch)
        {
            ToggleSwitch();
        }
        
        wasOnSwitch = onSwitch;
    }

    private void ToggleSwitch()
    {
        if (isOn)
        {
            foreach (PTCsnow snow in snowBlocks)
            {
                if (!snow.CanFreeze()) return; 
            }

            foreach (PTCsnow snow in snowBlocks)
            {
                snow.Freeze();
            }
        }
        else
        {
            foreach (PTCsnow snow in snowBlocks)
            {
                snow.Melt();
            }
        }
        isOn = !isOn;


        if (isOn == true)
        {
            switchRenderer.material.color = onColor;
        }
        else
        {
            switchRenderer.material.color = offColor;
        }
    }
}
