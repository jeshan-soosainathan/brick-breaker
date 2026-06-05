using UnityEngine;

public class BlockManager : MonoBehaviour
{

    public GameObject blockPrefab;

    public Sprite[] blockSprites;

    public int blockGap = 2;


    //Define a grid using numbers as index
    public string gridBlocks = "000 2222 000\n222222222222\n22 333 22\n333";

    public int blocks;
    public int blocksDestroyed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CreateBlockGrid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CreateBlockGrid()
    {
        //Get block size 
        Vector2 blockSize = blockPrefab.GetComponent<SpriteRenderer>().bounds.size;

        string[] rows = gridBlocks.Split("\n");

        for (int y = 0; y < rows.Length; y++)
        {
            string row = rows[y].Trim();
            float rowWidth = (blockSize.x + blockGap) * row.Length + blockGap * (row.Length - 1);

            float blockY = (blockSize.y + blockGap) * -y;


            for (int x = 0; x < row.Length; x++)
            {
                string blockStr = row[x].ToString();
                if (blockStr == " ")
                    continue;

                float blockX = (-rowWidth / 2) + (blockSize.x + blockGap) * x + (blockSize.x / 2);
                Vector3 pos = new Vector3(blockX, blockY, 0) + this.transform.position;

                GameObject block = Instantiate(blockPrefab, pos, Quaternion.identity, this.transform);
                blocks++;
                int spriteIndex = int.Parse(blockStr);
                Sprite blockSprite = blockSprites[spriteIndex];

                block.GetComponent<SpriteRenderer>().sprite = blockSprite;


            }


        }

    }


}
