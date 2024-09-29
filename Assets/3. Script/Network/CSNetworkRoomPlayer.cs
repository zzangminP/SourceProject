using Mirror;
using UnityEngine;

public class CSNetworkRoomPlayer : NetworkRoomPlayer
{
    // 클라이언트에서 F1 키를 누르면 서버에 준비 상태를 전환하도록 요청합니다.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CmdChangeReadyState(!readyToBegin);

        }
    }


}
