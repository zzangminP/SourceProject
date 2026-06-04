using UnityEngine;
using UnityEngine.UI;

public class EscMenu : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    void Start()
    {
        yesButton.onClick.AddListener(ExitGameButton);
        noButton.onClick.AddListener(ReturnToGameButton);
        
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ExitGameButton();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            ReturnToGameButton();
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void ExitGameButton()
    {
        Application.Quit();
    }

    void ReturnToGameButton()
    {
        gameObject.SetActive(false);
    }

}
