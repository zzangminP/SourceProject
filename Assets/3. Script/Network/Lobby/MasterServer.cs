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
//        // 새로운 방 생성 및 리스트에 추가
//        RoomInfo newRoom = new RoomInfo
//        {
//            roomName = msg.roomName,
//            iPAddress = conn.,  // 호스트의 IP 주소
//            port = 7777,
//            maxPlayers = msg.maxPlayers,
//            currentPlayers = 1 // 호스트 1명
//        };
//
//        rooms.Add(newRoom);
//
//        // 다른 클라이언트에게도 방 리스트 업데이트
//        RoomListMessage roomListMessage = new RoomListMessage { rooms = rooms };
//        NetworkServer.SendToAll(roomListMessage); // 전체 클라이언트에게 방 리스트 전송
//    }
//
//    public void OnJoinRoomRequest(NetworkConnection conn, JoinRoomMessage msg)
//    {
//        RoomInfo selectedRoom = rooms.Find(room => room.roomName == msg.roomName);
//        if (selectedRoom != null)
//        {
//            // 중계 서버를 통해 클라이언트와 호스트를 연결
//            // 중계 서버가 중간에서 트래픽을 관리하여 NAT 문제 해결
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
