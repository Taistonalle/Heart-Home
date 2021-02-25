using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SilkieStates {
    IDLE,
    RUN,
    JUMP,
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
    }
    public void StateChange(SilkieStates state) {
        if(silkieState == state) {
            return;
        }
        silkieState = state;
    }


}
