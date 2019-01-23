#!/bin/bash

# Script createting three new rooms and returning list of rooms 

room1=1
room2=2
room3=3

clear

echo "Creating room nr $room1"
curl -X POST "http://localhost/api/Rooms/CreateRoomAsync" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"id\":$room1,\"name\":\"Room $room1\"}"
echo ; echo ;

echo "Creating room nr $room2"
curl -X POST "http://localhost/api/Rooms/CreateRoomAsync" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"id\":$room2,\"name\":\"Room $room2\"}"
echo ; echo ;

echo "Creating room nr $room3"
curl -X POST "http://localhost/api/Rooms/CreateRoomAsync" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"id\":$room3,\"name\":\"Room $room3\"}"
echo ; echo ;

echo "Current list of rooms"
curl -X GET "http://localhost/api/Rooms/GetRoomsListAsync" -H "accept: application/json"

read -n1 -r -p "Press any key to continue..." key