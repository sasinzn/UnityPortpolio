using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 10.0f;
    public float stickRange = 100.0f;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 direction;

    private Vector3 screenLeftBottom;
    private Vector3 screenRightTop;

    public Sprite[] sprites; // ��������Ʈ �迭, 6���� ��������Ʈ�� �Ҵ��մϴ�.

    public int currentSpriteIndex = 0;
    private GameObject joypad;
    private Transform padStick;
    public bool touch = false;

    private void Awake() // ������Ʈ�� Ȱ��ȭ �ɶ� �ѹ� ȣ��Ǵ� �Լ�
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        Vector2 screenPos = new Vector2(Screen.width, Screen.height);

        screenLeftBottom = Camera.main.ScreenToWorldPoint(Vector2.zero);
        screenRightTop = Camera.main.ScreenToWorldPoint(screenPos);
        touch = true;

    }

    private void OnEnable() // ������Ʈ�� Ȱ��ȭ �ɶ����� ȣ��Ǵ� �Լ�
    {
        joypad = GameObject.Find("Joypad");
        padStick = joypad.transform.GetChild(0);
        joypad.SetActive(false);
        
    }

    private void Start() // ������Ʈ�� Ȱ��ȭ �ɶ� �ѹ� ȣ��Ǵ� �Լ�
    {
        currentSpriteIndex = GameManager.instance.GetSelectPlane();
        //���� ĳ���ͷ� �⺻ ��������Ʈ ����
        spriteRenderer.sprite = sprites[currentSpriteIndex];
        //���� ĳ���ͷ� ���ϸ����� ���̾� ����
        animator.SetLayerWeight(currentSpriteIndex, 1.0f);
    }

    private void Update()
    {
        KeyboardControl();
        TouchControl();
        
        Move();
        
    }

    private void KeyboardControl()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        animator.SetFloat("horizontal", horizontal);

        direction = Vector3.right * horizontal + Vector3.up * Vertical;

        if (direction.sqrMagnitude > 1)
            direction.Normalize();
    }

    private void TouchControl()
    {
        if(PlayerInfo.instance.touch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                joypad.SetActive(true);
                joypad.transform.position = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                padStick.position = Input.mousePosition;

                Vector2 stickPos = padStick.position;
                Vector2 padPos = joypad.transform.position;
                direction = (stickPos - padPos) / stickRange;

                if (direction.sqrMagnitude > 1.0f)
                {
                    direction.Normalize();
                    padStick.position = padPos + direction * stickRange;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                joypad.SetActive(false);

                direction = Vector2.zero;
            }
        }
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector2 halfSize = spriteRenderer.size * 0.5f;

        if (transform.position.x < screenLeftBottom.x + halfSize.x)
        {
            Vector3 pos = transform.position;
            pos.x = screenLeftBottom.x + halfSize.x;
            transform.position = pos;
        }

        if (transform.position.x > screenRightTop.x - halfSize.x)
        {
            Vector3 pos = transform.position;
            pos.x = screenRightTop.x - halfSize.x;
            transform.position = pos;
        }

        if (transform.position.y < screenLeftBottom.y + halfSize.y)
        {
            Vector3 pos = transform.position;
            pos.y = screenLeftBottom.y + halfSize.y;
            transform.position = pos;
        }

        if (transform.position.y > screenRightTop.y - halfSize.y)
        {
            Vector3 pos = transform.position;
            pos.y = screenRightTop.y - halfSize.y;
            transform.position = pos;
        }
    }

}
