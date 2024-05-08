using UnityEngine;

[CreateAssetMenu(menuName = "LevelChunkData")]
public class LevelChunkData : ScriptableObject
{
    public enum Direction
    {
        North, East, South, West, None
    }

    public Vector3 ExitDirectionToVector()
    {
        switch (exitDirection)
        {
            case Direction.North:
                return Vector3.forward;
            case Direction.East:
                return Vector3.right;
            case Direction.South:
                return Vector3.back;
            case Direction.West:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }


    public Vector2 chunkSize = new Vector2(10f, 10f);

    public GameObject[] levelChunks;
    public Direction entryDirection;
    public Direction exitDirection;

    public bool isEndChunk = false;
    public int endTokenCount = 0;
    public bool isStartChunk = false;
}
