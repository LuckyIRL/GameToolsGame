using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelChunkData")]
public class LevelChunkData : MonoBehaviour
{
    public enum Direction 
    {
        North, East, South, West, End
    }

    public Vector2 chunkSize = new Vector2(10f, 10f);

    public GameObject[] levelChunks;
    public Direction entryDirection;
    public Direction exitDirection;
    public Direction exitExitDirection;

    public bool isEndChunk = false;
    public int endTokenCount = 0;
    public bool isStartChunk = false;
}
