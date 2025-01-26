using UnityEngine;

public class PlayerSpawnGameObject : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start(){
        int spawnAmount = 20;
        for (int i = 0; i < spawnAmount; i++){
            GameObject playerGameObject = Instantiate(playerPrefab);
            // MoveToPositionGameObject moveToPositionGameObject = playerGameObject.AddComponent<MoveToPositionGameObject>();
            // moveToPositionGameObject.SetSpeed(Random.Range(1f, 5f));
        }
    }
}
