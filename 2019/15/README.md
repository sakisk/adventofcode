# Day 15: Oxygen System
## Part 1
Out here in deep space, many things can go wrong. Fortunately, many of those things have indicator lights. Unfortunately, one of those lights is lit: the oxygen system for part of the ship has failed!

According to the readouts, the oxygen system must have failed days ago after a rupture in oxygen tank two; that section of the ship was automatically sealed once oxygen levels went dangerously low. A single remotely-operated **repair droid** is your only option for fixing the oxygen system.

The Elves' care package included an Intcode program (your puzzle input) that you can use to remotely control the repair droid. By running that program, you can direct the repair droid to the oxygen system and fix the problem.

The remote control program executes the following steps in a loop forever:

- Accept a movement command via an input instruction.
- Send the movement command to the repair droid.
- Wait for the repair droid to finish the movement operation.
- Report on the status of the repair droid via an output instruction.

Only **four movement** commands are understood: north (`1`), south (`2`), west (`3`), and east (`4`). Any other command is invalid. The movements differ in direction, but not in distance: in a long enough east-west hallway, a series of commands like `4,4,4,4,3,3,3,3` would leave the repair droid back where it started.

The repair droid can reply with any of the following status codes:

- `0`: The repair droid hit a wall. Its position has not changed.
- `1`: The repair droid has moved one step in the requested direction.
- `2`: The repair droid has moved one step in the requested direction; its new position is the location of the oxygen system.

You don't know anything about the area around the repair droid, but you can figure it out by watching the status codes.

For example, we can draw the area using D for the droid, # for walls, . for locations the droid can traverse, and empty space for unexplored locations. Then, the initial state looks like this:
```
      
      
   D  
      
```      
To make the droid go north, send it 1. If it replies with 0, you know that location is a wall and that the droid didn't move:
```
      
   #  
   D  
      
```
To move east, send 4; a reply of 1 means the movement was successful:
```
      
   #  
   .D 
      
```      
Then, perhaps attempts to move north (1), south (2), and east (4) are all met with replies of 0:
```
      
   ## 
   .D#
    # 
```      
Now, you know the repair droid is in a dead end. Backtrack with 3 (which you already know will get a reply of 1 because you already know that location is open):
```
      
   ## 
   D.#
    # 
```
Then, perhaps west (3) gets a reply of 0, south (2) gets a reply of 1, south again (2) gets a reply of 0, and then west (3) gets a reply of 2:
```
      
   ## 
  #..#
  D.# 
   #  
```
Now, because of the reply of 2, you know you've found the oxygen system! In this example, it was only 2 moves away from the repair droid's starting position.

**What is the fewest number of movement commands** required to move the repair droid from its starting position to the location of the oxygen system?

## Puzzle input
```
3,1033,1008,1033,1,1032,1005,1032,31,1008,1033,2,1032,1005,1032,58,1008,1033,3,1032,1005,1032,81,1008,1033,4,1032,1005,1032,104,99,101,0,1034,1039,1002,1036,1,1041,1001,1035,-1,1040,1008,1038,0,1043,102,-1,1043,1032,1,1037,1032,1042,1105,1,124,1002,1034,1,1039,101,0,1036,1041,1001,1035,1,1040,1008,1038,0,1043,1,1037,1038,1042,1106,0,124,1001,1034,-1,1039,1008,1036,0,1041,101,0,1035,1040,102,1,1038,1043,1001,1037,0,1042,1105,1,124,1001,1034,1,1039,1008,1036,0,1041,1002,1035,1,1040,1001,1038,0,1043,102,1,1037,1042,1006,1039,217,1006,1040,217,1008,1039,40,1032,1005,1032,217,1008,1040,40,1032,1005,1032,217,1008,1039,5,1032,1006,1032,165,1008,1040,7,1032,1006,1032,165,1101,2,0,1044,1106,0,224,2,1041,1043,1032,1006,1032,179,1102,1,1,1044,1105,1,224,1,1041,1043,1032,1006,1032,217,1,1042,1043,1032,1001,1032,-1,1032,1002,1032,39,1032,1,1032,1039,1032,101,-1,1032,1032,101,252,1032,211,1007,0,31,1044,1106,0,224,1101,0,0,1044,1106,0,224,1006,1044,247,1002,1039,1,1034,101,0,1040,1035,1001,1041,0,1036,102,1,1043,1038,1002,1042,1,1037,4,1044,1105,1,0,9,21,83,15,75,17,11,9,80,22,37,23,19,89,6,29,79,24,75,3,39,3,98,13,20,53,24,30,59,26,13,19,63,84,10,2,57,7,22,43,28,72,11,25,67,17,90,6,10,24,93,76,36,21,34,18,19,15,72,53,18,19,82,8,57,40,18,2,48,71,19,46,26,32,69,29,27,42,8,58,25,17,44,39,47,24,54,32,48,6,26,43,91,4,16,47,45,19,73,3,52,43,25,5,22,73,58,12,56,23,44,7,46,96,48,25,8,16,56,20,48,72,28,44,26,14,23,28,61,29,15,69,86,28,97,6,4,77,4,1,37,55,70,69,22,19,23,78,21,41,2,1,48,29,20,30,22,91,36,15,46,16,83,5,95,38,9,42,84,25,45,3,81,38,79,8,1,78,42,25,58,15,29,48,52,19,36,4,27,43,24,62,6,56,60,22,22,48,23,70,8,83,17,13,63,85,25,13,14,85,79,18,13,63,3,48,94,22,73,18,26,40,68,12,25,10,56,90,59,19,68,25,27,20,20,65,1,22,55,20,1,20,88,24,69,65,13,49,8,5,78,77,1,3,93,9,13,34,17,75,28,13,92,66,35,7,98,3,63,78,59,87,2,80,83,56,15,28,96,25,32,3,27,47,5,73,56,9,59,19,16,60,2,21,50,92,44,19,73,64,7,21,39,19,20,20,63,5,12,6,14,34,12,8,48,12,68,33,14,99,9,85,20,76,18,29,99,52,11,5,98,65,83,15,30,97,35,21,96,4,53,44,23,39,25,53,60,78,85,11,7,4,39,23,84,22,29,56,37,88,18,19,84,4,65,86,8,27,66,24,26,19,95,13,19,61,19,42,85,14,19,29,90,22,15,78,18,90,8,24,21,97,86,15,40,21,61,21,49,61,6,88,40,9,2,38,13,85,16,50,55,93,83,16,77,25,27,91,8,95,15,60,70,63,13,24,24,96,30,8,22,27,74,17,14,92,18,49,4,38,9,33,88,12,62,28,35,77,29,59,3,18,45,5,10,42,58,23,78,72,15,79,2,48,47,14,65,24,5,83,41,11,89,4,57,36,19,12,2,40,21,16,44,36,13,69,70,1,11,51,16,68,30,24,83,26,40,14,82,48,10,5,83,1,76,90,15,44,24,10,88,30,24,78,1,54,97,83,27,46,87,5,19,86,19,48,19,9,50,20,69,17,10,80,34,23,24,18,75,19,20,21,73,11,32,5,15,35,2,77,22,53,18,22,86,6,9,37,30,64,28,77,17,28,12,41,62,59,2,92,97,77,14,3,76,85,11,47,14,85,6,53,2,18,52,29,23,54,35,75,5,97,40,6,45,4,75,64,5,13,86,7,84,84,1,38,23,81,72,5,26,97,70,14,40,9,41,63,41,26,80,57,14,69,90,2,28,95,24,21,80,18,26,33,39,29,11,70,73,69,17,79,13,7,73,6,21,11,75,35,10,23,30,78,75,1,1,73,4,62,30,11,21,6,38,8,40,9,56,3,24,92,66,3,86,61,28,40,17,81,74,58,92,19,4,48,34,39,30,14,36,35,73,12,15,60,49,77,13,53,77,12,20,78,18,34,17,36,17,53,64,7,63,26,20,19,94,16,26,84,13,18,60,47,17,11,56,2,48,53,11,8,79,94,22,14,8,95,7,12,21,77,16,44,4,89,70,96,11,81,8,72,5,35,79,45,1,47,10,86,75,82,5,47,5,65,4,50,22,34,12,84,13,62,80,63,23,45,39,36,0,0,21,21,1,10,1,0,0,0,0,0,0```
