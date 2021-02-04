using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveAudio : StateMachineBehaviour
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AudioSource RunningSound = animator.gameObject.GetComponent<PlayerMovement>().RunningSound;
        if (RunningSound != null && !RunningSound.isPlaying)
            RunningSound.Play();
    }

}
