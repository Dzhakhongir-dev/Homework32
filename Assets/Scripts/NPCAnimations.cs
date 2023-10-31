using UnityEngine;

public class NPCAnimations : MonoBehaviour
{
    private Animator animator;

    [HideInInspector]
    public string currentState;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        animator.Play(newState);
        currentState = newState;
    }
}
