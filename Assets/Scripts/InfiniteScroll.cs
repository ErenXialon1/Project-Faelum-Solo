using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour

{
    public GameObject tilePrefab; // G�r�nt�lenecek karo objelerinin prefab referans�
    public Transform player; // Oyuncunun konumunu temsil eden Transform referans�

    private const int poolSize = 100; // Karo objelerinin havuzunun maksimum boyutu
    private const int gridSize = 20; // Karo objelerinin yar� uzunlu�u
    private Queue<GameObject> tilePool = new Queue<GameObject>(); // Kullan�lmayan karo objelerinin havuzu
    private Dictionary<Vector2, GameObject> tiles = new Dictionary<Vector2, GameObject>(); // G�r�nen karo objelerinin tutuldu�u s�zl�k

    private Vector2 lastPlayerPos; // Son bilinen oyuncu konumu

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newTile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            newTile.SetActive(false);
            tilePool.Enqueue(newTile);
        }

        lastPlayerPos = GetRoundedPlayerPosition(); // Ba�lang��ta oyuncunun yuvarlanm�� konumu al�n�r
        GenerateInitialTiles(); // �lk karo objeleri olu�turulur
    }

    private void Update()
    {
        Vector2 currentPlayerPos = GetRoundedPlayerPosition(); // G�ncel oyuncu konumu al�n�r

        // Oyuncunun konumu de�i�ti�inde karo objeleri g�ncellenir
        if (currentPlayerPos != lastPlayerPos)
        {
            GenerateNewTiles(currentPlayerPos); // Yeni karo objeleri olu�turulur
            RemoveOldTiles(currentPlayerPos); // Gereksiz karo objeleri kald�r�l�r
            lastPlayerPos = currentPlayerPos; // Son bilinen oyuncu konumu g�ncellenir
        }
    }

    // Oyuncunun konumunu yuvarlayarak tam say� de�erleri elde eder
    private Vector2 GetRoundedPlayerPosition()
    {
        return new Vector2(Mathf.Floor(player.position.x), Mathf.Floor(player.position.y));
    }

    // Ba�lang��ta g�r�nen karo objelerini olu�turur
    private void GenerateInitialTiles()
    {
        for (int i = -gridSize; i <= gridSize; i++)
        {
            for (int j = -gridSize; j <= gridSize; j++)
            {
                Vector2 pos = new Vector2(i, j);
                GameObject tile = GetTileFromPool();
                tile.transform.position = new Vector3(pos.x, pos.y, 0);
                tile.SetActive(true);
                tiles[pos] = tile;
            }
        }
    }

    // Oyuncunun hareket etti�i y�ne ba�l� olarak yeni karo objelerini olu�turur
    private void GenerateNewTiles(Vector2 currentPlayerPos)
    {
        int minX = Mathf.FloorToInt(currentPlayerPos.x) - gridSize;
        int maxX = Mathf.FloorToInt(currentPlayerPos.x) + gridSize;
        int minY = Mathf.FloorToInt(currentPlayerPos.y) - gridSize;
        int maxY = Mathf.FloorToInt(currentPlayerPos.y) + gridSize;

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                Vector2 pos = new Vector2(i, j);
                // E�er bu konumda bir karo objesi yoksa, yeni bir tane olu�turulur
                if (!tiles.ContainsKey(pos))
                {
                    GameObject tile = GetTileFromPool();
                    tile.transform.position = new Vector3(pos.x, pos.y, 0);
                    tile.SetActive(true);
                    tiles[pos] = tile;
                }
            }
        }
    }

    // Oyuncunun hareket etti�i y�ne ba�l� olarak gereksiz karo objelerini kald�r�r
    private void RemoveOldTiles(Vector2 currentPlayerPos)
    {
        List<Vector2> tilesToRemove = new List<Vector2>();

        foreach (KeyValuePair<Vector2, GameObject> tile in tiles)
        {
            // E�er karo objesi oyuncudan yeterince uzaksa, kald�r�l�r
            if (Mathf.Abs(tile.Key.x - currentPlayerPos.x) > gridSize || Mathf.Abs(tile.Key.y - currentPlayerPos.y) > gridSize)
            {
                tilesToRemove.Add(tile.Key);
                ReturnTileToPool(tile.Value);
            }
        }

        // Kald�r�lacak karo objelerini s�zl�kten siler
        foreach (Vector2 pos in tilesToRemove)
        {
            tiles.Remove(pos);
        }
    }

    // Kullan�lmayan bir karo objesini havuzdan al�r
    private GameObject GetTileFromPool()
    {
        if (tilePool.Count == 0)
        {
            Debug.LogWarning("Pool is empty! Instantiating a new tile.");
            GameObject newTile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            newTile.SetActive(false);
            return newTile;
        }

        return tilePool.Dequeue();
    }

    // Kullan�lan bir karo objesini havuza geri d�nd�r�r
    private void ReturnTileToPool(GameObject tile)
    {
        tile.SetActive(false);
        tilePool.Enqueue(tile);
    }
}