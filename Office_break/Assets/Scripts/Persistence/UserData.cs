using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    public string UserAlias;
    public int Clips, Erasers, Pencils, Rulers, HairGums, StapleBoxes, Staplers, Docks, WaterBottles, WateryCoffees = 0;
    public List<string> BoughtWeapons;

    public UserData(string userAlias)
    {
        this.UserAlias = userAlias;
        this.BoughtWeapons = new List<string>();
    }

    public void UpdateUserData(LevelData currentLevelData)
    {
        this.Clips += currentLevelData.collectedClips;

        DataManager.SaveData(this);
    }
}