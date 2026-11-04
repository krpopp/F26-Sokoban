using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Block
{

    [SerializeField]
    private Animator myAnim;

    private Vector2 inputVector;
    private InputAction inputMove;

    protected override void Start()
    {
        base.Start();
        inputMove = InputSystem.actions.FindAction("Move");
    }
    
    private void Update()
    {
        if (State == MoveStates.idle) MoveInput();
    }

    private void MoveInput()
    {

        inputVector = inputMove.ReadValue<Vector2>();
        if (inputVector != Vector2.zero) TurnAndMove();
    }

    private void TurnAndMove()
    {
        if (inputVector == Vector2.up)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.forward);
        } 
        if (inputVector == Vector2.down)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.back);
        } 
        if (inputVector == Vector2.left)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.left);
        } 
        if (inputVector == Vector2.right)
        {
            if (CheckMove(inputVector)) transform.rotation = Quaternion.LookRotation(Vector3.right);
        }
    }

    protected override void StartMove(Cell newParent, Vector2 _delta)
    {
        myAnim.SetBool("isMoving", true);
        base.StartMove(newParent, _delta);
    }
    
}
