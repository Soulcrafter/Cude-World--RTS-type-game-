using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar {

    static WorldManager world;

    public static void RegisterWorld(WorldManager world) {

        AStar.world = world;
    }

    public static bool FindPath(Vector3Int start, Vector3Int end, out Stack<Vector3Int> path) {

        Debug.Log("Start: " + start + " Dest: " + end);


        List<Vector3Int> closedSet = new List<Vector3Int>();
        List<Vector3Int> openSet = new List<Vector3Int>();
        openSet.Add(start);

        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();

        //G score is a map with a default value of 0
        Dictionary<Vector3Int, float> gScore = new Dictionary<Vector3Int, float>();
        gScore.Add(start, 0);

        //F score is a map with evaluated nodes9
        Dictionary<Vector3Int, float> fScore = new Dictionary<Vector3Int, float>();
        fScore.Add(start, HeuristicCostEstimate(start, end));


        int attempts = 0;

        while (openSet.Count > 0) {

            attempts++;

            //Get lowest fScore position
            Vector3Int current = FindLowestFScorePosition(fScore);

            //Check goal position
            if (current.Equals(end)) {
                Debug.Log("Found path in " + attempts);
                path = ReconstructPath(cameFrom, end);
                return true;
            }

            //Edit sets
            openSet.Remove(current);
            closedSet.Add(current);

            //Find Neighbors
            List<Vector3Int> neighbors = GetNeighborsAroundPoint(current, world);

            for (int i = 0; i < neighbors.Count; i++) {
                
                if (attempts > 1000) {

                    Debug.LogError("Too many attempts, bailing out");
                    path = null;
                    return false;
                }

                if (closedSet.Contains(neighbors[i]) == true) {
                    continue; //Skip evaluated neighbors
                }

                float tentative_gScore = gScore[current] + HeuristicCostEstimate(current, neighbors[i]);

                if (!openSet.Contains(neighbors[i])) {

                    openSet.Add(neighbors[i]);
                } else {

                    float actualScore = 0; // is 0

                    if (gScore.ContainsKey(neighbors[i])) {

                        actualScore = gScore[neighbors[i]];
                    }
                    if (tentative_gScore >= actualScore) {
                        continue;
                    }
                }

                cameFrom.Add(neighbors[i], current);
                gScore.Add(neighbors[i], tentative_gScore);
                fScore.Add(neighbors[i], tentative_gScore + HeuristicCostEstimate(neighbors[i], end));
            }
        }

        path = null;
        return false;
    }

    static float HeuristicCostEstimate(Vector3Int start, Vector3Int end) {
        
        int A = Mathf.Abs(start.x - end.x);
        int B = Mathf.Abs(start.y - end.y);
        int C = Mathf.Abs(start.z - end.z);

        //Square Magnitude of Euclidean distance

        if (A > C) {
            return 14 * C + 10 * (A - C) + 10 * B;
        }

        return 14 * A + 10 * (C - A) + 10 * B;
    }

    static Vector3Int FindLowestFScorePosition(Dictionary<Vector3Int, float> fScore) {

        Vector3Int position = new Vector3Int();
        float lowest = Mathf.Infinity;

        foreach (KeyValuePair<Vector3Int, float> pair in fScore) {

            if (lowest == Mathf.Infinity || pair.Value <= lowest) {
                position = pair.Key;
                lowest = pair.Value;
            }
        }

        //Debug.Log("Current: " + position + " fScore " + lowest);
        return position;
    }

    static List<Vector3Int> GetNeighborsAroundPoint(Vector3Int point, WorldManager world) {

        List<Vector3Int> neighbors = new List<Vector3Int>();

        for (int x = -1; x <= 1; x++) {
            for (int y = -1; y <= 1; y++) {
                for (int z = -1; z <= 1; z++) {

                    Vector3Int next = new Vector3Int(point.x + x, point.y + y, point.z + z);
                    neighbors.Add(next);
                }
            }
        }

        return neighbors;
    }

    static Stack<Vector3Int> ReconstructPath(Dictionary<Vector3Int, Vector3Int> cameFrom, Vector3Int current) {

        Stack<Vector3Int> totalPath = new Stack<Vector3Int>();

        totalPath.Push(current);
        while (cameFrom.ContainsKey(current)) {
            current = cameFrom[current];
            totalPath.Push(current);
        }

        return totalPath;
    }
}
