using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeUp : MonoBehaviour
{
    public Button titleButton;
    public PlayerControl playerControl;
    public TextMeshProUGUI killCount;
    void Start()
    {
        playerControl = GetComponentInParent<PlayerControl>();
        titleButton.onClick.AddListener(ReturnToTitleButton);    
    }
    private void OnEnable()
    {
        killCount.text = ($"Kills : {playerControl.killCount}");
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            ReturnToTitleButton();
        }
    }
    public void ReturnToTitleButton()
    {
        SceneManager.LoadScene("title");
    }
}
