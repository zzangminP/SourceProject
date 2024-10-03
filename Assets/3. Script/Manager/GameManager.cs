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

    // Ÿ�̸� UI �ؽ�Ʈ
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

    // Ÿ�̸� ���� �Լ� (duration: ��)
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

        // Ÿ�̸Ӱ� ������ �� ó�� (��: "00:00" ǥ��)
        player.timer.text = "00:00";
        OnTimerComplete();
    }

    // Ÿ�̸Ӱ� ������ �� ������ �Լ�
    private void OnTimerComplete()
    {
        Debug.Log("Time's up!");

    }
}
