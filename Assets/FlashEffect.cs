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
        Debug.Log("try get µÊ");
        flashCanvas.alpha = 0f;  
    }

    public void FlashScreen()
    {
        Debug.Log("Flash½ºÅ©¸°");
        isFlashing = true;
        flashCanvas.alpha = 1f;
        StartCoroutine(FadeFlash());
    }

    IEnumerator FadeFlash()
    {

        Debug.Log("ÇÃ·¡½Ã¹ð ½ÃÀÛ");
        float elapsed = 0f;
        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            flashCanvas.alpha = Mathf.Lerp(1f, 0f, elapsed / flashDuration);
            yield return null;
        }
        flashCanvas.alpha = 0f;
        Debug.Log("ÇÃ·¡½Ã¹ð ³¡");
        isFlashing = false;
    }
}
