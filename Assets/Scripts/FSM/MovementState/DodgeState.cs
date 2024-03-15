using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DodgeState : PlayerMovementState
{
    private Vector3 target;
    private bool isDodgeFinished;
    public DodgeState(PlayerMovementStateMachine sm) : base(sm)
    {

    }
    public override void Enter()
    {
        target = new Vector3(0.18f, 1.57f, 10.11f);

        isDodgeFinished = false;

        sm.character.animator.SetTrigger("IsRolling");
        sm.character.rb.velocity = Vector3.zero;

        SetMoveDirection();
        Dodge();
    }
    public override void Update()
    {
        sm.character.rb.useGravity = !IsOnSlope();

        if (isDodgeFinished == true)
        {
            if(CameraStateMachine.Instance.currentState == CameraStateMachine.Instance.cameraLockOnState)
            {
                sm.ChangeState(sm.lockOnWalkState);

            }
            else
            {
                sm.ChangeState(sm.walkState);
            }
        }
    }
    public override void PhysicsUpdate()
    {

    }
    public override void LateUpdate()
    {

    }
    public override void Exit()
    {
    }
    public override void OnAnimationEnterEvent()
    {

    }
    public override void OnAnimationExitEvent()
    {
        isDodgeFinished = true;
    }
    public override void OnAnimationTransitionEvent()
    {

    }
    private void Dodge()
    {
        sm.character.transform.LookAt(sm.character.transform.position + moveDirection);

        if (IsOnSlope() == true)
        {
            sm.character.rb.AddForce(GetSlopeMoveDirection() * 10f, ForceMode.Impulse);
            if (sm.character.rb.velocity.y > 5)
            {
                sm.character.rb.AddForce(Vector3.down * 80f, ForceMode.Force);
            }
        }
        else
        {
            sm.character.rb.AddForce(sm.character.transform.forward * 400f, ForceMode.Force);
        }
    }
}