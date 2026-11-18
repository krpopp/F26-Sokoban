using UnityEngine;

public class PTCsnow : Block
{
    [Header("Visuals")] 
    [SerializeField] GameObject snowModel;
    [SerializeField] GameObject meltedModel;
    
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip meltSound;
    [SerializeField] AudioClip freezeSound;
    
    private bool isMelted = false;

    protected override void Start()
    {
        base.Start();

        canMove = false;
        
        snowModel.SetActive(true);
        meltedModel.SetActive(false);
    }

    public void Melt()
    {
        if (isMelted) return;
        
        Cell cell = transform.parent.GetComponent<Cell>();

        if (cell == null || cell.ContainObj != gameObject) return; 
        
        isMelted = true;
        
       audioSource.PlayOneShot(meltSound); 
       
        cell.RemoveContainObj();
        snowModel.SetActive(false);
        meltedModel.SetActive(true);
        
        
        
        gridManager.UpdateGrid();
    }

    public bool CanFreeze()
    {
        if (!isMelted) return true;

        Cell cell = transform.parent.GetComponent<Cell>();

        return cell != null && !cell.CheckContainObj();
    }

    public void Freeze()
    {
        if (!isMelted || !CanFreeze()) return;
        
        Cell cell = transform.parent.GetComponent<Cell>();
        
        isMelted = false;

        cell.ContainObj = gameObject;
        
        audioSource.PlayOneShot(freezeSound);
        
        snowModel.SetActive(true);
        meltedModel.SetActive(false);
        
        gridManager.UpdateGrid();
        
    }
}
