﻿using UnityEngine;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using Unity.AI.Navigation;
using UnityEngine.UIElements;
using static UnityEngine.EventSystems.EventTrigger;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class BoardManager : MonoBehaviour
{
	int columns;//Number of columns in our game board.
	int rows;//Number of rows in our game board.
	public int Enemiesleft = 0;
	public int TileScale = 3;
	public Count wallCount = new Count(5, 9);//Lower and upper limit for our random number of walls per level.
    public Count EnemyCount = new Count(5, 9);
    public BoardPieceData exit;//Prefab to spawn for exit.
	public BoardPieceData[] floorTiles;//Array of floor prefabs.
	public BoardPieceData[] wallTiles;//Array of wall prefabs.                            
	public BoardPieceData[] outerWallTiles;//Array of outer tile prefabs.
    public GameObject[] EnemyPrefabs;
    public GameObject[] PlayerPrefab;
	public GameObject Exit;
	public GameManager GameManager;



    private Transform boardHolder;                                  //A variable to store a reference to the transform of our Board object.
	private List<Vector3> gridPositions = new List<Vector3>();  //A list of possible locations to place tiles.

	

	private void Start()
    {
        SetupScene();

		GameManager = FindAnyObjectByType<GameManager>();
		
    }

    private void Update()
    {
		if (Enemiesleft <= 0)
		{
			SpawnExit();
		}


    }

    //Clears our list gridPositions and prepares it to generate a new board.
    void InitialiseList()
	{

        //Clear our list gridPositions.
        gridPositions.Clear();

		//Loop through x axis (columns).
		for (int x = 1; x < columns - 1; x++)
		{
			//Within each column, loop through y axis (rows).
			for (int y = 1; y < rows - 1; y++)
			{
				//At each index add a new Vector3 to our list with the x and y coordinates of that position.
				gridPositions.Add(new Vector3(x, 0f, y));
			}
		}
	}

	//Sets up the outer walls and floor (background) of the game board.
	void BoardSetup()
	{
		//Instantiate Board and set boardHolder to its transform.
		boardHolder = new GameObject("Board").transform;

		//Loop along x axis, starting from -1 (to fill corner) with floor or outerwall edge tiles.
		for (int x = -1; x < columns + 1; x++)
		{
			//Loop along y axis, starting from -1 to place floor or outerwall tiles.
			for (int y = -1; y < rows + 1; y++)
			{
				//Choose a random tile from our array of floor tile prefabs and prepare to instantiate it.
				GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)].Prefab;

				//Check if we current position is at board edge, if so choose a random outer wall prefab from our array of outer wall tiles.
				if (x == -1 || x == columns || y == -1 || y == rows)
					toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)].Prefab;

				//Instantiate the GameObject instance using the prefab chosen for toInstantiate at the Vector3 corresponding to current grid position in loop, cast it to GameObject.
				GameObject instance =
					Instantiate(toInstantiate, new Vector3(x *3, 0f, y*3), Quaternion.identity) as GameObject;

				//Set the parent of our newly instantiated object instance to boardHolder, this is just organizational to avoid cluttering hierarchy.
				instance.transform.SetParent(boardHolder);
			}
		}
	}

	//RandomPosition returns a random position from our list gridPositions.
	Vector3 RandomPosition()
	{
		//Declare an integer randomIndex, set it's value to a random number between 0 and the count of items in our List gridPositions.
		int randomIndex = Random.Range(0, gridPositions.Count);

		//Declare a variable of type Vector3 called randomPosition, set it's value to the entry at randomIndex from our List gridPositions.
		Vector3 randomPosition = gridPositions[randomIndex] *3;
		randomPosition.y = 0;

		//Remove the entry at randomIndex from the list so that it can't be re-used.
		gridPositions.RemoveAt(randomIndex);

		//Return the randomly selected Vector3 position.
		return randomPosition;
	}

	//LayoutObjectAtRandom accepts an array of game objects to choose from along with a minimum and maximum range for the number of objects to create.
	void LayoutObjectAtRandom(BoardPieceData[] tileArray, int minimum, int maximum)
	{
		//Choose a random number of objects to instantiate within the minimum and maximum limits
		int objectCount = Random.Range(minimum, maximum + 1);

		//Instantiate objects until the randomly chosen limit objectCount is reached
		for (int i = 0; i < objectCount; i++)
		{
			//Choose a position for randomPosition by getting a random position from our list of available Vector3s stored in gridPosition
			Vector3 randomPosition = RandomPosition();

			//Choose a random tile from tileArray and assign it to tileChoice
			GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)].Prefab;

			//Instantiate tileChoice at the position returned by RandomPosition with no change in rotation
			
			
				Instantiate(tileChoice, randomPosition, Quaternion.identity);
			
			
		}
	}

    void LayoutEnemiesAtRandom()
    {
        int objectCount = Random.Range(EnemyCount.minimum, EnemyCount.maximum + 1);

		for(int i = 0;i < objectCount;i++) 
		{
			Enemiesleft++;
			Vector3 randomPosition = RandomPosition();
			randomPosition.x *= 1;
			randomPosition.y *= 1;
			randomPosition.z *= 1;

            Instantiate(EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)],
                                         randomPosition,
                                         Quaternion.identity);
		}
    }

    void AddPlayer()
    {
        
            Vector3 randomPosition = RandomPosition();
            randomPosition.x *= 1;
            randomPosition.y *= 1;
            randomPosition.z *= 1;

            Instantiate(
                PlayerPrefab[Random.Range(0, PlayerPrefab.Length)],
                randomPosition,
                Quaternion.identity);
        
    }

    //SetupScene initializes our level and calls the previous functions to lay out the game board
    public void SetupScene()
	{
        columns = Random.Range(10, 35);
        rows = Random.Range(10, 35);

        //Creates the outer walls and floor.
        BoardSetup();

		//Reset our list of gridpositions.
		InitialiseList();

		//Instantiate a random number of wall tiles based on minimum and maximum, at randomized positions.
		LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);

		//Instantiate the exit tile in the middle of the board
		

		GetComponent<NavMeshSurface>().BuildNavMesh();

		LayoutEnemiesAtRandom();

		AddPlayer();

    }

	public void SpawnExit()
	{
		Exit = GameObject.FindWithTag("Exit_Door");
			
			if (Exit ==  null)
				Instantiate(exit.Prefab, new Vector3((columns * TileScale) / 2, 0f, (rows * TileScale) / 2), Quaternion.identity);
			
			var hits = Physics.OverlapSphere(new Vector3((columns * TileScale) / 2, 1f, (rows * TileScale) / 2), 0.1f);
		foreach (var collider in hits)
		{
			if (!collider.CompareTag("Player"))
			{
				Destroy(collider.gameObject);
			}
			else
			{
                GameManager.SaveGame();
                UnityEngine.Cursor.lockState = CursorLockMode.Confined;
                UnityEngine.Cursor.visible = true;

                SceneManager.LoadScene("MenuTest");
            }
		}
				
		
			
		
    }
}

[Serializable]
public class Count
{
	public int minimum;             //Minimum value for our Count class.
	public int maximum;             //Maximum value for our Count class.

	public Count(int min, int max)
	{
		minimum = min;
		maximum = max;
	}
}

