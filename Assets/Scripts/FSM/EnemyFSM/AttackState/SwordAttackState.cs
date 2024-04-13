using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyFSM
{
    public class SwordAttackState : EnemyPatternState
    {
        public SwordAttackState(EnemyBehaviorStateMachine sm) : base(sm)
        {
            stoppingDistance = 1f;
        }

        public override void Enter()
        {
            dir = GetLookAtAngle();
            sm.enemy.navMesh.isStopped = true;
            sm.enemy.animator.SetTrigger("Attack");
        }
        public override void Update()
        {
            sm.enemy.transform.rotation = Quaternion.Slerp(sm.enemy.transform.rotation, dir, Time.deltaTime * 10);

            if (sm.enemy.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f && sm.enemy.animator.IsInTransition(0) == false)
            {
                //GetBossPattern();
                sm.ChangeState(sm.backFlipState);
                //sm.ChangeState(sm.idleState);
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
            sm.enemy.navMesh.isStopped = false;
        }
        public override void OnAnimationEnterEvent()
        {

        }
        public override void OnAnimationExitEvent()
        {

        }
        public override void OnAnimationTransitionEvent()
        {
            if(canAttack == false)
            {
                canAttack = true;
                sm.enemy.swordAttack.SetCanAttack(canAttack);
            }
            else
            {
                canAttack = false;
                sm.enemy.swordAttack.SetCanAttack(canAttack);
            }
        }
        public override void OnAnimatorIK()
        {

        }
        private void GetBossPattern()
        {
            int pattern = Random.Range(0, 2);
            switch (pattern)
            {
                case 0:
                    sm.ChangeState(sm.idleState);
                    break;
                case 1:
                    sm.ChangeState(sm.jumpAttackState);
                    break;
                default:
                    break;
            }
        }
        
    }
}