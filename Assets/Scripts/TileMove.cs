using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileMove : MonoBehaviour
{
    public Transform duck;
    public Tilemap tilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3Int cellPos = tilemap.WorldToCell(mousePos);
        Vector2 pos = tilemap.GetCellCenterLocal(cellPos);

        duck.position = pos;
    }
}
