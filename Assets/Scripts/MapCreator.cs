using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f;
    public static float BLOCK_HEIGHT = 0.2f;
    public static int BLOCK_NUM_IN_SCREEN = 24;

    private LevelControl level_control = null;
    private struct FloorBlock
    {
        public bool is_created;
        public Vector3 position;
    };
    private FloorBlock last_block;
    private PlayerControl player = null;
    private BlockCreator block_creator;

    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        this.last_block.is_created = false;
        this.block_creator = this.gameObject.GetComponent<BlockCreator>();
        this.level_control = new LevelControl();
        this.level_control.initialize();
    }

    private void create_floor_block()
    {
        Vector3 block_position;

        if (!this.last_block.is_created)
        {
            block_position = this.player.transform.position;
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            block_position.y = 0.0f;
        }
        else
        {
            block_position = this.last_block.position;
        }

        block_position.x += BLOCK_WIDTH;

        this.level_control.update();
        LevelControl.CreationInfo current = this.level_control.current_block;

        // 상단/하단 레인 Y 위치
        float[] laneHeights = { 0.0f, 1.0f };
        block_position.y = laneHeights[Mathf.Clamp(current.height, 0, 1)];

        if (current.block_type != Block.TYPE.NONE)
        {
            this.block_creator.createBlock(block_position, current.block_type);
        }

        this.last_block.position = block_position;
        this.last_block.is_created = true;
    }

    public bool isDelete(GameObject block_object)
    {
        float left_limit = this.player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
        return block_object.transform.position.x < left_limit;
    }

    void Update()
    {
        float block_generate_x = this.player.transform.position.x + BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;

        while (this.last_block.position.x < block_generate_x)
        {
            // 1) 바닥은 항상 생성
            this.create_floor();
        }
    }
    private void create_floor()
    {
        Vector3 block_position;

        if (!this.last_block.is_created)
        {
            block_position = this.player.transform.position;
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            block_position.y = -1.0f; // 바닥을 한 칸 아래로 이동
        }
        else
        {
            block_position = this.last_block.position;
        }

        block_position.x += BLOCK_WIDTH;
        block_position.y = -1.0f; // 바닥을 한 칸 아래로 이동

        this.block_creator.createBlock(block_position, Block.TYPE.FLOOR);

        this.last_block.position = block_position;
        this.last_block.is_created = true;
    }
    private void create_note()
    {
        // 노트 생성 확률 (80%)
        if (Random.Range(0, 10) < 8)
        {
            Vector3 note_position = this.last_block.position;

            // 레인 선택 (0: 하단, 1: 상단)
            float[] laneHeights = { 0.0f, 1.0f };
            int lane = Random.Range(0, 2);
            note_position.y = laneHeights[lane];

            this.block_creator.createBlock(note_position, Block.TYPE.NOTE);
        }
    }
    public void CreateNoteAt(Vector3 position, Lane lane)
    {
        block_creator.createBlock(position, Block.TYPE.NOTE);
    }
    public Vector3 GetPlayerPosition()
    {
        return player.transform.position;
    }



}
