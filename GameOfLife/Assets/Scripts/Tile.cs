using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool alive;
    public God god;
    public int x, y;
    byte neighbours;

    private void Start()
    {
        StartCoroutine(Tick());
    }
    IEnumerator Tick()
    {
        yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.Space));
        Cycle();
        StartCoroutine(Tick());
    }
    void Cycle()
    {
        neighbours = 0;
        for (int i = x-1; i < x+2;  i++)
        {
            if (i < 0 || i >= god.tiles.GetLength(0)) continue;
            for (int j = y-1; j < y+2; j++)
            {
                if ((i == x && j == y) || j < 0 || j >= god.tiles.GetLength(1)) continue;
                if (god.tiles[i, j] != null) neighbours++;
            }
        }
        Debug.Log(neighbours);
    }
}
