using Mirror;
using UnityEngine;

public class CSNetworkRoomPlayer : NetworkRoomPlayer
{
    // Ŭ���̾�Ʈ���� F1 Ű�� ������ ������ �غ� ���¸� ��ȯ�ϵ��� ��û�մϴ�.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CmdChangeReadyState(!readyToBegin);

        }
    }


}
