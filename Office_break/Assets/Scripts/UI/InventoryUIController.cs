using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField]
    private Button slingshotButton, rulerButton, crossbowButton, buyButton;

    [SerializeField]
    private GameObject buyGameObject, weaponAlreadyBoughtGameObject;

    private Dictionary<string, WeaponRequirements> availableWeapons;

    /// <summary> Called before the first frame update. </summary>
    private void Start()
    {
        InitializeAvailableWeapons();

        slingshotButton.onClick.AddListener(() => WeaponButtonClicked("Tirachinas"));
        crossbowButton.onClick.AddListener(() => WeaponButtonClicked("Ballesta"));
        rulerButton.onClick.AddListener(() => WeaponButtonClicked("Regla"));

        buyButton.onClick.AddListener(BuyButtonClicked);
    }

    /// <summary> The listener associated with the click to the button associated to a Weapon. </summary>
    /// <param name="selectedWeapon"> The name of the selected weapon. </param>
    private void WeaponButtonClicked(string selectedWeapon)
    {
        ShowWeaponBuyStatus(selectedWeapon);
    }

    /// <summary> The listener associated with the click of the buy weapon button. </summary>
    private void BuyButtonClicked()
    {
        IEnumerable<Text> texts = this.gameObject.GetComponentsInChildren<Text>();

        string weaponName = texts.FirstOrDefault(t => t.name.Equals("WeaponName", System.StringComparison.OrdinalIgnoreCase)).text;

        WeaponRequirements weaponRequirements = this.availableWeapons[weaponName];

        UserData existingUserData = DataManager.LoadData("admin");

        existingUserData.BoughtWeapons.Add(weaponName);

        DataManager.SaveData(existingUserData);

        ShowWeaponBuyStatus(weaponName);
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

    /// <summary> Manages the representation of the requirements according to the given requirements. </summary>
    /// <param name="weaponRequirementes"> The given <see cref="WeaponRequirements"/>. </param>
    /// <param name="clipsText"> The text for showing the necessary clips. </param>
    private void ManageWeaponRequirementsRepresentation(WeaponRequirements weaponRequirementes, Text clipsText)
    {
        UserData userData = DataManager.LoadData("admin");
        bool canBuyWeapon = true;

        if (userData.Clips < weaponRequirementes.NeededClips)
        {
            canBuyWeapon = false;

            // Indicates that the user can not buy the weapon.
            clipsText.color = Color.red;
        }
        else
        {
            clipsText.color = Color.black;
        }

        buyButton.gameObject.SetActive(canBuyWeapon);
    }

    /// <summary> Initializes all the available weapons. </summary>
    private void InitializeAvailableWeapons()
    {
        WeaponRequirements slingshot = new WeaponRequirements
        {
            NeededClips = 0,
        };

        WeaponRequirements ruler = new WeaponRequirements
        {
            NeededClips = 15,
        };

        WeaponRequirements crossbow = new WeaponRequirements
        {
            NeededClips = 30,
        };

        availableWeapons = new Dictionary<string, WeaponRequirements>
        {
            { "Tirachinas", slingshot },
            { "Regla", ruler },
            { "Ballesta", crossbow }
        };
    }
}