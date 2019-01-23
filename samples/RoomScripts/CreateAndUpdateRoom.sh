#!/bin/bash

# Script creating and updating room

room=16

clear

echo "Creating room nr $room"
curl -X POST "http://localhost/api/Rooms/CreateRoomAsync" -H "accept: application/json" -H "Content-Type: application/json" -d "{\"id\":$room,\"name\":\"Room $room\"}"
echo ; echo ;

echo "Updating room"
curl -X PUT "http://localhost/api/Rooms/UpdateRoomAsync/$room" -H "accept: application/json" -H "Content-Type: application/json" -d "{ \"name\": \"Room $room\", \"calendar\": [ \"2019-01-23\",\"2019-01-27\",\"2019-01-29\", ]}"
echo ; echo ;

echo "Getting room calendar after update"
curl -X GET "http://localhost/api/Rooms/GetRoomCalendarAsync/$room/calendar" -H "accept: application/json"
echo ; echo ;

echo "Second update"
curl -X PUT "http://localhost/api/Rooms/UpdateRoomAsync/$room" -H "accept: application/json" -H "Content-Type: application/json" -d "{ \"name\": \"Room $room\", \"calendar\": [ \"2019-01-23\",\"2019-01-29\",\"2019-01-30\",\"2019-02-05\", ]}"
echo ; echo ;

echo "Getting room calendar after second update"
curl -X GET "http://localhost/api/Rooms/GetRoomCalendarAsync/$room/calendar" -H "accept: application/json"

read -n1 -r -p "Press any key to continue..." key