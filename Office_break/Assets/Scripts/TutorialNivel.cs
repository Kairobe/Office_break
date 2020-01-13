using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialNivel : MonoBehaviour
{


    private string textoEscrito;
    [SerializeField]
    public Text TextoTutorial;

    private int faseDelTutorial;

    private Vector3 posJugador;

    private Dictionary<string, GameObject> gameObjectPrefabs;

    private Dictionary<string, int> maxNumberOfObjectByType, collectedObjectNumber;

    [SerializeField]
    private GameObject clipPrefab, maletinPrefab, extintorPrefab;

    GameObject player;
    [SerializeField]
    private int clipMaxNumber, maletinMaxNumber, extintorMaxNumber = 0;

    public List<(Vector3 position, int rotation)> availablePositions;

    private ControladorUi controladorUi;

    public GameObject[] currentLevelCheckPoints;

    private int activeCheckPoint = 0;

    private int lapNumber = 0;

    [SerializeField]
    private int raceLapsNumber;
    
    public BossController bo;

    private Vector3 posMal = new Vector3(17f, 0.09f, -20f);

    private bool MaletinPillado = false;


    // Start is called before the first frame update
    void Start()
    {
        bo = GameObject.FindObjectOfType(typeof(BossController)) as BossController;
        bo.esTutorial();
        // TextoTutorial = this.GetComponent<Text>();
        textoEscrito = "Bienvenido/a al tutorial!";
        textoEscrito = textoEscrito + "\n (Pulsa cualquier tecla para seguir.)";
        faseDelTutorial = 0;
        TextoTutorial.text = textoEscrito;

        player = GameObject.FindWithTag("Player");

        //Instantiate(maletinPrefab, posMal, Quaternion.identity);

        //InitializePositionsAndRotation();

        InitializeObjectsToCollect();

        //InitializeMaximumNumberOfInstancesByGameObject();

        //InitializeGameObjectPrefabs();

        //InitializeObjects();

        //this.controladorUi.UpdateLapCounter(this.lapNumber, this.raceLapsNumber);
    }

    private void Awake()
    {
       /* GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        this.currentLevelCheckPoints = new GameObject[checkpoints.Length];

        this.currentLevelCheckPoints[0] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint0");
        this.currentLevelCheckPoints[1] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint1");
        this.currentLevelCheckPoints[2] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint2");
        this.currentLevelCheckPoints[3] = checkpoints.FirstOrDefault(cp => cp.name == "CheckPoint3");

        for (int i = 1; i < this.currentLevelCheckPoints.Length; i++)
        {
            this.currentLevelCheckPoints[i].SetActive(false);
        }

        this.controladorUi = GameObject.FindGameObjectWithTag("PropiedadesUi").GetComponent<ControladorUi>();*/
    }

    // Update is called once per frame
    void Update()
    {
        if(faseDelTutorial == 0)
        {
            StartCoroutine(EsperaTutorial());

            posJugador = player.transform.position;
            //Debug.Log(posJugador);
            if (Input.anyKey)
            {
                faseDelTutorial = 1;
                textoEscrito = "Bien hecho! \n Ahora prueba a moverte. ";
                TextoTutorial.text = textoEscrito;
                StartCoroutine(EsperaTutorial());
                
            }
        }
        Vector3 posactual = player.transform.position;
        //Debug.Log(posactual);
        if (faseDelTutorial == 1)
        {
            //Vector3 posactual = player.transform.position;
            if (Mathf.Abs(posactual[0]-posJugador[0]) > 2 ||  Mathf.Abs(posactual[2]-posJugador[2]) > 2){
                faseDelTutorial = 2;
                textoEscrito = "Te has movido!";
                TextoTutorial.text = textoEscrito;
                StartCoroutine(EsperaTutorial());

            }
        }
        if (faseDelTutorial == 3)
        {

            //StartCoroutine(EsperaTutorial());
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(EsperaTutorial());
                textoEscrito = "Puede que encuentres clips o maletines mientras te mueves. \n Deberías recogerlos. (Sirven para crear armas.) \n ¡Prueba a coger el maletín!";
                TextoTutorial.text = textoEscrito;
                Instantiate(maletinPrefab, posMal, Quaternion.identity);
                //faseDelTutorial = 4;
               
            }
           
            
        }
        if (faseDelTutorial == 4)
        {
            //StartCoroutine(EsperaTutorial());
            //StartCoroutine(EsperaTutorial());
            if (MaletinPillado)
            {
                
                textoEscrito = "¡Bien hecho! \n \n Además, de ese ascensor puede salir  jefe. (Aunque ahora no)  \n \n ";
                TextoTutorial.text = textoEscrito;
                StartCoroutine(EsperaTutorial());
                //StartCoroutine(EsperaTutorial());
                
                //faseDelTutorial = 5;
            }
        }
        if (faseDelTutorial == 5)
        {
            
            

                textoEscrito = "Para superar a tus enemigos, puedes dispararles. \n Prueba a pulsar espacio.";
                TextoTutorial.text = textoEscrito;
                StartCoroutine(EsperaTutorial());
                //Debug.Log(faseDelTutorial);
            //faseDelTutorial = 6;

        }
        if (faseDelTutorial == 6)
        {
            //StartCoroutine(EsperaTutorial());
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) )
            {

                textoEscrito = "Bien hecho! \n En tus aventuras verás objetos que no son clips. Te darán ventajas útiles. \n \n (Pulsa enter para continuar)";
                TextoTutorial.text = textoEscrito;
                StartCoroutine(EsperaTutorial());
                //faseDelTutorial = 7;
            }
        }
        if (faseDelTutorial == 7)
        {
            //StartCoroutine(EsperaTutorial());
            if (Input.GetKeyDown(KeyCode.Return))
            {

                textoEscrito = "También puedes acceder al menú de creación de objetos al final de nivel. \n \n (Pulsa enter para continuar)";
                TextoTutorial.text = textoEscrito;
                StartCoroutine(EsperaTutorial());
                //faseDelTutorial = 8;
            }
        }
        if (faseDelTutorial == 8)
        {
            //StartCoroutine(EsperaTutorial());
            if (Input.GetKeyDown(KeyCode.Return))
            {

                textoEscrito = "Y con esto ya puedes empezar a jugar. Pulsa enter para ir al menú principal :)";
                TextoTutorial.text = textoEscrito;
                StartCoroutine(EsperaTutorial());
                //faseDelTutorial = 9;
            }
        }
        if (faseDelTutorial == 9)
        {
           // StartCoroutine(EsperaTutorial());
            if (Input.GetKeyDown(KeyCode.Return))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);

            }
        }
    }

    private IEnumerator EsperaTutorial()
    {

        if (faseDelTutorial == 1)
        {
            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(1.5f);
            textoEscrito = "Pulsa a,s,d,w o las flechas para moverte.";
            TextoTutorial.text = textoEscrito;
        }
        else if (faseDelTutorial == 2)
        {

            yield return new WaitForSeconds(1);
            textoEscrito = "Necesitas café para moverte. \n Más adelante hay una cafetera. Si te detienes ante ella, recargarás café. \n \n (Pulsa enter para continuar)";
            TextoTutorial.text = textoEscrito;
            faseDelTutorial = 3;
        }
        else if (faseDelTutorial == 0)
        {
            yield return new WaitForSeconds(2);
        }
        else if (faseDelTutorial == 4)
        {
            yield return new WaitForSeconds(1);
            faseDelTutorial = 5;
        }
        else if (faseDelTutorial == 5)
        {
            yield return new WaitForSeconds(1);
            faseDelTutorial = 6;
        }
        else
        {
            yield return new WaitForSeconds(1);
            faseDelTutorial = faseDelTutorial + 1;
        }
        
           
        }

    private void TutorialTexto()
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


    public void pillarMaletin()
    {
        MaletinPillado = true;
    }
       
}