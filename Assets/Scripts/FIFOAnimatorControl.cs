using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FIFOAnimatorControl : MonoBehaviour
{
    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("DF");
        }

        if(Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
        {
            animator.SetTrigger("JK");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.TryUseSkill();
        }
    }
}
