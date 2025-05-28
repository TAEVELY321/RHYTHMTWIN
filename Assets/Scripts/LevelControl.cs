using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    public struct CreationInfo
    {
        public Block.TYPE block_type;
        public int max_count;
        public int height; // → 0: 하단, 1: 상단
        public int current_count;
    };

    public CreationInfo previous_block;
    public CreationInfo current_block;
    public CreationInfo next_block;
    public int block_count = 0;
    public int level = 0;

    private void clear_next_block(ref CreationInfo block)
    {
        block.block_type = Block.TYPE.FLOOR;
        block.max_count = 10;
        block.height = 0;
        block.current_count = 0;
    }

    public void initialize()
    {
        this.block_count = 0;
        this.clear_next_block(ref this.previous_block);
        this.clear_next_block(ref this.current_block);
        this.clear_next_block(ref this.next_block);
    }

    private void update_level(ref CreationInfo current, CreationInfo previous)
    {
        int rand = Random.Range(0, 5);
        if (rand == 0)
        {
            current.block_type = Block.TYPE.NOTE;
            current.max_count = 1;
            current.height = Random.Range(0, 2); // 0: 하단, 1: 상단
        }
        else
        {
            current.block_type = Block.TYPE.FLOOR;
            current.max_count = 10;
            current.height = 0;
        }
    }

    public void update()
    {
        this.current_block.current_count++;

        if (this.current_block.current_count >= this.current_block.max_count)
        {
            this.previous_block = this.current_block;
            this.current_block = this.next_block;
            this.clear_next_block(ref this.next_block);
            this.update_level(ref this.next_block, this.current_block);
        }

        this.block_count++;
    }
}
