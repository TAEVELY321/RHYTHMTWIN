using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    public GameObject floorPrefab;
    public GameObject notePrefab;

    private int block_count = 0;

    public void createBlock(Vector3 position, Block.TYPE type)
    {
        GameObject prefab = null;

        switch (type)
        {
            case Block.TYPE.FLOOR:
                prefab = floorPrefab;
                break;
            case Block.TYPE.NOTE:
                prefab = notePrefab;
                break;
            default:
                return;
        }

        GameObject go = Instantiate(prefab);
        go.transform.position = position;

        // 노트라면 라인 타입 설정
        if (type == Block.TYPE.NOTE)
        {
            var judge = go.GetComponent<NoteJudge>();
            if (judge != null)
            {
                judge.laneType = (position.y >= 1.0f) ? NoteJudge.LaneType.Top : NoteJudge.LaneType.Bottom;
            }
        }

        block_count++;
    }
}
