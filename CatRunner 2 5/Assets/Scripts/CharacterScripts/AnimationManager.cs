using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager
{
    private Animator animator;

    public AnimationManager(Animator animator)
    {
        this.animator = animator;
    }

    public void SetIdleAnim()
    {
        animator.SetInteger("State", (int)CharacterAnimationState.Idle);
    }

    public void SetFallingAnim()
    {
        animator.SetInteger("State", (int)CharacterAnimationState.Fall);
    }

    public void SetRunningAnim()
    {
		animator.SetInteger("State", (int)CharacterAnimationState.Run);
    }

    public void SetJumpingAnim()
    {
        animator.SetInteger("State", (int)CharacterAnimationState.Jump);
    }

    public void SetDyingAnim()
    {
        animator.enabled = false;
        animator.enabled = true;
        animator.Play("Dying");
    }
}