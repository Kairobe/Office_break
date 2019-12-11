using UnityEngine;
using UnityEngine.UI;

public class BriefcasesManager : MonoBehaviour
{
    private int currentPosition = 1;
    private int collectedBriefcases = 0;

    [SerializeField]
    private GameObject chooseBriefcaseContainerGameObject, openBriefcaseContainerGameObject;

    [SerializeField]
    private GameObject leftButtonGameObject, rightButtonGameObject, openBriefcaseGameObject;

    [SerializeField]
    private GameObject leftPanelGameObject, rightPanelGameObject, briefcaseIndexGameObject;

    private Button leftButton, rightButton, openBriefcaseButton;
    private Text briefcaseIndexText;

    // Start is called before the first frame update
    void Start()
    {
        this.ShowMenu(CurrentLevelController.CurrentLevelData.collectedBriefcases);
        this.UpdateBriefcaseIndexText();
    }

    private void Awake()
    {
        this.leftButton = this.leftButtonGameObject.GetComponentInChildren<Button>();
        this.rightButton = this.rightButtonGameObject.GetComponentInChildren<Button>();
        this.openBriefcaseButton = this.openBriefcaseGameObject.GetComponentInChildren<Button>();
        this.briefcaseIndexText = this.briefcaseIndexGameObject.GetComponentInChildren<Text>();

        this.leftButton.onClick.AddListener(() => LeftButtonClicked());
        this.rightButton.onClick.AddListener(() => RightButtonClicked());
        this.openBriefcaseButton.onClick.AddListener(() => OpenBriefcaseButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ShowMenu(int collectedBriefcases)
    {
        this.leftPanelGameObject.SetActive(false);
        this.rightPanelGameObject.SetActive(false);

        this.collectedBriefcases = collectedBriefcases;

        if (this.collectedBriefcases > 1)
        {
            this.rightPanelGameObject.SetActive(true);
        }
    }

    private void LeftButtonClicked()
    {
        if (this.collectedBriefcases == 0 || this.currentPosition == 1)
        {
            return;
        }

        this.currentPosition--;

        if (this.currentPosition == 1)
        {
            this.leftPanelGameObject.SetActive(false);
        }
        else if (this.currentPosition != 1)
        {
            this.leftPanelGameObject.SetActive(true);
        }

        this.rightPanelGameObject.SetActive(true);
        this.UpdateBriefcaseIndexText();
    }

    private void RightButtonClicked()
    {
        if (this.collectedBriefcases == 0 || this.currentPosition == collectedBriefcases)
        {
            return;
        }

        this.currentPosition++;

        if (this.currentPosition == collectedBriefcases)
        {
            this.rightPanelGameObject.SetActive(false);
        }
        else if (this.currentPosition < collectedBriefcases)
        {
            this.rightPanelGameObject.SetActive(true);
        }

        this.leftPanelGameObject.SetActive(true);
        this.UpdateBriefcaseIndexText();
    }

    private void OpenBriefcaseButtonClicked()
    {
        this.chooseBriefcaseContainerGameObject.SetActive(false);
        this.openBriefcaseContainerGameObject.SetActive(true);
    }

    private void UpdateBriefcaseIndexText()
    {
        this.briefcaseIndexText.text = $"Maletin {this.currentPosition} de {this.collectedBriefcases}";
    }

    private void GenerateRandomObjectCollection()
    {
    }
}