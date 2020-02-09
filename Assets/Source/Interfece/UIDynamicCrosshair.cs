using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDynamicCrosshair : MonoBehaviour
{
    static public float spread = 0;

    public const int PISTOL_SHOOTING_SPREAD = 20;
    public const int JUMP_SPREAD = 50;
    public const int WALK_SPREAD = 10;
    public const int RUN_SPREAD = 25;

    public GameObject crosshair;
    public Color colorCrosshair = Color.red;

    private static UIDynamicCrosshair _instance;
    public static UIDynamicCrosshair Instance
    {
        get { return _instance; }
    }

    private Image topPart;
    private Image bottomPart;
    private Image leftPart;
    private Image rightPart;

    float initialPosition;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
    }
//
    void Start()
    {
        //crosshair.GetComponent<RectTransform>().localPosition = new Vector2(trnsform.parent / 2, Screen.height / 2);

        topPart = crosshair.transform.Find("TopPart").GetComponent<Image>();
        bottomPart = crosshair.transform.Find("BottomPart").GetComponent<Image>();
        leftPart = crosshair.transform.Find("LeftPart").GetComponent<Image>();
        rightPart = crosshair.transform.Find("RightPart").GetComponent<Image>();

        initialPosition = topPart.GetComponent<RectTransform>().localPosition.y;
    }

    void Update()
    {
        topPart.color = bottomPart.color = leftPart.color = rightPart.color = colorCrosshair;
        if (spread != 0)
        {
            topPart.GetComponent<RectTransform>().localPosition = new Vector3(0, initialPosition + spread, 0);
            bottomPart.GetComponent<RectTransform>().localPosition = new Vector3(0, -(initialPosition + spread), 0);
            leftPart.GetComponent<RectTransform>().localPosition = new Vector3(-(initialPosition + spread), 0, 0);
            rightPart.GetComponent<RectTransform>().localPosition = new Vector3(initialPosition + spread, 0, 0);
            spread -= 1;
        }
    }
}