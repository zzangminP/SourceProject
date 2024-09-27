//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Mirror;
//
//public class MasterServer : NetworkBehaviour
//{
//    [SerializeField] private List<RoomInfo> rooms = new List<RoomInfo>();
//
//    public void OnRequestRoomList(NetworkConnection conn, EmptyMessage msg)
//    {
//        RoomListMessage roomListMessage = new RoomListMessage { rooms = rooms };
//        conn.Send(roomListMessage);
//    }
//
//    public void OnRoomCreated(NetworkConnection conn, RoomInfoMessage msg)
//    {
//        // ���ο� �� ���� �� ����Ʈ�� �߰�
//        RoomInfo newRoom = new RoomInfo
//        {
//            roomName = msg.roomName,
//            iPAddress = conn.,  // ȣ��Ʈ�� IP �ּ�
//            port = 7777,
//            maxPlayers = msg.maxPlayers,
//            currentPlayers = 1 // ȣ��Ʈ 1��
//        };
//
//        rooms.Add(newRoom);
//
//        // �ٸ� Ŭ���̾�Ʈ���Ե� �� ����Ʈ ������Ʈ
//        RoomListMessage roomListMessage = new RoomListMessage { rooms = rooms };
//        NetworkServer.SendToAll(roomListMessage); // ��ü Ŭ���̾�Ʈ���� �� ����Ʈ ����
//    }
//
//    public void OnJoinRoomRequest(NetworkConnection conn, JoinRoomMessage msg)
//    {
//        RoomInfo selectedRoom = rooms.Find(room => room.roomName == msg.roomName);
//        if (selectedRoom != null)
//        {
//            // �߰� ������ ���� Ŭ���̾�Ʈ�� ȣ��Ʈ�� ����
//            // �߰� ������ �߰����� Ʈ������ �����Ͽ� NAT ���� �ذ�
//            StartRelay(conn, selectedRoom);
//        }
//        else
//        {
//            Debug.LogError("Requested room not found");
//        }
//    }
//
//    private void StartRelay(NetworkConnection clientConn, RoomInfo hostRoom)
//    {
//                
//        Debug.Log("Relaying client to host: " + hostRoom.roomName);
//
//    }
//
//}
