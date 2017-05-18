using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

	// Controls
	[Range(10,50)]
	public int obstacleChance;
	public int tileSize;
	public Vector2 levelSize;

	// Components
    public Transform obstacle;

    void Start()
	{
        Generate();
	}

    public void Generate()
    {
		// Destroy the existing obstacles before making new ones
        string parentName = "Obstacles";
		if(transform.FindChild(parentName)) {
			DestroyImmediate(transform.Find(parentName).gameObject);
        }

		// Create a parent object to hold all the tiles and make it a child of the LevelGenerator
        Transform levelParent = new GameObject(parentName).transform;
        levelParent.parent = transform;

        // Create a new map by looping through the levelSize and placing a tile every grid space
		for (int x = tileSize/2; x < levelSize.x; x+=tileSize) {
			for (int y = tileSize/2;  y < levelSize.y; y+=tileSize) {
                Vector3 tilePos = new Vector3(-levelSize.x / 2 + x, 0, -levelSize.y / 2 + y);
				if (Random.Range(1,obstacleChance) == 1) {
                    Transform newTile = Instantiate(obstacle, tilePos, Quaternion.Euler(Vector3.right * 90)) as Transform;
					newTile.parent = levelParent;
				}
			}
        }
    }

	// Draw a simple diagonal line to help visualise the size of the grid
	void OnDrawGizmos ()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawLine (new Vector3(-levelSize.x/2, 0, -levelSize.y/2), new Vector3(levelSize.x/2, 0, levelSize.y/2));
	}

}
