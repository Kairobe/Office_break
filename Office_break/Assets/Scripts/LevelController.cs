using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private GameObject clipPrefab, briefcasePrefab, extinguisherPrefab;

    [SerializeField]
    private GameObject[] playerGameObjects;

    [SerializeField]
    private int clipMaxNumber, maletinMaxNumber, extintorMaxNumber = 0;

    [SerializeField]
    private GameObject[] currentLevelCheckPoints;

    [SerializeField]
    private int raceLapsNumber;

    private Dictionary<string, GameObject> gameObjectPrefabsDictionary;

    private Dictionary<string, int> maxNumberOfObjectByType, collectedObjectNumberDictionary;

    private GameObject player;

    private List<(Vector3 position, int rotation)> availablePositions;

    private CommonUIController controladorUi;

    private Dictionary<GameObject, float> currentLevelLightsDictionary;

    private int activeCheckPoint = 0, lapNumber = 0, playerMinimunPosition = 1, playerPositionInRace = 1;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        InitializePositionsAndRotation();

        InitializeObjectsToCollect();

        InitializeMaximumNumberOfInstancesByGameObject();

        InitializeGameObjectPrefabs();

        InitializeObjects();

        this.controladorUi.UpdateLapCounter(this.lapNumber, this.raceLapsNumber);
    }

    /// <summary> Called when the script instance is being loaded. </summary>
    private void Awake()
    {
        this.player = this.playerGameObjects.FirstOrDefault(pgo => pgo.CompareTag("Player"));

        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        this.currentLevelCheckPoints = new GameObject[checkpoints.Length];

        this.currentLevelLightsDictionary = GameObject.FindGameObjectsWithTag("InnerLight").ToDictionary(k => k, v => v.GetComponent<Light>().intensity);

        this.currentLevelCheckPoints[0] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint0");
        this.currentLevelCheckPoints[1] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint1");
        this.currentLevelCheckPoints[2] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint2");
        this.currentLevelCheckPoints[3] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint3");

        for (int i = 1; i < this.currentLevelCheckPoints.Length; i++)
        {
            this.currentLevelCheckPoints[i].SetActive(false);
        }

        this.controladorUi = GameObject.FindGameObjectWithTag("PropiedadesUi").GetComponent<CommonUIController>();
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        (int playerPosition, int totalPlayers) = this.GetPlayerPositionInGame();

        this.playerPositionInRace = playerPosition;

        this.controladorUi.UpdatePlayerPositionInRace(playerPosition, totalPlayers);

        ManageInnerLights();
    }

    /// <summary>
    /// Increases the number of collected objects of the given type in the given units.
    /// </summary>
    /// <param name="objectType"> The type of the object to increase the collected number. </param>
    /// <param name="units">
    /// (Optional) The total ammount of units to increase. By default it is set to one unit.
    /// </param>
    public void IncreaseCollectedNumber(string objectType, int? units = null)
    {
        int unitsToIncrease = units ?? 1;

        this.collectedObjectNumberDictionary[objectType] += unitsToIncrease;

        this.controladorUi.UpdateObjectCount(objectType, this.collectedObjectNumberDictionary[objectType]);
    }

    /// <summary>
    /// Activates the next <see cref="CheckpointController"/> depending on the passed Checkpoint
    /// number and the <see cref="GameObject"/> that has crossed that Checkpoint.
    /// </summary>
    /// <param name="passedCheckPointNumber"> The number of the passed <see cref="CheckpointController"/>. </param>
    /// <param name="passingGameObject"> The <see cref="GameObject"/> that has crossed that checkpoint. </param>
    public void ActivateNextCheckPoint(int passedCheckPointNumber, GameObject passingGameObject)
    {
        if (passedCheckPointNumber != this.activeCheckPoint)
        {
            return;
        }

        if (passedCheckPointNumber == 0)
        {
            if (this.lapNumber == this.raceLapsNumber)
            {
                if (!passingGameObject.CompareTag("Player"))
                {
                    passingGameObject.GetComponent<DirectedAgent>().StopAgent();
                    this.playerMinimunPosition++;

                    return;
                }

                LevelData currentLevelData = new LevelData("admin", this.collectedObjectNumberDictionary["Clip"], this.collectedObjectNumberDictionary["Briefcase"], this.playerPositionInRace);
                CurrentLevelController.CurrentLevelData = currentLevelData;
                UpdateCurrentUserData(currentLevelData);

                //Para que vaya a la pantalla de fin de nivel:
                SceneManager.LoadScene(SceneNames.EndOfLevel);

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

    /// <summary>
    /// Manages the inner lights, turning off those that are out of range according to the current
    /// position of the player.
    /// </summary>
    private void ManageInnerLights()
    {
        foreach (GameObject lightGameObject in currentLevelLightsDictionary.Keys)
        {
            Light lightComponent = lightGameObject.GetComponent<Light>();

            if (Vector3.Distance(lightGameObject.transform.position, this.player.transform.position) > 15)
            {
                lightComponent.intensity = 0;
            }
            else
            {
                lightComponent.intensity = currentLevelLightsDictionary[lightGameObject];
            }
        }
    }

    /// <summary>
    /// Gets the position of the player in the current race and the total players in the race.
    /// </summary>
    /// <returns>
    /// The position of the player in the current race and the total players in the race.
    /// </returns>
    private (int playerPosition, int totalPlayers) GetPlayerPositionInGame()
    {
        if (this.playerMinimunPosition > 1)
        {
            return (this.playerMinimunPosition, this.playerGameObjects.Length);
        }

        List<(float distance, bool isPlayer)> distancesToNextCheckpointTuple = new List<(float distance, bool isPlayer)>();

        foreach (GameObject playerGameObject in playerGameObjects)
        {
            var distance = Vector3.Distance(this.currentLevelCheckPoints[this.activeCheckPoint].transform.position, playerGameObject.transform.position);

            bool isPlayer = false;

            if (playerGameObject.CompareTag("Player"))
            {
                isPlayer = true;
            }

            distancesToNextCheckpointTuple.Add((distance, isPlayer));
        }

        return (distancesToNextCheckpointTuple.OrderBy(d => d.distance).Select(d => d.isPlayer).ToList().FindIndex(b => b == true) + 1, this.playerGameObjects.Length);
    }

    /// <summary>
    /// Initializes the manixum number of instances that must be generated of each Game Object type.
    /// </summary>
    private void InitializeMaximumNumberOfInstancesByGameObject()
    {
        this.maxNumberOfObjectByType = new Dictionary<string, int>
        {
            { nameof(Clip), clipMaxNumber },
            { nameof(Briefcase), maletinMaxNumber },
            { nameof(Extinguisher), extintorMaxNumber }
        };
    }

    /// <summary> Initializes the prefabs of the different Game Objects to be created. </summary>
    private void InitializeGameObjectPrefabs()
    {
        this.gameObjectPrefabsDictionary = new Dictionary<string, GameObject>
        {
            { nameof(Clip), clipPrefab },
            { nameof(Briefcase), briefcasePrefab },
            { nameof(Extinguisher), extinguisherPrefab }
        };
    }

    /// <summary> Initializes the collectable Game Objects. </summary>
    private void InitializeObjectsToCollect()
    {
        this.collectedObjectNumberDictionary = new Dictionary<string, int>
        {
            { nameof(Clip), 0 },
            { nameof(Briefcase), 0 }
        };
    }

    /// <summary> Updates the data associated with the current user. </summary>
    /// <param name="currentLevelData"> The updated user data. </param>
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
            GameObject prefab = this.gameObjectPrefabsDictionary[objectType];

            for (int i = 0; i < this.maxNumberOfObjectByType[objectType]; i++)
            {
                (Vector3 position, int rotation) = availablePositionsCopy[Random.Range(0, availablePositionsCopy.Count)];

                GameObject createdGameObject = Instantiate(prefab, position, prefab.transform.rotation);
                createdGameObject.transform.Rotate(new Vector3(0, 0, rotation));

                availablePositionsCopy.Remove((position, rotation));
            }
        }
    }

    /// <summary>
    /// Initializes the available positions and the rotation that must be applied to the created
    /// Game Objects.
    /// </summary>
    private void InitializePositionsAndRotation()
    {
        availablePositions = new List<(Vector3 position, int rotation)>();

        string currentSceneName = SceneManager.GetActiveScene().name;

        switch (currentSceneName)
        {
            case "OficinaFix":
                InitializeOfficePositions();

                break;
            case "Cafeteria":
                InitializeCoffeeShopPositions();

                break;
            case "Jardin":
                InitializeGardenPositions();

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Initializes the available positions for spawning objects in the Coffee Shop scene.
    /// </summary>
    private void InitializeCoffeeShopPositions()
    {
        availablePositions.Add((new Vector3(9.5f, 0.5f, -1), 0));
        availablePositions.Add((new Vector3(5f, 0.5f, -9.5f), 90));
        availablePositions.Add((new Vector3(4, 0.5f, -15.5f), 90));
        availablePositions.Add((new Vector3(9.5f, 0.5f, -19f), 90));
        availablePositions.Add((new Vector3(14, 0.5f, -18.5f), 90));
        availablePositions.Add((new Vector3(19, 0.5f, -18f), 90));
        availablePositions.Add((new Vector3(23, 0.5f, -12.5f), 0));
        availablePositions.Add((new Vector3(23.5f, 0.5f, -6.5f), 0));
        availablePositions.Add((new Vector3(24, 0.5f, 1.5f), 0));
        availablePositions.Add((new Vector3(20, 0.5f, 6.5f), 90));
    }

    /// <summary> Initializes the available positions for spawning objects in the Office scene. </summary>
    private void InitializeOfficePositions()
    {
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

    /// <summary> Initializes the available positions for spawning objects in the Garden scene. </summary>
    private void InitializeGardenPositions()
    {
        availablePositions.Add((new Vector3(10f, 0.5f, -23), 90));
        availablePositions.Add((new Vector3(15f, 0.5f, -23), 90));
        availablePositions.Add((new Vector3(19.5f, 0.5f, -15f), 0));
        availablePositions.Add((new Vector3(15.5f, 0.5f, -12.5f), 90));
        availablePositions.Add((new Vector3(10.5f, 0.5f, -16f), 0));
        availablePositions.Add((new Vector3(5f, 0.5f, -16f), 0));
        availablePositions.Add((new Vector3(-5f, 0.5f, -5f), 0));
        availablePositions.Add((new Vector3(14f, 0.5f, -3.5f), 90));
        availablePositions.Add((new Vector3(14f, 0.5f, -0.5f), 90));
        availablePositions.Add((new Vector3(0.8f, 0.5f, -2), 0));
        availablePositions.Add((new Vector3(0.8f, 0.5f, -11), 0));
    }
}