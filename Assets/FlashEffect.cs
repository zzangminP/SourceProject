using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    public CanvasGroup flashCanvas;
    public float flashDuration = 2f; 
    private bool isFlashing = false;

    void Start()
    {
        TryGetComponent<CanvasGroup>(out flashCanvas);
        Debug.Log("try get ��");
        flashCanvas.alpha = 0f;  
    }

    public void FlashScreen()
    {
        Debug.Log("Flash��ũ��");
        isFlashing = true;
        flashCanvas.alpha = 1f;
        StartCoroutine(FadeFlash());
    }

    IEnumerator FadeFlash()
    {

        Debug.Log("�÷��ù� ����");
        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            flashCanvas.alpha = Mathf.Lerp(1f, 0f, elapsed / flashDuration);
            yield return null;
        }
        flashCanvas.alpha = 0f;
        Debug.Log("�÷��ù� ��");
        isFlashing = false;
    }
}
