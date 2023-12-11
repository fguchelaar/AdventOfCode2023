#!/bin/bash

# Loop from 1 to 25
for day in {1..25}
do
  # Pad the day with a leading zero if necessary
  formattedDay=$(printf "%02d" $day)

  # Construct the directory name
  dirName="day-$formattedDay"

  # Check if the directory exists
  if [ -d "$dirName" ]; then
    echo "Running project in $dirName"
    cd "$dirName"
    time dotnet run --configuration Release
    cd - # Go back to the original directory
  fi
done
