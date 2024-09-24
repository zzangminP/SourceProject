using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class FlashEffect : MonoBehaviour
{
    public CanvasGroup flashCanvasImage;
    public CanvasGroup flashCanvasEffect;
    //public Image flashImage;
    public float flashDuration = 2f; 
    private bool isFlashing = false;

    private int width, height;

    

    void Start()
    {
        //TryGetComponent<CanvasGroup>(out flashCanvas);
        flashCanvasImage = GetComponent<CanvasGroup>();
        flashCanvasEffect = gameObject.transform.GetChild(0).GetComponentInChildren<CanvasGroup>();

        
        


        width = Screen.width;
        height = Screen.height;
        
        flashCanvasImage.alpha = 0f;  
    }

    public void FlashScreen()
    {

        isFlashing = true;
        StartCoroutine(FlashImage());


        
        
    }

    IEnumerator FadeFlash()
    {



        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
           

            elapsed += Time.deltaTime;
            flashCanvasImage.alpha = Mathf.Lerp(1f, 0f, elapsed / flashDuration);
            flashCanvasEffect.alpha = Mathf.Lerp(1f, 0f, elapsed / flashDuration);
            yield return null;
        }

        flashCanvasImage.alpha = 0f;
        flashCanvasEffect.alpha = 0f;

        isFlashing = false;
    }

    IEnumerator FlashImage()
    {
        yield return new WaitForEndOfFrame();



        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        gameObject.GetComponent<Image>().sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));


        flashCanvasImage.alpha = 1.0f;
        flashCanvasEffect.alpha = 1.0f;
        StartCoroutine(FadeFlash());
    }
}
