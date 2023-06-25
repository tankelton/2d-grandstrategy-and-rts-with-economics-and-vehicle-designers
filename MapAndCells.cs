public class Cell
{
    public enum TerrainType
    {
        Grass,
        Water,
        Forest,
        Mountain
        // Add more as needed
    }

    public TerrainType Terrain { get; set; }
    public bool HasBuilding { get; set; }
    public string BuildingType { get; set; } // Could be an enum or a Building class depending on your design

    public Cell(TerrainType terrain)
    {
        Terrain = terrain;
        HasBuilding = false;
    }

    public void AddBuilding(string buildingType)
    {
        HasBuilding = true;
        BuildingType = buildingType;
    }

    public void RemoveBuilding()
    {
        HasBuilding = false;
        BuildingType = null;
    }
}

public class Map
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public Cell[,] Cells { get; private set; }

    public Map(int width, int height)
    {
        Width = width;
        Height = height;
        Cells = new Cell[width, height];

        GenerateTerrain();
    }

  private void GenerateTerrain()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                float xCoord = (float)i / Width;
                float yCoord = (float)j / Height;

                // Generate Perlin noise value between 0 and 1
                float noiseValue = Mathf.PerlinNoise(xCoord, yCoord);

                // Determine terrain type based on noise value
                Cell.TerrainType terrain;
                if (noiseValue < 0.3f)
                {
                    terrain = Cell.TerrainType.Water;
                }
                else if (noiseValue < 0.5f)
                {
                    terrain = Cell.TerrainType.Grass;
                }
                else if (noiseValue < 0.7f)
                {
                    terrain = Cell.TerrainType.Forest;
                }
                else
                {
                    terrain = Cell.TerrainType.Mountain;
                }

                Cells[i, j] = new Cell(terrain);
            }
        }
    }

    public void ChangeTerrain(int x, int y, Cell.TerrainType newTerrain)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Cells[x, y].Terrain = newTerrain;
        }
    }

    public void AddBuilding(int x, int y, string buildingType)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Cells[x, y].AddBuilding(buildingType);
        }
    }

    public void RemoveBuilding(int x, int y)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height)
        {
            Cells[x, y].RemoveBuilding();
        }
    }
}
