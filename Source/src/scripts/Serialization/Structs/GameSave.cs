using GodotGame.PlayerBehaviour.InventorySystem;
using System;


namespace GodotGame.General
{
[Serializable]
public struct GameSave
{
    public string playerName;

    //public bool isMale;

    public int lastSavePoint;

    public Inventory inventory;

    public string[] currentEvents;

    static public GameSave Empty => new GameSave
    {
        playerName = string.Empty,
        //isMale = false,

        inventory = Inventory.Empty,

        currentEvents = new string[] { }
    };
}

}
