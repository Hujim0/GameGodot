using GodotGame.PlayerBehaviour.InventorySystem;

public struct GameSave
{
    public string playerName;

    public bool isMale;

    public Inventory inventory;

    public int lastSavePoint;

    static public GameSave Empty => new GameSave
    {
        playerName = string.Empty,
        isMale = false,
        inventory = Inventory.Empty,
    };
}
