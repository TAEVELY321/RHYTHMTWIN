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

        // ���� �����¿��� x ���� �߰��� �������� �̵�
        this.position_offset = this.transform.position - this.player.transform.position;

        // �߰��� �������� �̵��ϰ� �ʹٸ� ���⼭ ����
        this.position_offset.x += 3.0f; // �� ���� Ŭ���� ĳ���ʹ� ���ʿ� ��ġ
    }
    void LateUpdate()
    { // ��� ���� ������Ʈ�� Update() �޼��� ó�� �Ŀ� �ڵ����� ȣ��
      // ī�޶� ���� ��ġ�� new_position�� �Ҵ�
        Vector3 new_position = this.transform.position;
        // �÷��̾��� X��ǥ�� ���� ���� ���ؼ� new_position�� X�� ����
        new_position.x = this.player.transform.position.x + this.position_offset.x;
        // ī�޶� ��ġ�� ���ο� ��ġ�� ����
        this.transform.position = new_position;
    }
}
