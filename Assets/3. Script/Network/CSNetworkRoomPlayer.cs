using Mirror;
using UnityEngine;

public class CSNetworkRoomPlayer : NetworkRoomPlayer
{
    // Ŭ���̾�Ʈ���� F1 Ű�� ������ ������ �غ� ���¸� ��ȯ�ϵ��� ��û�մϴ�.
    private void Update()
    {
        Debug.Log(readyToBegin);
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Pressed F1");
            CmdChangeReadyState(!readyToBegin);

        }
    }


}
