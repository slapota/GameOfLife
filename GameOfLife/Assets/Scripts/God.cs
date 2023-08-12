using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class God : MonoBehaviour
{
    public Tile tile;
    public Transform grid;
    public int scale;
    public int size;
    public Tile[,] tiles = null;

    private void Start()
    {
        tiles = new Tile[scale, scale];
        //Load();
        StartCoroutine(Spawn());
    }
    void Load()
    {
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            for (int j = 0; j < tiles.GetLength(0); j++)
            {
                Tile newTile = Instantiate(tile, grid);
                newTile.transform.localPosition = new Vector2((j * size)-size*scale/2, (i * size)-size*scale/2);
                newTile.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
                newTile.GetComponent<Image>().color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 100);
                newTile.alive = false;
                tiles[j, i] = newTile;
            }
        }
    }
    IEnumerator Spawn()
    {
        yield return new WaitUntil(()=>Input.GetMouseButtonDown(0));
        Vector2 pos = transform.InverseTransformPoint(Input.mousePosition);
        pos = new Vector2(((int)Mathf.Round(pos.x / size)) * size, ((int)Mathf.Round(pos.y / size)) * size);
        Tile newTile = Instantiate(tile, grid);
        newTile.GetComponent<RectTransform>().sizeDelta = new Vector2(size, size);
        newTile.transform.localPosition = pos;
        newTile.god = this;
        try
        {
            Vector2 slot = new Vector2((pos.x / size) + (scale / 2), (pos.y / size) + (scale / 2));
            if (tiles[(int)slot.x, (int)slot.y] != null)
            {
                Destroy(newTile.gameObject);
            }
            else
            {
                tiles[(int)slot.x, (int)slot.y] = newTile;
                Debug.Log(slot);
            }
        }
        catch
        {
            Destroy(newTile.gameObject);
            Debug.Log("poser cik�n");
        }
        yield return new WaitForSecondsRealtime(0.1f);
        StartCoroutine(Spawn());
    }
}
