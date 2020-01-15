using UnityEngine;
using UnityEngine.UI;

public class BriefcasesManager : MonoBehaviour
{
    [SerializeField]
    private GameObject openBriefcaseContainerGameObject;

    [SerializeField]
    private GameObject standardBriefcase, silverBriefcase, goldenBriefcase;

    [SerializeField]
    private GameObject acceptButtonGameObject, openBriefcaseButtonGameObject;

    [SerializeField]
    private GameObject obtainedItemsTextGameObject, obtainedBriefcaseTypeTextGameObject;

    private BriefcaseType obtainedBriefcaseType;

    // Start is called before the first frame update
    void Start()
    {
        this.ShowMenu(CurrentLevelController.CurrentLevelData.collectedBriefcases);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowMenu(int collectedBriefcases)
    {
        string obtainedBriefcaseTypeToSpanish = string.Empty;

        if (collectedBriefcases > 0)
        {
            if (collectedBriefcases < 2)
            {
                this.obtainedBriefcaseType = BriefcaseType.Standard;

                this.standardBriefcase.SetActive(true);
                this.silverBriefcase.SetActive(false);
                this.goldenBriefcase.SetActive(false);

                obtainedBriefcaseTypeToSpanish = "normal";
            }
            else if (collectedBriefcases < 5)
            {
                this.obtainedBriefcaseType = BriefcaseType.Silver;

                this.standardBriefcase.SetActive(false);
                this.silverBriefcase.SetActive(true);
                this.goldenBriefcase.SetActive(false);

                obtainedBriefcaseTypeToSpanish = "de plata";
            }
            else
            {
                this.obtainedBriefcaseType = BriefcaseType.Gold;

                this.standardBriefcase.SetActive(false);
                this.silverBriefcase.SetActive(false);
                this.goldenBriefcase.SetActive(true);

                obtainedBriefcaseTypeToSpanish = "de oro";
            }

            this.obtainedBriefcaseTypeTextGameObject.GetComponent<Text>().text = $"Has coleccionado {collectedBriefcases} maletines, lo que corresponde a un maletin {obtainedBriefcaseTypeToSpanish}!";
            this.openBriefcaseButtonGameObject.GetComponent<Button>().onClick.AddListener(() => OpenBriefcaseButtonClicked());
        }
        else
        {
            this.obtainedBriefcaseTypeTextGameObject.GetComponent<Text>().text = "No has obtenido ningún maletín...";

            this.openBriefcaseButtonGameObject.SetActive(false);
            this.acceptButtonGameObject.SetActive(true);
        }
    }

    private void OpenBriefcaseButtonClicked()
    {
        this.GenerateRandomObjectCollection();
    }

    private void GenerateRandomObjectCollection()
    {
        int numberOfItemsToGenerate = 0;

        switch (this.obtainedBriefcaseType)
        {
            case BriefcaseType.Standard:
                numberOfItemsToGenerate = UnityEngine.Random.Range(3, 6);

                break;
            case BriefcaseType.Silver:
                numberOfItemsToGenerate = UnityEngine.Random.Range(6, 12);

                break;
            case BriefcaseType.Gold:
                numberOfItemsToGenerate = UnityEngine.Random.Range(12, 20);

                break;
            default:
                break;
        }

        this.obtainedItemsTextGameObject.GetComponent<Text>().text = $"Enhorabuena! Has obtenido {numberOfItemsToGenerate} clips!";

        UserData currentUserData = DataManager.LoadData("admin");
        currentUserData.Clips += numberOfItemsToGenerate;

        DataManager.SaveData(currentUserData);
    }
}