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

    // Ÿ�̸� �ڷ�ƾ
    private IEnumerator TimerCoroutine(float duration)
    {
        float remainingTime = duration;

        while (remainingTime > 0)
        {
            // ���� �ð��� ��:�� �������� ��ȯ
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            // UI �ؽ�Ʈ ������Ʈ
            player.timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // 1�ʸ��� ����
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
