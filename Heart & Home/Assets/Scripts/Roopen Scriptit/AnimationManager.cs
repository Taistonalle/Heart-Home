using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SilkieStates {
    IDLE,
    RUN,
    JUMP,
    FALLING,
    ATTACK,
}
public class AnimationManager : MonoBehaviour   
{
    SilkieStates silkieState;
    Animator animator;

    private void Start() {
        silkieState = SilkieStates.IDLE;
        animator = gameObject.GetComponent<Animator>();
    }
    void FixedUpdate() {
        if (silkieState == SilkieStates.IDLE) {
            animator.Play("Silkie_Idle");
        }
        if (silkieState == SilkieStates.RUN) {
            animator.Play("Silkie_Run");
        }
        if (silkieState == SilkieStates.JUMP) {
            animator.Play("Silkie_Jump");
        }
        if(silkieState == SilkieStates.FALLING) {
            animator.Play("Silkie_Falling");
        }
        if(silkieState == SilkieStates.ATTACK) {
            animator.Play("Silkie_Attack");
        }
    }
    public void StateChange(SilkieStates state) {
        if(silkieState == state) {
            return;
        }
        silkieState = state;
    }


}
