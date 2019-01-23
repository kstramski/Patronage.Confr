#!/bin/bash

# Script getting list of rooms

echo "Current list of rooms"
curl -X GET "http://localhost/api/Rooms/GetRoomsListAsync" -H "accept: application/json"

read -n1 -r -p "Press any key to continue..." key