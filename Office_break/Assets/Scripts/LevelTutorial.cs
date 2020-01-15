using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelTutorial : MonoBehaviour
{
    [SerializeField]
    public Text TextoTutorial;

    [SerializeField]
    private GameObject clipPrefab, maletinPrefab, extintorPrefab;

    [SerializeField]
    private int clipMaxNumber, maletinMaxNumber, extintorMaxNumber = 0;

    [SerializeField]
    private int raceLapsNumber;

    private readonly CommonUIController uiController;

    private string writtenText;
    private int tutorialPhase;

    private Dictionary<string, GameObject> gameObjectPrefabs;

    private Dictionary<string, int> maxNumberOfObjectByType, collectedObjectNumber;

    private GameObject player;

    public List<(Vector3 position, int rotation)> availablePositions;

    public GameObject[] currentLevelCheckPoints;

    private int activeCheckPoint = 0, lapNumber = 0;

    public BossController bossController;

    private Vector3 currentPosition;
    private Vector3 playerPosition;
    private Vector3 briefcasePosition = new Vector3(17f, 0.09f, -20f);

    private bool briefcaseHasBeenCollected = false;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        bossController = GameObject.FindObjectOfType(typeof(BossController)) as BossController;
        bossController.ActivateTutorialMode();

        writtenText = "Bienvenido/a al tutorial!";

        tutorialPhase = 0;
        TextoTutorial.text = writtenText;

        player = GameObject.FindWithTag("Player");

        InitializeObjectsToCollect();

        currentPosition = player.transform.position;
    }

    /// <summary> Called once per frame. </summary>
    private void Update()
    {
        switch (tutorialPhase)
        {
            case 1:
                writtenText = "Puedes moverte con las flechas o asdw";
                TextoTutorial.text = writtenText;

                break;
            case 2:
                writtenText = "Puede que encuentres clips o maletines mientras te mueves. \n Deberías recogerlos. \n Sirven para crear armas.";
                TextoTutorial.text = writtenText;

                break;
            case 3:
                Instantiate(maletinPrefab, briefcasePosition, Quaternion.identity);
                tutorialPhase = 4;
                briefcaseHasBeenCollected = false;

                break;
            case 4:
                if (briefcaseHasBeenCollected)
                {
                    writtenText = "¡Bien hecho! \n \n ";
                    TextoTutorial.text = writtenText;
                }
                else
                {
                    writtenText = "¡Intenta coger el maletín!";
                    TextoTutorial.text = writtenText;
                }

                break;
            case 5:

                writtenText = "Para superar a tus enemigos, puedes dispararles. \n Prueba a pulsar espacio.";
                TextoTutorial.text = writtenText;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    tutorialPhase = 6;
                }

                break;
            case 6:
                writtenText = "Bien hecho! \n ";
                TextoTutorial.text = writtenText;

                break;
            case 7:
                writtenText = "Del ascensor puede salir el jefe. \n ¡Que no te alcance!";
                TextoTutorial.text = writtenText;

                break;
            case 8:
                writtenText = "Y con esto ya puedes empezar a jugar. Pulsa -> para ir al menú principal :)";
                TextoTutorial.text = writtenText;

                break;
            case 9:
                SceneManager.LoadScene(SceneNames.MainMenu);

                break;
            default:
                break;
        }
    }

    /// <summary> Collects a <see cref="Briefcase"/>. </summary>
    public void CollectBriefcase()
    {
        briefcaseHasBeenCollected = true;
    }

    /// <summary> The controller associated with the pressing of the left arrow during the tutorial. </summary>
    public void LeftArrow()
    {
        switch (tutorialPhase)
        {
            case 4:
                tutorialPhase = 2;

                break;
            case 7:
                tutorialPhase = 5;

                break;
            default:
                tutorialPhase--;

                break;
        }
    }

    /// <summary>
    /// The controller associated with the pressing of the right arrow during the tutorial.
    /// </summary>
    public void RightArrow()
    {
        tutorialPhase++;
    }

    /// <summary> Initializes the Game Objects that are collectable. </summary>
    private void InitializeObjectsToCollect()
    {
        this.collectedObjectNumber = new Dictionary<string, int>
        {
            { nameof(Clip), 0 },
            { nameof(Briefcase), 0 }
        };
    }
}