using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public void OnClickNormal()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void OnClickHard()
    {
        SceneManager.LoadScene("HardGameScene");
    }
    public void OnClickCraze()
    {
        SceneManager.LoadScene("CrazeGameScene");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("GameScene");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("HardGameScene");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("CrazeGameScene");
        }
    }
}
