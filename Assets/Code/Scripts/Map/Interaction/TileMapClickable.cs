using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapClickable : Clickable
{
    public MapMovement mapMovement;

    void Start()
    {
        mapMovement = GameMap.Movement;
    }

    public override void handle(Vector3 globalPosition, Vector3 mousePosition)
    {
        Moveable userMoveable = GamePlayers.LocalUser.character.moveable;
        if (userMoveable) 
        {
            Tilemap tilemap = mapMovement.tilemap;
            Vector3 hitPoint = globalPosition + tilemap.transform.position/2;

            Vector3Int cellPosition = tilemap.WorldToCell(hitPoint);
            if (mapMovement.CanMove(cellPosition))
            {
                mapMovement.MoveTo(userMoveable, cellPosition);
            }
        }
    }
}
