#!/bin/bash

# Script createting new room 

roomId=20

clear

echo "Creating room nr $roomId"
curl -X POST "http://localhost/api/Rooms/CreateRoomAsync" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"id\":$roomId,\"name\":\"Room $roomId\"}"
echo ; echo ;

echo "Getting room nr $roomId details before removing"
curl -X GET "http://localhost/api/Rooms/GetRoomDetailsAsync/$roomId" -H "accept: application/json"
echo ; echo ;

echo "Removing room nr $roomId"
curl -X DELETE "http://localhost/api/Rooms/DeteleRoomAsync/$roomId" -H "accept: application/json"
echo ; echo ;

echo "Getting room nr $roomId details after removing"
curl -X GET "http://localhost/api/Rooms/GetRoomDetailsAsync/$roomId" -H "accept: application/json"

read -n1 -r -p "Press any key to continue..." key