using GodotGame.PlayerBehaviour.InventorySystem;
using System;

[Serializable]
public struct GameSave
{
    public string playerName;

    //public bool isMale;

    public int lastSavePoint;

    #region npcdata

    public int Frenk;

    public int Wolf;
    //...

    #endregion

    public Inventory inventory;

    public string[] currentEvents;

    static public GameSave Empty => new GameSave
    {
        playerName = string.Empty,
        //isMale = false,

        Frenk = 0,


        inventory = Inventory.Empty,

        currentEvents = new string[] { }
    };
}
