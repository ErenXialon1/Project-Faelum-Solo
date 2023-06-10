using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteScroll : MonoBehaviour

{
    public GameObject tilePrefab; // Görüntülenecek karo objelerinin prefab referansý
    public Transform player; // Oyuncunun konumunu temsil eden Transform referansý

    private const int poolSize = 100; // Karo objelerinin havuzunun maksimum boyutu
    private const int gridSize = 20; // Karo objelerinin yarý uzunluðu
    private Queue<GameObject> tilePool = new Queue<GameObject>(); // Kullanýlmayan karo objelerinin havuzu
    private Dictionary<Vector2, GameObject> tiles = new Dictionary<Vector2, GameObject>(); // Görünen karo objelerinin tutulduðu sözlük

    private Vector2 lastPlayerPos; // Son bilinen oyuncu konumu

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newTile = Instantiate(tilePrefab, Vector3.zero, Quaternion.identity);
            newTile.SetActive(false);
            tilePool.Enqueue(newTile);
        }

        lastPlayerPos = GetRoundedPlayerPosition(); // Baþlangýçta oyuncunun yuvarlanmýþ konumu alýnýr
        GenerateInitialTiles(); // Ýlk karo objeleri oluþturulur
    }

    private void Update()
    {
        Vector2 currentPlayerPos = GetRoundedPlayerPosition(); // Güncel oyuncu konumu alýnýr

        // Oyuncunun konumu deðiþtiðinde karo objeleri güncellenir
        if (currentPlayerPos != lastPlayerPos)
        {
            GenerateNewTiles(currentPlayerPos); // Yeni karo objeleri oluþturulur
            RemoveOldTiles(currentPlayerPos); // Gereksiz karo objeleri kaldýrýlýr
            lastPlayerPos = currentPlayerPos; // Son bilinen oyuncu konumu güncellenir
        }
    }

    // Oyuncunun konumunu yuvarlayarak tam sayý deðerleri elde eder
    private Vector2 GetRoundedPlayerPosition()
    {
        return new Vector2(Mathf.Floor(player.position.x), Mathf.Floor(player.position.y));
    }

    // Baþlangýçta görünen karo objelerini oluþturur
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

    // Oyuncunun hareket ettiði yöne baðlý olarak yeni karo objelerini oluþturur
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
                // Eðer bu konumda bir karo objesi yoksa, yeni bir tane oluþturulur
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

    // Oyuncunun hareket ettiði yöne baðlý olarak gereksiz karo objelerini kaldýrýr
    private void RemoveOldTiles(Vector2 currentPlayerPos)
    {
        List<Vector2> tilesToRemove = new List<Vector2>();

        foreach (KeyValuePair<Vector2, GameObject> tile in tiles)
        {
            // Eðer karo objesi oyuncudan yeterince uzaksa, kaldýrýlýr
            if (Mathf.Abs(tile.Key.x - currentPlayerPos.x) > gridSize || Mathf.Abs(tile.Key.y - currentPlayerPos.y) > gridSize)
            {
                tilesToRemove.Add(tile.Key);
                ReturnTileToPool(tile.Value);
            }
        }

        // Kaldýrýlacak karo objelerini sözlükten siler
        foreach (Vector2 pos in tilesToRemove)
        {
            tiles.Remove(pos);
        }
    }

    // Kullanýlmayan bir karo objesini havuzdan alýr
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

    // Kullanýlan bir karo objesini havuza geri döndürür
    private void ReturnTileToPool(GameObject tile)
    {
        tile.SetActive(false);
        tilePool.Enqueue(tile);
    }
}