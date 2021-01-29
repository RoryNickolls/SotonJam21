using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject islandPrefab;

    private void Start()
    {
        List<Vector3> islandPositions = GenerateIslandChain(Vector3.zero, Vector2.up, 2f, 6f, 120, 0.1f, 40, 70);
        foreach (Vector3 pos in islandPositions)
        {
            Instantiate(islandPrefab, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));
        }
    }

    private List<Vector3> GenerateIslandChain(Vector3 startPos, Vector3 islandsDir, float minHopDist, float maxHopDist, float maxHopAngle, float chainChance, int minChainLength, int maxChainLength)
    {
        List<Vector3> positions = new List<Vector3>();
        positions.Add(startPos);

        int chainLength = Random.Range(minChainLength, maxChainLength);
        Vector3 lastIslandPos = startPos;
        for (int j = 0; j < chainLength; j++)
        {
            Vector2 nextDir = Quaternion.Euler(0, 0, Random.Range(-10, 10)) * islandsDir;
            lastIslandPos = NextIslandPosition(lastIslandPos, nextDir, Random.Range(minHopDist, maxHopDist), Random.Range(-maxHopAngle / 2, maxHopAngle / 2));
            positions.Add(lastIslandPos);

            float chance = Random.Range(0f, 1f);
            if (chance < chainChance)
            {
                Vector2 newChainDir = Quaternion.Euler(0, 0, Random.Range(60, 120) * Mathf.Sign(Random.Range(-1, 1))) * islandsDir;
                List<Vector3> newChain = GenerateIslandChain(lastIslandPos, newChainDir, minHopDist, maxHopDist, maxHopAngle, chainChance / 2, Mathf.Max(1, minChainLength / 2), Mathf.Max(1, maxChainLength / 2));
                positions.AddRange(newChain);
            }
        }

        return positions;
    }

    private Vector3 NextIslandPosition(Vector3 lastPos, Vector3 dir, float hopDist, float hopAngle)
    {
        return lastPos + (Quaternion.Euler(0, 0, hopAngle) * dir * hopDist);
    }
}
