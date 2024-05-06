using System.Collections.Generic;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData firstChunk;
    public LevelChunkData endChunk; // Changed from EndChunk to endChunk for consistency
    private LevelChunkData previousChunk;
    public Vector3 spawnOrigin;
    private Vector3 spawnPosition;
    private Vector3 endChunkSpawnPosition;
    public int chunksToSpawn = 10;
    FloatingOrigin floatingOrigin;
    public GameManager gameManager;

    private void Awake()
    {
        floatingOrigin = FindObjectOfType<FloatingOrigin>();
        gameManager = FindObjectOfType<GameManager>();
    }

    void OnEnable()
    {
        TriggerExit.OnChunkExited += PickAndSpawnChunk;
    }

    private void OnDisable()
    {
        TriggerExit.OnChunkExited -= PickAndSpawnChunk;
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PickAndSpawnChunk();
        }
    }

    private void Start()
    {
        previousChunk = firstChunk;
        endChunkSpawnPosition = spawnOrigin;
        for (int i = 0; i < chunksToSpawn; i++)
        {
            PickAndSpawnChunk();
        }
    }

    LevelChunkData PickNextChunk()
    {
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();
        LevelChunkData nextChunk = null;
        LevelChunkData.Direction nextRequiredDirection = LevelChunkData.Direction.North;

        // Check if the condition to spawn the End Chunk is met
        if (previousChunk.endTokenCount >= gameManager.endTokenCountNeeded && !gameManager.endChunkSpawned)
        {
            // Set the next chunk to be the End Chunk and mark it as spawned
            nextChunk = endChunk;
            gameManager.endChunkSpawned = true;
            Debug.Log("End chunk spawned");
        }
        else
        {
            // Proceed with selecting the next chunk based on the exit direction of the previous chunk
            switch (previousChunk.exitDirection)
            {
                case LevelChunkData.Direction.North:
                    nextRequiredDirection = LevelChunkData.Direction.South;
                    spawnPosition = spawnPosition + new Vector3(0f, 0, previousChunk.chunkSize.y);
                    break;
                case LevelChunkData.Direction.East:
                    nextRequiredDirection = LevelChunkData.Direction.West;
                    spawnPosition = spawnPosition + new Vector3(previousChunk.chunkSize.x, 0, 0);
                    break;
                case LevelChunkData.Direction.South:
                    nextRequiredDirection = LevelChunkData.Direction.North;
                    spawnPosition = spawnPosition + new Vector3(0f, 0, -previousChunk.chunkSize.y);
                    break;
                case LevelChunkData.Direction.West:
                    nextRequiredDirection = LevelChunkData.Direction.East;
                    spawnPosition = spawnPosition + new Vector3(-previousChunk.chunkSize.x, 0, 0);
                    break;
                default:
                    break;
            }

            // Add all chunks that have the required direction to the allowed chunk list
            for (int i = 0; i < levelChunkData.Length; i++)
            {
                if (levelChunkData[i].entryDirection == nextRequiredDirection)
                {
                    allowedChunkList.Add(levelChunkData[i]);
                }
            }

            // If there are no allowed chunks (likely due to end game conditions), return null
            if (allowedChunkList.Count == 0)
            {
                return null;
            }

            // Select the next chunk from the allowed list
            nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)];
        }

        return nextChunk;
    }


    void PickAndSpawnChunk()
    {
        LevelChunkData chunkToSpawn = PickNextChunk();
        GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
        previousChunk = chunkToSpawn;
        Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);
    }

    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }

    // Spawn the end chunk
    public void SpawnEndChunk()
    {
        // Ensure the end chunk is defined
        if (endChunk == null)
        {
            Debug.LogError("End chunk data is not set.");
            return;
        }

        // Get the last spawned chunk's position
        Vector3 lastSpawnedChunkPosition = spawnPosition;

        // Spawn the end chunk at the position of the last spawned chunk
        Instantiate(endChunk.levelChunks[0], lastSpawnedChunkPosition + spawnOrigin, Quaternion.identity);
    }

}
