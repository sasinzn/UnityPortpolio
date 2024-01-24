using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites; // 스프라이트 배열, 6개의 스프라이트를 할당합니다.

    public int currentSpriteIndex = 0;

    private void Start()
    {
        if (spriteRenderer == null || sprites.Length == 0)
        {
            Debug.LogError("SpriteRenderer or sprites are not set.");
            return;
        }

        // 초기 스프라이트 설정
        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }

    private void Update()
    {
        Select();
    }

    private void Select()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.instance.SelectPlane(currentSpriteIndex);
            PlayerInfo.instance.SelectChar((uint)currentSpriteIndex);
            GameManager.instance.GameStart();
        }

        if (Input.GetKeyDown(KeyCode.F3))
            GameManager.instance.GameEnd();

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (currentSpriteIndex <= 0)
                currentSpriteIndex = 0;
            else
                currentSpriteIndex--;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (currentSpriteIndex >= sprites.Length-1)
                currentSpriteIndex = sprites.Length - 1;
            else
                currentSpriteIndex++;
        }

        spriteRenderer.sprite = sprites[currentSpriteIndex];
    }

    public void OnClickFA8()
    {
        if(currentSpriteIndex==0)
        {
            GameManager.instance.SelectPlane(currentSpriteIndex);
            PlayerInfo.instance.SelectChar((uint)currentSpriteIndex);
            GameManager.instance.GameStart();
        }
        else
            currentSpriteIndex = 0;
    }

    public void OnClickFA117()
    {
        if (currentSpriteIndex == 1)
        {
            GameManager.instance.SelectPlane(currentSpriteIndex);
            PlayerInfo.instance.SelectChar((uint)currentSpriteIndex);
            GameManager.instance.GameStart();
        }
        else
            currentSpriteIndex = 1;
    }

    public void OnClickF22()
    {
        if (currentSpriteIndex == 2)
        {
            GameManager.instance.SelectPlane(currentSpriteIndex);
            PlayerInfo.instance.SelectChar((uint)currentSpriteIndex);
            GameManager.instance.GameStart();
        }
        else
            currentSpriteIndex = 2;
    }

    public void OnClickAV8()
    {
        if (currentSpriteIndex == 3)
        {
            GameManager.instance.SelectPlane(currentSpriteIndex);
            PlayerInfo.instance.SelectChar((uint)currentSpriteIndex);
            GameManager.instance.GameStart();
        }
        else
            currentSpriteIndex = 3;
    }

    public void OnClickF4()
    {
        if (currentSpriteIndex == 4)
        {
            GameManager.instance.SelectPlane(currentSpriteIndex);
            PlayerInfo.instance.SelectChar((uint)currentSpriteIndex);
            GameManager.instance.GameStart();
        }
        else
            currentSpriteIndex = 4;
    }

    public void OnClickX86()
    {
        if (currentSpriteIndex == 5)
        {
            GameManager.instance.SelectPlane(currentSpriteIndex);
            PlayerInfo.instance.SelectChar((uint)currentSpriteIndex);
            GameManager.instance.GameStart();
        }
        else
            currentSpriteIndex = 5;
    }
}
