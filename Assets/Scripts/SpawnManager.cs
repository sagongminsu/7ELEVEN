using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject Rock;
    public GameObject Tree;
    public GameObject Alien;
    public GameObject Sheep1;
    public GameObject Sheep2;
    [SerializeField] public int treeCount;
    [SerializeField] public int rockCount;
    [SerializeField] public int alienCount;
    [SerializeField] public int sheep1Count;
    [SerializeField] public int sheep2Count;

    // Start is called before the first frame update
    void Start()
    {
        InitialSpawnRock();
        InitialSpawnTree();
        InitialSpawnAlien();
        InitialSpawnSheep1();
        InitialSpawnSheep2();
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

            while (Physics.OverlapSphere(randomSpawnPosition, 3).Length > 0){
                randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            }

            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out RaycastHit hit, 200, 8))
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

            while (Physics.OverlapSphere(randomSpawnPosition, 3).Length > 0)
            {
                randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            }

            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out RaycastHit hit, 200, 8))
            {
                Instantiate(Tree, hit.point, Quaternion.identity);
            }
        }
    }

    void InitialSpawnAlien()
    {
        for (int i = 0; i < alienCount; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));

            while (Physics.OverlapSphere(randomSpawnPosition, 3).Length > 0)
            {
                randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            }

            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out RaycastHit hit, 200, 8))
            {
                Instantiate(Alien, hit.point, Quaternion.identity);
            }
        }
    }

    void InitialSpawnSheep1()
    {
        for (int i = 0; i < sheep1Count; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));

            while (Physics.OverlapSphere(randomSpawnPosition, 3).Length > 0)
            {
                randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            }

            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out RaycastHit hit, 200, 8))
            {
                Instantiate(Sheep1, hit.point, Quaternion.identity);
            }
        }
    }

    void InitialSpawnSheep2()
    {
        for (int i = 0; i < sheep2Count; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));

            while (Physics.OverlapSphere(randomSpawnPosition, 3).Length > 0)
            {
                randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            }

            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out RaycastHit hit, 200, 8))
            {
                Instantiate(Sheep2, hit.point, Quaternion.identity);
            }
        }
    }
}
