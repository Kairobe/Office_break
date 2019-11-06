using System.Collections.Generic;
using UnityEngine;

public class Nivel : MonoBehaviour
{
    private Dictionary<string, GameObject> gameObjectPrefabs;

    private Dictionary<string, int> maxNumberOfObjectByType, collectedObjectNumber;

    [SerializeField]
    private GameObject clipPrefab, maletinPrefab, extintorPrefab;

    [SerializeField]
    private int clipMaxNumber, maletinMaxNumber, extintorMaxNumber = 0;

    public List<Vector3> availablePositions = new List<Vector3>();

    private ControladorUi controladorUi;

    private void Awake()
    {
        this.controladorUi = GameObject.FindGameObjectWithTag("PropiedadesUi").GetComponent<ControladorUi>();
    }

    // Start is called before the first frame update
    void Start()
    {
        availablePositions.Add(new Vector3(1, 3, 0));
        availablePositions.Add(new Vector3(2, 3, 0));
        availablePositions.Add(new Vector3(3, 3, 0));
        availablePositions.Add(new Vector3(4, 3, 0));
        availablePositions.Add(new Vector3(5, 3, 0));

        this.collectedObjectNumber = new Dictionary<string, int>();
        this.collectedObjectNumber.Add(nameof(Clip), 0);
        this.collectedObjectNumber.Add(nameof(Maletin), 0);

        this.gameObjectPrefabs = new Dictionary<string, GameObject>();
        this.gameObjectPrefabs.Add(nameof(Clip), clipPrefab);
        this.gameObjectPrefabs.Add(nameof(Maletin), maletinPrefab);
        this.gameObjectPrefabs.Add(nameof(Extintor), extintorPrefab);

        this.maxNumberOfObjectByType = new Dictionary<string, int>();
        this.maxNumberOfObjectByType.Add(nameof(Clip), clipMaxNumber);
        this.maxNumberOfObjectByType.Add(nameof(Maletin), maletinMaxNumber);
        this.maxNumberOfObjectByType.Add(nameof(Extintor), extintorMaxNumber);

        InitializeObjects();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary> Increases the number of collected objects of the given type in the given units. </summary>
    /// <param name="objectType"> The type of the object to increase the collected number. </param>
    /// <param name="units">
    /// (Optional) The total ammount of units to increase. By default it is set to one unit.
    /// </param>
    public void IncreaseCollectedNumber(string objectType, int? units = null)
    {
        int unitsToIncrease = units ?? 1;

        this.collectedObjectNumber[objectType] += unitsToIncrease;

        this.controladorUi.UpdateObjectCount(objectType, this.collectedObjectNumber[objectType]);
    }

    /// <summary> Initializes the different objects in random positions in the scene. </summary>
    private void InitializeObjects()
    {
        foreach (string objectType in this.maxNumberOfObjectByType.Keys)
        {
            GameObject prefab = this.gameObjectPrefabs[objectType];

            for (int i = 0; i < this.maxNumberOfObjectByType[objectType]; i++)
            {
                Vector3 position = availablePositions[Random.Range(0, availablePositions.Count)];
                Instantiate(prefab, position, prefab.transform.rotation);

                availablePositions.Remove(position);
            }
        }
    }
}