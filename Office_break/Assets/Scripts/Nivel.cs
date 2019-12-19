using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nivel : MonoBehaviour
{
    private Dictionary<string, GameObject> gameObjectPrefabs;

    private Dictionary<string, int> maxNumberOfObjectByType, collectedObjectNumber;

    [SerializeField]
    private GameObject clipPrefab, maletinPrefab, extintorPrefab;

    [SerializeField]
    private int clipMaxNumber, maletinMaxNumber, extintorMaxNumber = 0;

    public List<(Vector3 position, int rotation)> availablePositions;

    private ControladorUi controladorUi;

    public GameObject[] currentLevelCheckPoints;

    private int activeCheckPoint = 0;

    private int lapNumber = 0;

    [SerializeField]
    private int raceLapsNumber;

    // Start is called before the first frame update
    void Start()
    {
        InitializePositionsAndRotation();

        InitializeObjectsToCollect();

        InitializeMaximumNumberOfInstancesByGameObject();

        InitializeGameObjectPrefabs();

        InitializeObjects();

        this.controladorUi.UpdateLapCounter(this.lapNumber, this.raceLapsNumber);
    }

    private void Awake()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        this.currentLevelCheckPoints = new GameObject[checkpoints.Length];

        this.currentLevelCheckPoints[0] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint0");
        this.currentLevelCheckPoints[1] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint1");
        this.currentLevelCheckPoints[2] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint2");
        this.currentLevelCheckPoints[3] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint3");

        for (int i = 1; i < this.currentLevelCheckPoints.Length; i++)
        {
            this.currentLevelCheckPoints[i].SetActive(false);
        }

        this.controladorUi = GameObject.FindGameObjectWithTag("PropiedadesUi").GetComponent<ControladorUi>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Initializes the manixum number of instances that must be generated of each Game Object type.
    /// </summary>
    private void InitializeMaximumNumberOfInstancesByGameObject()
    {
        this.maxNumberOfObjectByType = new Dictionary<string, int>
        {
            { nameof(Clip), clipMaxNumber },
            { nameof(Maletin), maletinMaxNumber },
            { nameof(Extintor), extintorMaxNumber }
        };
    }

    /// <summary> Initializes the prefabs of the different Game Objects to be created. </summary>
    private void InitializeGameObjectPrefabs()
    {
        this.gameObjectPrefabs = new Dictionary<string, GameObject>
        {
            { nameof(Clip), clipPrefab },
            { nameof(Maletin), maletinPrefab },
            { nameof(Extintor), extintorPrefab }
        };
    }

    /// <summary> Initializes the Game Objects that are collectable. </summary>
    private void InitializeObjectsToCollect()
    {
        this.collectedObjectNumber = new Dictionary<string, int>
        {
            { nameof(Clip), 0 },
            { nameof(Maletin), 0 }
        };
    }

    /// <summary>
    /// Initializes the available positions and the rotation that must be applied to the created
    /// Game Object.
    /// </summary>
    private void InitializePositionsAndRotation()
    {
        availablePositions = new List<(Vector3 position, int rotation)>();

        availablePositions.Add((new Vector3(17.5f, 0.5f, -20), 0));
        availablePositions.Add((new Vector3(17.5f, 0.5f, -28), 0));
        availablePositions.Add((new Vector3(16, 0.5f, -32.5f), 90));
        availablePositions.Add((new Vector3(10f, 0.5f, -32.5f), 90));
        availablePositions.Add((new Vector3(1, 0.5f, -33), 0));
        availablePositions.Add((new Vector3(1, 0.5f, -24.5f), 0));
        availablePositions.Add((new Vector3(-1.5f, 0.5f, -15), 0));
        availablePositions.Add((new Vector3(5.3f, 0.5f, -11.5f), 90));
        availablePositions.Add((new Vector3(6, 0.5f, -15.8f), 90));
        availablePositions.Add((new Vector3(12, 0.5f, -17), 90));
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

    public void ActivateNextCheckPoint(int checkPointPassedNumber)
    {
        if (checkPointPassedNumber != this.activeCheckPoint)
        {
            return;
        }

        if (checkPointPassedNumber == 0)
        {
            if (this.lapNumber == this.raceLapsNumber)
            {
                LevelData currentLevelData = new LevelData("admin", this.collectedObjectNumber["Clip"], this.collectedObjectNumber["Maletin"]);
                CurrentLevelController.CurrentLevelData = currentLevelData;
                UpdateCurrentUserData(currentLevelData);

                //Para que vaya a la pantalla de fin de nivel:
                SceneManager.LoadScene(4);

                //this.controladorUi.EndGame();
            }

            this.lapNumber++;
            this.controladorUi.UpdateLapCounter(this.lapNumber, this.raceLapsNumber);

            if (this.lapNumber > 1)
            {
                this.InitializeObjects();
            }
        }

        int currentCheckPoint = this.activeCheckPoint;
        int nextCheckPoint = (++this.activeCheckPoint) % this.currentLevelCheckPoints.Length;

        this.currentLevelCheckPoints[currentCheckPoint].SetActive(false);
        this.currentLevelCheckPoints[nextCheckPoint].SetActive(true);
        this.activeCheckPoint = nextCheckPoint;
    }

    private static void UpdateCurrentUserData(LevelData currentLevelData)
    {
        UserData currentUserData = DataManager.LoadData("admin");

        if (currentUserData is null)
        {
            currentUserData = new UserData("admin");
        }

        currentUserData.UpdateUserData(currentLevelData);
    }

    /// <summary> Initializes the different objects in random positions in the scene. </summary>
    private void InitializeObjects()
    {
        List<(Vector3 position, int rotation)> availablePositionsCopy = new List<(Vector3 position, int rotation)>(availablePositions);

        foreach (string objectType in this.maxNumberOfObjectByType.Keys)
        {
            GameObject prefab = this.gameObjectPrefabs[objectType];

            for (int i = 0; i < this.maxNumberOfObjectByType[objectType]; i++)
            {
                (Vector3 position, int rotation) = availablePositionsCopy[Random.Range(0, availablePositionsCopy.Count)];

                GameObject createdGameObject = Instantiate(prefab, position, prefab.transform.rotation);
                createdGameObject.transform.Rotate(new Vector3(0, 0, rotation));

                availablePositionsCopy.Remove((position, rotation));
            }
        }
    }
}