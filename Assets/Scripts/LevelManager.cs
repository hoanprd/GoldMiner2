using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] prefabToSpawn;  // Tham chiếu đến prefab cần sinh ra
    public Transform parentObject;    // Tham chiếu đến đối tượng cha
    public int[] levelTarget;
    public int levelIndex;
    public static int levelEndGame;

    void Start()
    {
        levelEndGame = 2;
        //levelIndex = PlayerPrefs.GetInt("Level");
        levelIndex = 0;


        // Sinh ra prefab và gán vào đối tượng cha
        SpawnPrefab();
    }

    void SpawnPrefab()
    {
        // Kiểm tra nếu prefab và đối tượng cha được gán
        if (prefabToSpawn != null && parentObject != null)
        {
            for (int i = 0; i < prefabToSpawn.Length; i++)
            {
                if (i == levelIndex)
                {
                    // Sinh ra prefab tại vị trí (0, 0, 0) và với góc quay (0, 0, 0)
                    GameObject newObject = Instantiate(prefabToSpawn[i], Vector3.zero, Quaternion.identity);
                    // Gán đối tượng vừa sinh ra vào đối tượng cha
                    newObject.transform.SetParent(parentObject);
                }
            }
        }
        else
        {
            Debug.LogWarning("Prefab hoặc đối tượng cha chưa được gán!");
        }
    }
}