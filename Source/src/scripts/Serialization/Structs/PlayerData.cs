using GodotGame.PlayerBehaviour.InventorySystem;

public struct PlayerData
{
    public string playerName;

    public bool isMale;

    public Inventory inventory;

    static public PlayerData Empty => new PlayerData
    {
        playerName = string.Empty,
        isMale = false,
        inventory = Inventory.Empty,
    };
}
