using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationE1 : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    Animator animator;
    Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    private void Update()
    {
        float verticalVelocity = agent.velocity.y;
        animator.SetFloat("yVelocity", verticalVelocity);
    }
}
