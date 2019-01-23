#!/bin/bash

# Script createting two identical rooms and throwing an exception

room=15


clear

echo "Creating room nr $room"
curl -X POST "http://localhost/api/Rooms/CreateRoomAsync" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"id\":$room,\"name\":\"Room $room\"}"
echo ; echo ;

echo "Creating duplicate"
curl -X POST "http://localhost/api/Rooms/CreateRoomAsync" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"id\":$room,\"name\":\"Room $room\"}"

read -n1 -r -p "Press any key to continue..." key