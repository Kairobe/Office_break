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
        ShowWeaponBuyStatus(selectedWeapon);
    }

    private void BuyButtonClicked()
    {
        IEnumerable<Text> texts = this.gameObject.GetComponentsInChildren<Text>();

        string weaponName = texts.FirstOrDefault(t => t.name == "NombreArma").text;

        WeaponRequirements weaponRequirements = this.availableWeapons[weaponName];

        UserData existingUserData = DataManager.LoadData("admin");

        existingUserData.BoughtWeapons.Add(weaponName);

        DataManager.SaveData(existingUserData);

        ShowWeaponBuyStatus(weaponName);
    }

    private void InitializeAvailableWeapons()
    {
        WeaponRequirements tirachinas = new WeaponRequirements
        {
            NeededClips = 0,
        };

        WeaponRequirements regla = new WeaponRequirements
        {
            NeededClips = 15,
        };

        WeaponRequirements ballesta = new WeaponRequirements
        {
            NeededClips = 30,
        };

        availableWeapons = new Dictionary<string, WeaponRequirements>
        {
            { "Tirachinas", tirachinas },
            { "Regla", regla },
            { "Ballesta", ballesta }
        };
    }

    /// <summary> Shows the status of the given weapon. </summary>
    /// <param name="selectedWeapon"> The weapon to show the status. </param>
    private void ShowWeaponBuyStatus(string selectedWeapon)
    {
        bool weaponAlreadyBought = DataManager.LoadData("admin").BoughtWeapons.Contains(selectedWeapon);

        if (weaponAlreadyBought)
        {
            this.weaponAlreadyBoughtGameObject.SetActive(true);
            this.buyGameObject.SetActive(false);
        }
        else
        {
            this.weaponAlreadyBoughtGameObject.SetActive(false);
            this.buyGameObject.SetActive(true);

            WeaponRequirements weaponRequirementes = this.availableWeapons[selectedWeapon];
            IEnumerable<Text> texts = this.gameObject.GetComponentsInChildren<Text>();

            Text clipsText = texts.FirstOrDefault(t => t.gameObject.name.Equals("NeededClips"));

            clipsText.text = $"Clips: x{weaponRequirementes.NeededClips}";

            ManageWeaponRequirementsRepresentation(weaponRequirementes, clipsText);
        }
    }

    private void ManageWeaponRequirementsRepresentation(WeaponRequirements weaponRequirementes, Text clipsText)
    {
        UserData userData = DataManager.LoadData("admin");
        bool canBuyWeapon = true;

        if (userData.Clips < weaponRequirementes.NeededClips)
        {
            canBuyWeapon = false;
            clipsText.color = Color.red;
        }
        else
        {
            clipsText.color = Color.black;
        }

        buyButton.gameObject.SetActive(canBuyWeapon);
    }
}