using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Block
{

    [SerializeField]
    protected Animator myAnim;

    private Vector2 inputVector;
    private InputAction inputMove;

    private void Start()
    {
        inputMove = InputSystem.actions.FindAction("Move");
    }
    
    private void Update()
    {
        if (State == MoveStates.idle) MoveInput();
    }

    protected virtual void MoveInput()
    {

        inputVector = inputMove.ReadValue<Vector2>();
        if (inputVector != Vector2.zero) TurnAndMove();
    }

    private void TurnAndMove()
    {
        if (inputVector == Vector2.up)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.back);
            Debug.Log("up");
        } 
        if (inputVector == Vector2.down)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.forward);
            Debug.Log("down");
        } 
        if (inputVector == Vector2.left)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.left);
            Debug.Log("left");
        } 
        if (inputVector == Vector2.right)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.right);
            Debug.Log("right");
        }
    }
    
    

    protected override void StartMove(Cell newParent, Vector2 _delta)
    {
        myAnim.SetBool("isMoving", true);
        base.StartMove(newParent, _delta);
    }

    protected override void FinishMove()
    {
        myAnim.SetBool("isMoving", false);
        base.FinishMove();
    }

}
