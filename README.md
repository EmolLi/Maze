# Maze

## Overview
This project is an assignment for COMP 521 (Modern Computer Games) in McGill University. It's a 3D maze game implemented with C#, powered by Unity 3D. The goal is to practice game terrain generation and random maze generation algorithm taught in class.

The source code is in [Scripts](https://github.com/EmolLi/Maze/tree/master/Assets/Scripts) folder.

Following is the detailed requirements for the assignment.

## Requirements

The main play area consists of an outdoor terrain, divided into two major pieces by a steep (unscalable) mountainous ridge.
The player starts on one side, and a gap or hole in the terrain, too small to get through, reveals a clearly evident goal
location for the player on the other side.

On the player side, not adjacent to the ridge (so it is not accidentally traversed), is a conspicuously different patch of
terrain. Each time the player traverses (enters) this patch it changes visual state, and the third time it is entered (i.e.,
following entering and leaving twice) it disappears, dropping the player into an underground maze, from which they cannot
return.

The maze itself should be constructed as a random perfect maze, different on every playthrough, connecting the starting
location to a fixed ending point on the other side of the ridge, at or near the goal. Internally, the maze internal structure
does not necessarily need to preserve distance or topology with respect to the outside terrain, and can include elevation as
well as direction changes (i.e., it can be 3D as well as 2D if you want). Some elevation change is in fact required, as the
ending point should be level with the destination terrain.

The maze structure begins with a relatively straight initial entry hallway. After this short run, the actual maze structure
begins. Movement is continuous, but you can base the structure on a 16x16 grid (some variation in grid-size is ok). It
does not need to fully fill the grid, but must contain at least three dead-end branches. Each of these dead-ends consists of a larger, 4x4-sized region (“room”) containing a single object, representing a key that the player can pick up by passing over it. In order to exit the maze the player must have all 3 keys.

To offer some challenge, an opponent object is spawned at each key location, as soon as the player first enters the corresponding room. This object moves at a constant speed, slower (about 1/2 the speed) than the player, and begins a
deterministic motion, following a depth-first, exhaustive exploration of the entire maze, including the other rooms, but
treating the initial hallway as a dead end. Once it completes a full traversal and has returned to its starting point, it begins again. Contact between the player and this object ends the game (lose), and it should not be possible for the player to avoid overlap with it within the constraints of a maze tunnel.

The player also has an active option to dealing with the opponents, in the ability to shoot projectiles. The player has an
unbounded number of projectiles she can shoot, but only one projectile may be in flight at any one time, will disappear
upon contact with a wall or the maze entrance/exit. A projectile encountering an opponent has a 25% chance of destroying
it (along with the projectile), and a 75% chance of passing harmlessly through the opponent. Projectiles should be clearly
visible, move outward from the player in the direction the player is facing at that point, and move about 2× the speed of
the player.

Once the player reaches the goal location with all keys, the game terminates (win).

## ref 
- [Perlin Noise in Unity](https://www.youtube.com/watch?v=bG0uEXV6aHQ)
- [Terrain Generation with Perlin Noise](https://www.youtube.com/watch?v=vFvwyu_ZKfU&t=28s)
- [Diamond Square Algo Wiki](https://en.wikipedia.org/wiki/Diamond-square_algorithm)
- [Diamond Square Procedural Terrain](https://www.youtube.com/watch?v=1HV8GbFnCik)
- [Custom Mesh](https://www.youtube.com/watch?v=UeqBwK27sV4&feature=youtu.be)
