using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;
    void Start()
    {
        this.player = GameObject.FindGameObjectWithTag("Player");

        // 기존 오프셋에서 x 값을 추가로 왼쪽으로 이동
        this.position_offset = this.transform.position - this.player.transform.position;

        // 추가로 왼쪽으로 이동하고 싶다면 여기서 조정
        this.position_offset.x += 3.0f; // ← 값이 클수록 캐릭터는 왼쪽에 위치
    }
    void LateUpdate()
    { // 모든 게임 오브젝트의 Update() 메서드 처리 후에 자동으로 호출
      // 카메라 현재 위치를 new_position에 할당
        Vector3 new_position = this.transform.position;
        // 플레이어의 X좌표에 차이 값을 더해서 new_position의 X에 대입
        new_position.x = this.player.transform.position.x + this.position_offset.x;
        // 카메라 위치를 새로운 위치로 갱신
        this.transform.position = new_position;
    }
}
