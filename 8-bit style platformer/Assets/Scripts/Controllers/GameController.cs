using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField]
    Color[] defaultPalette = { Color.white, Color.white, Color.white, Color.clear };
    [SerializeField]
    Color[] paletteOne = { Color.white, Color.white, Color.white, Color.clear };
    [SerializeField]
    Color[] paletteTwo = { Color.white, Color.white, Color.white, Color.clear };
    [SerializeField]
    Color[] paletteThree = { Color.white, Color.white, Color.white, Color.clear };
    [SerializeField]
    Color[] paletteFour = { Color.white, Color.white, Color.white, Color.clear };

    public GameObject playerPrefab;
    public Texture2D spriteSheet;

    GameObject[] players;

    int currentPlayers;

    // Start is called before the first frame update
    void Start()
    {
        players = new GameObject[4];
        players.SetValue(GameObject.FindGameObjectWithTag("Player"), 0);
        currentPlayers = 0;
        CreatePlayer(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPlayers < 4)
        {
            if (Input.GetAxis("Submit_2") > 0.1f && players[1] == null)
            {
                CreatePlayer(2);
            }
            else if (Input.GetAxis("Submit_3") > 0.1f && players[2] == null)
            {
                CreatePlayer(3);
            }
            else if (Input.GetAxis("Submit_4") > 0.1f && players[3] == null)
            {
                CreatePlayer(4);
            }
        }
    }

    public Texture2D SetPalette(int paletteNum, int spriteX, int spriteY)
    {
        Texture2D texture = spriteSheet;

        Color[] pixels = texture.GetPixels(spriteX, spriteY, 8, 8);

        Texture2D newTexture = PaletteSwapper.SwapPalette(pixels, defaultPalette, SelectPalette(paletteNum));

        return newTexture;
    }

    public void CreatePlayer(int playerNum)
    {
        if (currentPlayers < 4)
        {
            players.SetValue(Instantiate(playerPrefab, new Vector3(-8 + playerNum * 2, 0f, 0), playerPrefab.transform.rotation), playerNum - 1);
            players[playerNum - 1].GetComponent<PlayerController>().IntializePlayer(playerNum);

            if (currentPlayers < playerNum)
            {
                currentPlayers = playerNum;
            }
        }
    }

    Color[] SelectPalette(int num)
    {
        if(num == 4)
        {
            return paletteFour;
        }
        else if (num == 3)
        {
            return paletteThree;
        }
        else if (num == 2)
        {
            return paletteTwo;
        }
        else
        {
            return paletteOne;
        }
    }

}
