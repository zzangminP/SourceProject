using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public PlayerControl player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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


        player.timer.text = "00:00";
        OnTimerComplete();
    }


    private void OnTimerComplete()
    {
        Debug.Log("Time's up!");

    }
}
