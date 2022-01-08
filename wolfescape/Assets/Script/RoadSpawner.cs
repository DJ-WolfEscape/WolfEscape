using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    private float offset = 30f;

    void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
        Shuffle();
    }
    public void MoveRoads()
    {
        GameObject moveroad = roads[0];
        //GameObject road = roads[0].gameObject;
        //Destroy(road);
        roads.Remove(moveroad);
        //GameObject x = Instantiate(moveroad);

        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        moveroad.transform.position = new Vector3(0, 0, newZ);
        roads.Add(moveroad);
    }
    public void Shuffle()
    {
        List<int> placements = new List<int>();


        for (int i = 1; i < 9; i++)
        {
            placements.Add(i * 30);
        }

        // Loop array
        for (int i = 2; i < 10; i++)
        {
            int rnd = Random.Range(0, placements.Count);

            roads[i].transform.position = new Vector3(0, 0, placements[rnd]);

            Debug.Log(roads[i].transform.position.z.ToString());

            Debug.Log(placements[rnd].ToString());

            placements.RemoveAt(rnd);
        }

        roads = roads.OrderBy(r => r.transform.position.z).ToList();
    }
}
