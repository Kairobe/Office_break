using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<string, WeaponRequirements> availableWeapons;

    [SerializeField]
    private Button tirachinasButton, reglaButton, ballestaButton, buyButton;

    [SerializeField]
    private GameObject buyGameObject, weaponAlreadyBoughtGameObject;

    // Start is called before the first frame update
    void Start()
    {
        InitializeAvailableWeapons();

        tirachinasButton.onClick.AddListener(() => WeaponButtonClicked("Tirachinas"));
        ballestaButton.onClick.AddListener(() => WeaponButtonClicked("Ballesta"));
        reglaButton.onClick.AddListener(() => WeaponButtonClicked("Regla"));

        buyButton.onClick.AddListener(BuyButtonClicked);
    }

    private void WeaponButtonClicked(string selectedWeapon)
    {
        WeaponRequirements weaponRequirementes = this.availableWeapons[selectedWeapon];
        IEnumerable<Text> texts = this.gameObject.GetComponentsInChildren<Text>();

        UpdateWeaponBuyStatus(selectedWeapon);

        Text clipsText = texts.FirstOrDefault(t => t.name.Equals("NeededClips"));
        Text briefcasesText = texts.FirstOrDefault(t => t.name.Equals("NeededBriefcases"));

        clipsText.text = $"Clips: x{weaponRequirementes.NeededClips}";
        briefcasesText.text = $"Maletines: x{weaponRequirementes.NeededBriefcases}";

        ManageWeaponRequirementsRepresentation(weaponRequirementes, clipsText, briefcasesText);
    }

    private void BuyButtonClicked()
    {
        IEnumerable<Text> texts = this.gameObject.GetComponentsInChildren<Text>();

        string weaponName = texts.FirstOrDefault(t => t.name == "NombreArma").text;

        WeaponRequirements weaponRequirements = this.availableWeapons[weaponName];

        LevelData existingUserData = DataManager.LoadData("admin");

        LevelData userDataChanges = new LevelData("admin", -weaponRequirements.NeededClips, -weaponRequirements.NeededBriefcases)
        {
            boughtWeapons = existingUserData.boughtWeapons
        };
        userDataChanges.boughtWeapons.Add(weaponName);

        DataManager.SaveData(userDataChanges);

        UpdateWeaponBuyStatus(weaponName);
    }

    private void InitializeAvailableWeapons()
    {
        WeaponRequirements tirachinas = new WeaponRequirements
        {
            NeededBriefcases = 0,
            NeededClips = 0,
        };

        WeaponRequirements regla = new WeaponRequirements
        {
            NeededBriefcases = 0,
            NeededClips = 0,
        };

        WeaponRequirements ballesta = new WeaponRequirements
        {
            NeededBriefcases = 5,
            NeededClips = 10,
        };

        availableWeapons = new Dictionary<string, WeaponRequirements>
        {
            { "Tirachinas", tirachinas },
            { "Regla", regla },
            { "Ballesta", ballesta }
        };
    }

    /// <summary> Updates the status of the given weapon. </summary>
    /// <param name="selectedWeapon"> The weapon to update the status. </param>
    private void UpdateWeaponBuyStatus(string selectedWeapon)
    {
        bool weaponAlreadyBought = DataManager.LoadData("admin").boughtWeapons.Contains(selectedWeapon);

        if (weaponAlreadyBought)
        {
            this.weaponAlreadyBoughtGameObject.SetActive(true);
            this.buyGameObject.SetActive(false);
        }
        else
        {
            this.weaponAlreadyBoughtGameObject.SetActive(false);
            this.buyGameObject.SetActive(true);
        }
    }

    private void ManageWeaponRequirementsRepresentation(WeaponRequirements weaponRequirementes, Text clipsText, Text briefcasesText)
    {
        LevelData userData = DataManager.LoadData("admin");
        bool canBuyWeapon = true;

        if (userData.collectedClips < weaponRequirementes.NeededClips)
        {
            canBuyWeapon = false;
            clipsText.color = Color.red;
        }
        else
        {
            clipsText.color = Color.black;
        }

        if (userData.collectedBriefcases < weaponRequirementes.NeededBriefcases)
        {
            canBuyWeapon = false;
            briefcasesText.color = Color.red;
        }
        else
        {
            briefcasesText.color = Color.black;
        }

        buyButton.gameObject.SetActive(canBuyWeapon);
    }
}