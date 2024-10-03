using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public PlayerControl player;

    // 타이머 UI 텍스트
    public TextMeshProUGUI timer_text;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(gameObject);
        player = GameObject.Find("gsg9Player").GetComponent<PlayerControl>();
    }

    

    private void Update()
    {
        if(SceneManager.GetActiveScene().name =="de_dust2")
        {


            

        }
    }

    // 타이머 시작 함수 (duration: 초)
    public void StartTimer(float duration)
    {
        StartCoroutine(TimerCoroutine(duration));
    }

    // 타이머 코루틴
    private IEnumerator TimerCoroutine(float duration)
    {
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            // 남은 시간을 분:초 형식으로 변환
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            // UI 텍스트 업데이트
            player.timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // 1초마다 감소
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        // 타이머가 끝났을 때 처리 (예: "00:00" 표시)
        player.timer.text = "00:00";
        OnTimerComplete();
    }

    // 타이머가 끝났을 때 실행할 함수
    private void OnTimerComplete()
    {
        Debug.Log("Time's up!");

    }
}
