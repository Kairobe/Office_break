using System;
using System.Collections.Generic;

[Serializable]
public class LevelData
{
    public string playerAlias;
    public int collectedClips, collectedBriefcases;
    public List<string> boughtWeapons;
    public int playerPosition;

    /// <summary> Saves the level data. </summary>
    /// <param name="playerAlias"> The alias of the player associated with the given data. </param>
    /// <param name="collectedClips">
    /// The number of the <see cref="Clip"/> that have been collected in the level.
    /// </param>
    /// <param name="collectedBriefcases">
    /// The number of the <see cref="Maletin"/> that have been collected in the level.
    /// </param>
    /// <param name="playerPosition"> The position of the player in the current race. </param>
    public LevelData(string playerAlias, int collectedClips, int collectedBriefcases, int playerPosition)
    {
        this.playerAlias = playerAlias;
        this.collectedClips = collectedClips;
        this.collectedBriefcases = collectedBriefcases;
        this.boughtWeapons = new List<string>();
        this.playerPosition = playerPosition;
    }
}