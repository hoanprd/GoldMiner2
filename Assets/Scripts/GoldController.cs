using UnityEngine;

/*public enum GoldType
{
    GoldLight,   // Vàng nhẹ
    GoldMedium,  // Vàng trung bình
    GoldHeavy,    // Vàng nặng
    Diamond,
    Pack,
    RockLight,
    RockMedium,
    RockHeavy
}*/

public class GoldController : MonoBehaviour
{
    //public GoldType goldType; // Loại vàng (Light, Medium, Heavy)
    private Transform hook; // Tham chiếu đến móc câu
    private bool isAttached = false; // Trạng thái vàng đã dính vào móc câu

    public bool randomPack;
    public int minRange, maxRange;
    public int value;
    public int weight;

    private MonoBehaviour movementScript; // Script di chuyển qua lại (ví dụ ObjectMover)

    /*public int lightGoldValue = 10;  // Điểm cho vàng nhẹ
    public int mediumGoldValue = 20; // Điểm cho vàng trung bình
    public int heavyGoldValue = 30;  // Điểm cho vàng nặng
    public int diamondValue = 40;
    public int packValue = 50;
    public int lightRockValue = 10;  // Điểm cho vàng nhẹ
    public int mediumRockValue = 20; // Điểm cho vàng trung bình
    public int heavyRockValue = 30;  // Điểm cho vàng nặng

    public float lightGoldWeight = 1f;  // Khối lượng của vàng nhẹ
    public float mediumGoldWeight = 2f; // Khối lượng của vàng trung bình
    public float heavyGoldWeight = 3f;  // Khối lượng của vàng nặng
    public float lightRockWeight = 1f;  // Khối lượng của vàng nhẹ
    public float mediumRockWeight = 2f; // Khối lượng của vàng trung bình
    public float heavyRockWeight = 3f;  // Khối lượng của vàng nặng*/

    private int goldValue; // Điểm của vàng hiện tại
    private float goldWeight; // Khối lượng của vàng

    void Start()
    {
        // Tìm script di chuyển qua lại trên object
        movementScript = GetComponent<ObjectMovementController>(); // Đổi thành script bạn đang sử dụng

        // Cập nhật giá trị điểm và khối lượng của vàng dựa trên loại vàng
        if (randomPack)
        {
            if (PlayerPrefs.GetInt("BuyLuckyUpValue") == 1)
            {
                minRange += 200;
                maxRange += 100;
            }
            goldValue = Random.Range(minRange, maxRange);
        }
        else
        {
            goldValue = value;
        }
        goldWeight = weight;

        /*switch (goldType)
        {
            case GoldType.GoldLight:
                goldValue = lightGoldValue;
                goldWeight = lightGoldWeight;
                break;
            case GoldType.GoldMedium:
                goldValue = mediumGoldValue;
                goldWeight = mediumGoldWeight;
                break;
            case GoldType.GoldHeavy:
                goldValue = heavyGoldValue;
                goldWeight = heavyGoldWeight;
                break;
            case GoldType.Diamond:
                goldValue = diamondValue;
                goldWeight = mediumGoldWeight;
                break;
            case GoldType.Pack:
                goldValue = packValue;
                goldWeight = mediumGoldWeight;
                break;
            case GoldType.RockLight:
                goldValue = lightRockValue;
                goldWeight = lightRockWeight;
                break;
            case GoldType.RockMedium:
                goldValue = mediumRockValue;
                goldWeight = mediumRockWeight;
                break;
            case GoldType.RockHeavy:
                goldValue = heavyRockValue;
                goldWeight = heavyRockWeight;
                break;
        }*/
    }

    void Update()
    {
        if (isAttached && hook != null)
        {
            // Vàng di chuyển cùng với móc câu
            transform.position = hook.position;
        }
    }

    public void AttachToHook(Transform hookTransform)
    {
        hook = hookTransform; // Gán móc câu hiện tại
        isAttached = true;    // Đánh dấu vàng đã được gắn

        // Dừng di chuyển qua lại
        if (movementScript != null)
        {
            movementScript.enabled = false;
        }
    }

    public void DetachFromHook()
    {
        isAttached = false;   // Đánh dấu trạng thái không còn gắn
        hook = null;          // Hủy tham chiếu đến hook

        // Kích hoạt lại di chuyển qua lại
        if (movementScript != null)
        {
            movementScript.enabled = true;
        }
    }

    public int GetGoldValue()
    {
        return goldValue; // Trả về giá trị điểm của vàng
    }

    public float GetGoldWeight()
    {
        return goldWeight; // Trả về khối lượng của vàng
    }
}