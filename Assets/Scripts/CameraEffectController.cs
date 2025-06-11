using UnityEngine;
using System.Collections;

public class CameraEffectController : MonoBehaviour
{
    public float rotateDuration = 0.5f;

    private Quaternion originalRotation;
    private bool isRotated = false;

    void Start()
    {
        originalRotation = transform.rotation;
    }

    public void RotateCamera()
    {
        if (!isRotated)
        {
            StopAllCoroutines();
            StartCoroutine(RotateToAngle(180f));
            isRotated = true;
        }
    }

    public void ResetCamera()
    {
        if (isRotated)
        {
            StopAllCoroutines();
            StartCoroutine(RotateToAngle(0f));
            isRotated = false;
        }
    }

    IEnumerator RotateToAngle(float targetZ)
    {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0f, 0f, targetZ);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;
            transform.rotation = Quaternion.Lerp(startRot, endRot, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }

        transform.rotation = endRot;
    }
}
