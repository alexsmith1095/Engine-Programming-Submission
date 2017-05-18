# CVG Engine Programming Unity Project

This is a simple Unity game prototype created as part of a University module submission. It demonstrates basic object-oriented programming techniques and implements simple AI.

## Game Description

The Game has a start screen with simple UI including two buttons. One button loads the game scene and the other quits the application when clicked.

The Game scene contains a player character on a plane with randomly and crudely placed cube obstacles. Enemies are spawned in waves in increasing numbers. They have a chance of spawning as a faster enemy which is more difficult to kill. These faster enemies also have a chance of dropping health for the player when killed.

The enemies start off in a wander state where they get a random point on the plane to navigate to. If they see a player they will chase until they get close enough and then go into an attack state and damage the player. If they lose sight of the player they will go back into the wander state.

There are certain UI elements representing the score and health of the player as well as a mini-map and text counting down the waves.

When the player dies a game over scene is loaded which includes a play again button and a quit button.

TL;DR - The player must kill all the zombies which spawn in increasing numbers through waves.

## Gameplay Mechanics

Some main gameplay mechanics include a top-down controlled player with shooting, an interface based finite state machine controlling the AI, wave based enemy spawning and randomly generated obstacles.

### Game Controls

* Movement <kbd>W</kbd> <kbd>A</kbd> <kbd>S</kbd> <kbd>D</kbd> / <kbd>&uarr;</kbd> <kbd>&larr;</kbd> <kbd>&darr;</kbd> <kbd>&rarr;</kbd>

* Aim <kbd>Mouse</kbd>

* Sprint <kbd>Shift</kbd>

* Fire <kbd>Left Mouse Button</kbd> / <kbd>Space</kbd>

* Change Fire Mode <kbd>Right Mouse Button</kbd> / <kbd>Tab</kbd>

* Pause <kbd>Escape</kbd> / <kbd>P</kbd>

### Prefabs

* Explosion - This is instantiated when a bullet ray hits and object

* Health - This has a chance of being instantiated when a fast zombie is killed

* Obstacle - These are randomly placed on the ground plane when the scene is loaded to create a more interesting level

* Shell - These are projected from the players weapon and are collected and reused in an object pool

* Zombie & Fast Zombie - These are two variations of an enemy which will try to kill the player

* Zombie Death Particles - A particle effect that sprays a bunch of red cubes when a zombie is killed

## Editor

The scene 'Maths Example' contains a GameObject with a script attached. The script has 3 float value inputs that are then put into certain equations from the Maths code library and are displayed in the UI text elements in the scene.

The zombie state classes utilise the AI class in the CustomFunctions code library. One example is the WanderState using 'GetRandomTargetPosition' and passing in the zombies current position and a random range as the float value. The function from the library can be seen below.

```
public static Vector3 GetRandomTargetPosition(Vector3 position, float distance)
{
    Vector3 randomDirection = Random.insideUnitSphere * distance;
    randomDirection += position;
    UnityEngine.NavMeshHit navMeshHit;
    UnityEngine.NavMesh.SamplePosition(randomDirection, out navMeshHit, distance, -1);
    return navMeshHit.position;
}
```

The GameObject 'Level Generator' has a script attached to it that loops through the size of the level specified and places a number of cube prefabs as obstacles depending on the chance specified, with the lower number being a higher possibility.

## Credit

* [**Alex Smith**](https://github.com/alexsmith1095) - Created for a second year programming module for Computer & Video Games at University of Salford

## Acknowledgments

* [Sebastian Lague](https://www.youtube.com/user/Cercopithecan/) - For inspiration and teaching in-depth the majority of the principles used.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
