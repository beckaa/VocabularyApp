using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAnimate : MonoBehaviour
{
    public Sprite[] sprites;
    public Image sprite;
    int frame = 0;
    int spritePerFrame = 10;
    int index=0;

    // Update is called once per frame
    void Update()
    {
        frame++;
        if(frame < spritePerFrame)
        {
            return;
        }   
        sprite.sprite = sprites[index];
        frame = 0;
        index++;
        if(index>= sprites.Length)
        {
            index = 0;
        }

    }
}
