using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemySpawner : MonoBehaviour
{
    [Range(1, 16)]
    public int enemyCount;
    [SerializeField] private List<GameObject> rooms;
    [SerializeField]private List<Transform> spawnpoints;
    public List<GameObject> prefabs;
    public List<GameObject> enemies;
    void Start()
    {
        foreach (GameObject room in rooms)
        {
            Transform parent = room.transform.Find("Spawnpoints");
            List<Transform> tempArray = parent.GetComponentsInChildren<Transform>().ToList();
            foreach (Transform child in tempArray)
            {
                spawnpoints.Add(child);
            }
            spawnpoints.Remove(parent);
        }

        List<Transform> _spawnpoints = new List<Transform>(spawnpoints);

        for(int i = 0; i < enemyCount; i++)
        {
            if (_spawnpoints.Count <= 0) { break; }
            int randomSpawn = Random.Range(0, (_spawnpoints.Count - 1));
            int randomprefab = Random.Range(0, (prefabs.Count - 1));
            
            GameObject instantiatedEnemy;

            if (_spawnpoints[randomSpawn].GetComponent<EnemySpawnpoint>().prefabOverride == null)
            {
                instantiatedEnemy = Instantiate(prefabs[randomprefab], _spawnpoints[randomSpawn].position, _spawnpoints[randomSpawn].rotation);

            }
            else
            {
                instantiatedEnemy = Instantiate(_spawnpoints[randomSpawn].GetComponent<EnemySpawnpoint>().prefabOverride, _spawnpoints[randomSpawn].position, _spawnpoints[randomSpawn].rotation);
            }
            instantiatedEnemy.GetComponent<Enemy>().initalSpawnPoint = _spawnpoints[randomSpawn];
            instantiatedEnemy.GetComponent<Enemy>().room = _spawnpoints[randomSpawn].GetComponent<EnemySpawnpoint>().room;
            instantiatedEnemy.GetComponent<Enemy>().SetAnimation(_spawnpoints[randomSpawn].GetComponent<EnemySpawnpoint>().animationType.ToString());
            _spawnpoints[randomSpawn].GetComponent<EnemySpawnpoint>().room.GetComponent<Room>().AssignEnemy(instantiatedEnemy);
            _spawnpoints.Remove(_spawnpoints[randomSpawn]);
            enemies.Add(instantiatedEnemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null && enemy.GetComponent<Enemy>() != null)
            {
                if (enemy.GetComponent<Enemy>().state == EnemyState.IDLE)
                {
                    enemy.transform.position = enemy.GetComponent<Enemy>().initalSpawnPoint.position;
                    enemy.transform.rotation = enemy.GetComponent<Enemy>().initalSpawnPoint.rotation;
                }
            }  
        }
    }
}
