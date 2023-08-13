using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Tile : MonoBehaviour
{
    public bool alive;
    public God god;
    public int x, y;

    private void Start()
    {
        StartCoroutine(Tick());
    }
    IEnumerator Tick()
    {
        yield return new WaitForSeconds(0.3f);
        //yield return new WaitUntil(()=>Input.GetKeyDown(KeyCode.Space));
        Cycle();
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(Tick());
    }
    void Cycle()
    {
        if(GetComponent<Image>().color == Color.yellow && Random.Range(0, 3)==0)
        {
            Boom();
        }
        switch(GetNeighbours())
        {
            case 0:
                switch(Random.Range(0, 10))
                {
                    case 0:
                    case 1:
                        Reproduce();
                        break;
                    case 2:
                        if (GetComponent<Image>().color == Color.green)
                        {
                            GetComponent<Image>().color = Color.red;
                        }
                        else
                        {
                            Destroy(gameObject);
                        }
                        break;
                }
                break;
            case 1:
                if (Random.Range(0, 2) == 0)
                {
                    Reproduce();
                }
                break;
            case 2:
                if(Random.Range(0, 3) == 0)
                {
                    Reproduce();
                }
                break;
            case 3:
                GetComponent<Image>().color = Color.green;
                break;
            case 5:
                GetComponent<Image>().color = Color.yellow;
                break;
            case 4:
            case 6:
            case 7:
            case 8:
                if(GetComponent<Image>().color == Color.green)
                {
                    GetComponent<Image>().color = Color.red;
                }
                else
                {
                    Destroy(gameObject);
                }
                break;
        }
    }
    byte GetNeighbours()
    {
        byte neighbours = 0;
        for (int i = x - 1; i < x + 2; i++)
        {
            if (i < 0 || i >= god.tiles.GetLength(0)) continue;
            for (int j = y - 1; j < y + 2; j++)
            {
                if ((i == x && j == y) || j < 0 || j >= god.tiles.GetLength(1)) continue;
                if (god.tiles[i, j] != null) neighbours++;
            }
        }
        return neighbours;
    }
    void Reproduce()
    {
        Vector2 slot = new Vector2(x + Random.Range(-1, 2), y + Random.Range(-1, 2));
        god.Spawn(new Vector2((slot.x * god.size) - god.size * god.scale / 2, (slot.y * god.size) - god.size * god.scale / 2), slot);
    }
    void Boom()
    {
        for (int i = x - 3; i < x + 4; i++)
        {
            if (i < 0 || i >= god.tiles.GetLength(0)) continue;
            for (int j = y - 3; j < y + 4; j++)
            {
                if ((i == x && j == y) || j < 0 || j >= god.tiles.GetLength(1)) continue;
                if (god.tiles[i, j] != null)
                {
                    Destroy(god.tiles[i, j].gameObject);
                }
            }
        }
        Destroy(gameObject);
    }
}
