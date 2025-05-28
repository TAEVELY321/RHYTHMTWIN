using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static float MOVE_SPEED = 5.0f;
    public static float RISE_SPEED = 5.0f;
    public static float FALL_SPEED = 5.0f;
    public static float MAX_Y = 1.0f;
    public static float MIN_Y = 0.0f;

    public enum STEP
    {
        NONE = -1,
        RUN = 0,
        JUMP,
        AIR,
        NUM,
    };

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.NONE;
    public float step_timer = 0.0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        next_step = STEP.RUN;
    }

    void Update()
    {
        step_timer += Time.deltaTime;

        Vector3 velocity = rb.velocity;

        // 항상 오른쪽 이동 유지
        velocity.x = MOVE_SPEED;

        // 상태 전이 판정
        if (next_step == STEP.NONE)
        {
            switch (step)
            {
                case STEP.RUN:
                    if (!IsGrounded())
                    {
                        next_step = STEP.AIR;
                    }
                    else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
                    {
                        next_step = STEP.JUMP;
                    }
                    break;
            }
        }

        // 상태 전이 처리
        while (next_step != STEP.NONE)
        {
            step = next_step;
            next_step = STEP.NONE;
            step_timer = 0.0f;
        }

        // 상태별 처리
        switch (step)
        {
            case STEP.RUN:
                if (transform.position.y > MIN_Y + 0.01f)
                {
                    velocity.y = -FALL_SPEED;
                }
                else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.F))
                {
                    next_step = STEP.JUMP;
                }
                break;

            case STEP.JUMP:
                if (transform.position.y < MAX_Y)
                {
                    velocity.y = RISE_SPEED;
                }
                else
                {
                    next_step = STEP.AIR;
                }
                break;

            case STEP.AIR:
                if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.F))
                {
                    if (transform.position.y < MAX_Y)
                        velocity.y = RISE_SPEED;
                }
                else if (Input.GetKey(KeyCode.J) || Input.GetKey(KeyCode.K))
                {
                    velocity.y = -FALL_SPEED * 2f;
                }
                else
                {
                    velocity.y = -FALL_SPEED;
                }

                // 바닥 도달 시 RUN으로
                if (transform.position.y <= MIN_Y + 0.01f)
                {
                    next_step = STEP.RUN;
                }
                break;
        }

        rb.velocity = velocity;
    }

    private bool IsGrounded()
    {
        return transform.position.y <= MIN_Y + 0.01f;
    }
}
