using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PaletteSwapper
{
    public static Texture2D SwapPalette(Color[] sprite, Color[] oldPalette, Color[] newPalette)
    {
        Texture2D response = new Texture2D(8, 8)
        {
            filterMode = FilterMode.Point
        };

        float colorDiff;

        for (int c = 0; c < sprite.Length; c++)
        {
            for (int i = 0; i < 4; i++)
            {
                colorDiff = new Vector4(
                    sprite[c].r - oldPalette[i].r,
                    sprite[c].g - oldPalette[i].g,
                    sprite[c].b - oldPalette[i].b,
                    sprite[c].a - oldPalette[i].a
                ).sqrMagnitude;

                //colorDiff = Mathf.Abs(pixels[c].grayscale - SelectPalette(0)[i].grayscale);

                if (colorDiff <= 4 * ((1f / 100f) * (1f / 100f)) && sprite[c].a != 0)
                //if (colorDiff <= ((1f / 100f)) && pixels[c].a != 0)
                {
                    sprite[c] = newPalette[i];
                    i = 4;
                }
            }
        }

        response.SetPixels(0, 0, 8, 8, sprite);
        response.Apply();
        return response;
    }
}
