using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerator : MonoBehaviour
{

    [SerializeField]
    private GameObject islandPrefab;

    [SerializeField] private GameObject suppliesPrefab;
    [SerializeField] private GameObject landmarkPrefab;

    [SerializeField] private GameObject storyPrefab;

    [SerializeField] private float specialIslandChance = 0.4f;
    [SerializeField] private float supplyIslandChance = 0.8f;
    [SerializeField] private float landmarkIslandChance = 0.1f;
    [SerializeField] private float storyIslandChance = 0.1f;

    private void Start()
    {
        List<Vector3> islandPositions = GenerateIslandChain(Vector3.zero, Vector2.up, 2f, 4f, 120, 0.4f, 7, 20);
        foreach (Vector3 pos in islandPositions)
        {
            GameObject island = Instantiate(islandPrefab, pos, Quaternion.Euler(0, 0, Random.Range(0, 360)));

            float chance = Random.Range(0f, 1f);
            if (chance <= specialIslandChance)
            {
                chance = Random.Range(0f, 1f);
                if (chance <= supplyIslandChance)
                {
                    Instantiate(suppliesPrefab, island.transform);
                }
                else if (chance <= supplyIslandChance + landmarkIslandChance)
                {
                    Instantiate(landmarkPrefab, island.transform);
                }
                else if (chance <= supplyIslandChance + landmarkIslandChance + storyIslandChance)
                {
                    Instantiate(storyPrefab, island.transform);
                }
            }
        }
    }

    // Generates a chain of islands with the specified parameters
    private List<Vector3> GenerateIslandChain(Vector3 startPos, Vector3 islandsDir, float minHopDist, float maxHopDist, float maxHopAngle, float chainChance, int minChainLength, int maxChainLength)
    {
        // Add start position
        List<Vector3> positions = new List<Vector3>();
        positions.Add(startPos);

        // Randomise chain length and loop to end of it
        int chainLength = Random.Range(minChainLength, maxChainLength);

        Vector3 lastIslandPos = startPos;
        for (int j = 0; j < chainLength; j++)
        {
            // Choose direction for next island slightly offset from main islandsDir
            // Then calculate next island position
            Vector2 nextDir = Quaternion.Euler(0, 0, Random.Range(-10, 10)) * islandsDir;
            lastIslandPos = NextIslandPosition(lastIslandPos, nextDir, Random.Range(minHopDist, maxHopDist), Random.Range(-maxHopAngle / 2, maxHopAngle / 2));
            positions.Add(lastIslandPos);

            // If chainChance is met then recursively start a new chain at a 60-120 degree angle with lower chances of more subchains
            float chance = Random.Range(0f, 1f);
            if (chance < chainChance)
            {
                Vector2 newChainDir = Quaternion.Euler(0, 0, Random.Range(60, 120) * Mathf.Sign(Random.Range(-1, 1))) * islandsDir;
                List<Vector3> newChain = GenerateIslandChain(lastIslandPos, newChainDir, minHopDist, maxHopDist, maxHopAngle, chainChance / 2, (int)Mathf.Max(1f, minChainLength * 0.75f), (int)Mathf.Max(1f, maxChainLength * 0.75f));
                positions.AddRange(newChain);
            }
        }

        return positions;
    }

    // Generates the next island position given some parameters
    private Vector3 NextIslandPosition(Vector3 lastPos, Vector3 dir, float hopDist, float hopAngle)
    {
        return lastPos + (Quaternion.Euler(0, 0, hopAngle) * dir * hopDist);
    }
}
