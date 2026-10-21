using UnityEngine;

public class Cell : MonoBehaviour
{

    //this script tracks cell data
    
    private GameObject containObj = null;
    public GameObject ContainObj
    {
        get => containObj;
        set
        {
            containObj = value;
        }
    }

    /// <summary>
    /// Checks if this cell has a block type child object
    /// </summary>
    public bool CheckContainObj()
    {
        if (ContainObj == null) return false;
        return true;
    }

    public void RemoveContainObj()
    {
        ContainObj = null;
    }

}
