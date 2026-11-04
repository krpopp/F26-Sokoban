using UnityEngine;
using System.Collections;

public class CSPSnowball : Block
{
    [SerializeField] private Vector3 stackOffset = new Vector3(0f, 1f, 0f);
    [SerializeField] private float snapDist = 0.01f;
    private bool hasFirstBall = false;
    private bool switchToEnd = false;
    public GameObject victoryPanel;
    public AudioSource victorySound;
    public AudioSource backgroundMusic;
    public AudioSource backgroundSound;
    public AudioSource rollSound;

    private CSPSnowmanBase stackTarget;
    public bool isStacked { get; private set; }

    public override bool CheckMove(Vector2 _delta)
    {
        if (isStacked) return false;
        return base.CheckMove(_delta);
    }

    protected override bool CheckHit(Block _hitObj, Vector2 _delta)
    {
        stackTarget = null;

        if (_hitObj is CSPSnowmanBase snowmanBase)
        {
            stackTarget = snowmanBase;
            if(_hitObj.GetComponentInChildren<CSPSnowball>()) hasFirstBall = true;
            return true;
        }
        return base.CheckHit(_hitObj, _delta);
    }

    protected override void StartMove(Cell _newParent, Vector2 _delta)
    {
        CSPSnowmanBase snowmanBase = stackTarget;
        stackTarget = null;

        rollSound.Play();

        if (snowmanBase == null)
        {
            base.StartMove(_newParent, _delta);
            return;
        }
        
        base.StartMove(_newParent, _delta);
        StopAllCoroutines();

        
        _newParent.ContainObj = snowmanBase.gameObject;
        gridManager.UpdateGrid();

        transform.SetParent(snowmanBase.transform, true);
        gridPos = snowmanBase.gridPos;

        isStacked = true;
        canMove = false;

        targetPos = snowmanBase.transform.position + stackOffset;
        if(hasFirstBall)
        {
            targetPos = snowmanBase.transform.position + 2 * stackOffset;
            switchToEnd = true;
        }else victorySound.Play();
        State = MoveStates.moving;
        StartCoroutine(StackLoop());
    }

    private IEnumerator StackLoop()
    {
        while (Vector3.Distance(transform.position, targetPos) > snapDist)
        {
            transform.position = Move();
            yield return null;
        }
        FinishMove();
        if (switchToEnd)
        {
            Time.timeScale = 0;
            victoryPanel.SetActive(true);
            backgroundMusic.Stop();
            backgroundSound.Stop();
            victorySound.Play();
        }
        
    }

    
}
