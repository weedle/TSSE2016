using UnityEngine;
using System.Collections;

public class targettingDefaulltAnim : MonoBehaviour
{
    private float maxDisplacement = 4f;
    private Vector2 dir;
    private float dirChangeTimer;

    public Color inactive = new Color(0, 225, 0);
    public Color active = new Color(225, 0, 0);
    private bool activeStatus = true;
    private UnityEngine.UI.Button targetButton;
    private int speed = 4;

    // Use this for initialization
    void Start()
    {
        dir = Random.insideUnitCircle;
        targetButton =
            GameObject.Find("targettingButton")
            .GetComponent<UnityEngine.UI.Button>();
        targetButton.onClick.AddListener(delegate
        {
            toggleActive(targetButton);
        });
        toggleActive(targetButton);
        maxDisplacement *= this.transform.localScale.x;
    }

    void toggleActive(UnityEngine.UI.Button button)
    {
        if (activeStatus)
        {
            this
                .GetComponent<UnityEngine.UI.Image>()
                .color = inactive;
            speed = 4;
        }
        else
        {
            this
                .GetComponent<UnityEngine.UI.Image>()
                .color = active;
            speed = 10;
        }
        activeStatus = !activeStatus;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(
            new Vector3(0, 0, speed * 50 * dir.SqrMagnitude() * Time.deltaTime));

        Vector3 pos = transform.position -
            transform.parent.position;
        //{
        if ((pos.x > maxDisplacement && dir.x > 0) ||
        (pos.x < -maxDisplacement && dir.x < 0))
        {
            dir.x *= -1;
        }
        if ((pos.y >= maxDisplacement && dir.y > 0) ||
        (pos.y <= -maxDisplacement && dir.y < 0))
        {
            dir.y *= -1;
        }
        print("Pos is now: " + (transform.position - transform.parent.position));

        transform.position =
                new Vector3(pos.x + speed * maxDisplacement * Time.deltaTime * dir.x,
                            pos.y + speed * maxDisplacement * Time.deltaTime * dir.y, 0)
                             + transform.parent.position;

        dirChangeTimer += Time.deltaTime;

        if (dirChangeTimer > 3)
        {
            dirChangeTimer = 0;
            dir = Random.insideUnitCircle;
        }
    }
}
