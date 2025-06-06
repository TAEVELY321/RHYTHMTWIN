using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : CharacterBase
{
    public static float MOVE_SPEED = 15.0f;
    public static float MAX_Y = 1.0f;
    public static float MIN_Y = 0.0f;

    public enum STEP
    {
        NONE = -1,
        RUN = 0,
        AIR,
        NUM,
    };

    public STEP step = STEP.NONE;
    public STEP next_step = STEP.NONE;
    public float step_timer = 0.0f;

    private float airHoldTime = 0.5f; // ���� ���� �ð�
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        transform.position = new Vector3(transform.position.x, MIN_Y, transform.position.z);
        next_step = STEP.RUN;
    }

    void Update()
    {
        step_timer += Time.deltaTime;

        // �׻� ������ �̵�
        Vector3 velocity = rb.velocity;
        velocity.x = MOVE_SPEED;
        velocity.y = 0f;
        rb.velocity = velocity;

        // ���� ����
        if (next_step != STEP.NONE)
        {
            step = next_step;
            next_step = STEP.NONE;
            step_timer = 0.0f;
        }

        switch (step)
        {
            case STEP.RUN:
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.F))
                {
                    // ��� �� �������� �̵�
                    transform.position = new Vector3(transform.position.x, MAX_Y, transform.position.z);
                    next_step = STEP.AIR;
                }
                break;

            case STEP.AIR:
                if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.K))
                {
                    GoToGround();
                }
                else if (step_timer >= airHoldTime)
                {
                    GoToGround();
                }
                break;
        }
    }

    private void GoToGround()
    {
        transform.position = new Vector3(transform.position.x, MIN_Y, transform.position.z);
        next_step = STEP.RUN;
    }
}

