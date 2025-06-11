using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatKeyManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            GameManager.score = 10000;
            GameManager.maxCombo = 100;
            SceneManager.LoadScene("ResultScene");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            GameManager.score = 8000;
            GameManager.maxCombo = 80;
            SceneManager.LoadScene("ResultScene");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            GameManager.score = 3000;
            GameManager.maxCombo = 40;
            SceneManager.LoadScene("ResultScene");
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            GameManager.score = 0;
            GameManager.maxCombo = 5;
            SceneManager.LoadScene("ResultScene");
        }
    }
}
