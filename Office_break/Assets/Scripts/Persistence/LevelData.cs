using System;

[Serializable]
public class LevelData
{
    public string playerAlias;
    public int collectedClips, collectedBriefcases;

    /// <summary> Saves the level data. </summary>
    /// <param name="playerAlias"> The alias of the player associated with the given data. </param>
    /// <param name="collectedClips">
    /// The number of the <see cref="Clip"/> that have been collected in the level.
    /// </param>
    /// <param name="collectedBriefcases">
    /// The number of the <see cref="Maletin"/> that have been collected in the level.
    /// </param>
    public LevelData(string playerAlias, int collectedClips, int collectedBriefcases)
    {
        this.playerAlias = playerAlias;
        this.collectedClips = collectedClips;
        this.collectedBriefcases = collectedBriefcases;
    }
}