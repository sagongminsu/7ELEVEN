using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem.HID;

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
    [SerializeField] public float treeRate;
    [SerializeField] public float rockRate;
    [SerializeField] public float alienRate;
    [SerializeField] public float sheep1Rate;
    [SerializeField] public float sheep2Rate;

    public GameObject Rocks;
    public GameObject Trees;
    public GameObject Aliens;
    public GameObject Sheeps;

    RaycastHit hit;

    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitialSpawnRock();
        InitialSpawnTree();
        InitialSpawnAlien();
        InitialSpawnSheep1();
        InitialSpawnSheep2();

        InvokeRepeating("SpawnTree", 1.0f, treeRate);
        InvokeRepeating("SpawnRock", 1.0f, rockRate);
        InvokeRepeating("SpawnAlien", 1.0f, alienRate);
        InvokeRepeating("SpawnSheep1", 1.0f, sheep1Rate);
        InvokeRepeating("SpawnSheep2", 1.0f, sheep2Rate);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void InitialSpawnRock()
    {
        for (int i = 0; i < rockCount; i++)
        {
            do
            {
                Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
                Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

            } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

            Instantiate(Rock, hit.point, Quaternion.Euler(Random.Range(-90f, -45f), Random.Range(-180f, 180f), Random.Range(-90f, -45f)), Rocks.transform);
        }
    }

    void InitialSpawnTree()
    {
        for (int i = 0; i < treeCount; i++)
        {
            do
            {
                Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
                Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

            } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

            Instantiate(Tree, hit.point, Quaternion.identity, Trees.transform);
        }
    }

    void InitialSpawnAlien()
    {
        for (int i = 0; i < alienCount; i++)
        {
            do
            {
                Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
                Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

            } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

            Instantiate(Alien, hit.point, Quaternion.identity, Aliens.transform);
        }
    }

    void InitialSpawnSheep1()
    {
        for (int i = 0; i < sheep1Count; i++)
        {
            do
            {
                Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
                Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

            } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

            Instantiate(Sheep1, hit.point, Quaternion.identity, Sheeps.transform);
        }
    }

    void InitialSpawnSheep2()
    {
        for (int i = 0; i < sheep2Count; i++)
        {
            do
            {
                Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
                Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

            } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

            Instantiate(Sheep2, hit.point, Quaternion.identity, Sheeps.transform);
        }
    }

    void SpawnTree()
    {
        do
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

        } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

        Instantiate(Tree, hit.point, Quaternion.identity, Trees.transform);
    }

    void SpawnRock()
    {
        do
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

        } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

        Instantiate(Rock, hit.point, Quaternion.Euler(Random.Range(-90f, -45f), Random.Range(-180f, 180f), Random.Range(-90f, -45f)), Rocks.transform);
    }

    void SpawnAlien()
    {
        do
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

        } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

        Instantiate(Alien, hit.point, Quaternion.identity, Aliens.transform);
    }

    void SpawnSheep1()
    {
        do
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

        } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

        Instantiate(Sheep1, hit.point, Quaternion.identity, Sheeps.transform);
    }

    void SpawnSheep2()
    {
        do
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(25f, 125f), 100, Random.Range(-35f, 75f));
            Physics.Raycast(randomSpawnPosition, Vector3.down, out hit, 200, 8);

        } while (Physics.OverlapSphere(hit.point, 3).Length > 1);

        Instantiate(Sheep2, hit.point, Quaternion.identity, Sheeps.transform);
    }


}
