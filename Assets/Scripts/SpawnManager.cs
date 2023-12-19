using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Rock;
    public GameObject Tree;
    [SerializeField] public int treeCount;
    [SerializeField] public int rockCount;
    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        InitialSpawnRock();
        InitialSpawnTree();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitialSpawnRock()
    {
        for (int i = 0; i < rockCount; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));

            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8))
            {
                Instantiate(Rock, hit.point, Quaternion.Euler(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f)));
            }
        }
    }

    void InitialSpawnTree()
    {
        for (int i = 0; i < treeCount; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));

            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8))
            {
                Instantiate(Tree, hit.point, Quaternion.identity);
            }
        }
    }
}
