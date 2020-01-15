using System;
using System.Collections.Generic;

[Serializable]
public class UserData
{
    public string UserAlias;
    public int Clips, Erasers, Pencils, Rulers, HairGums, StapleBoxes, Staplers, Docks, WaterBottles, WateryCoffees = 0;
    public List<string> BoughtWeapons;

    /// <summary> Initializes a new instance of the <see cref="UserData"/> class. </summary>
    /// <param name="userAlias"> The alias of the related user. </param>
    public UserData(string userAlias)
    {
        this.UserAlias = userAlias;
        this.BoughtWeapons = new List<string>();
    }

    /// <summary> Updates the user data with the current <see cref="LevelData"/>. </summary>
    /// <param name="currentLevelData"> The information about the current Level. </param>
    public void UpdateUserData(LevelData currentLevelData)
    {
        this.Clips += currentLevelData.collectedClips;

        DataManager.SaveData(this);
    }
}