using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelLayoutGenerator : MonoBehaviour
{
    public LevelChunkData[] levelChunkData;
    public LevelChunkData firstChunk;

    private LevelChunkData previousChunk;

    public Vector3 spawnOrigin;

    private Vector3 spawnPosition;
    public int chunksToSpawn = 10;

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

        for (int i = 0; i < chunksToSpawn; i++)
        {
            PickAndSpawnChunk();
        }
    }

    LevelChunkData PickNextChunk() 
    {
        // List of allowed chunks and the next chunk to spawn
        List<LevelChunkData> allowedChunkList = new List<LevelChunkData>();
        LevelChunkData nextChunk = null;

        LevelChunkData.Direction nextRequiredDirection = LevelChunkData.Direction.North;

        // Set the spawn position and the required direction for the next chunk
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

        nextChunk = allowedChunkList[Random.Range(0, allowedChunkList.Count)];

        return nextChunk;
    }

    // Pick a random chunk from the list of allowed chunks and spawn it
    void PickAndSpawnChunk()
    {
        LevelChunkData chunkToSpawn = PickNextChunk();

        GameObject objectFromChunk = chunkToSpawn.levelChunks[Random.Range(0, chunkToSpawn.levelChunks.Length)];
        previousChunk = chunkToSpawn;
        Instantiate(objectFromChunk, spawnPosition + spawnOrigin, Quaternion.identity);

    }

    // Update the spawn origin
    public void UpdateSpawnOrigin(Vector3 originDelta)
    {
        spawnOrigin = spawnOrigin + originDelta;
    }
}
