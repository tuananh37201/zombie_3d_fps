using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    /*========== KHAI BÁO VÀ KHỞI TẠO CÁC GIÁ TRỊ BAN ĐẦU ==========*/
    //-- Lấy TextMeshProUGUI
    public TextMeshProUGUI healthAmount;
    public TextMeshProUGUI armorAmount;

    //-- Khai báo máu và giáp ban đầu của nhân vật
    private float health = 100;
    private float armor = 50;


    /*========== CÁC HÀM TÍNH TOÁN ==========*/
    //-- Hàm kiểm tra va chạm
    private void OnTriggerEnter(Collider bited)
    {
        //Chạm vào gameObject có tab là Zombie sẽ trừ giáp, nếu giáp = 0 thì trừ máu
        if (bited.gameObject.CompareTag("Zombie"))
        {
            if (armor != 0)
            {
                armor -= 25;
                SetArmor();
            }
            else
            {
                health -= 20;
                SetHealth();
            }
        }

        //Nếu máu thấp hơn 100 thì nhặt máu sẽ được hổi 1 lượng máu = 10
        if (bited.gameObject.CompareTag("MedKit") && health < 100)
        {
            health += 10;
            SetHealth();
        }
    }

    /*========== CÁC HÀM ĐƯA THÔNG SỐ RA UI ==========*/
    //-- Máu
    void SetHealth()
    {
        healthAmount.text = health.ToString();
    }

    //-- Giáp
    void SetArmor()
    {
        armorAmount.text = armor.ToString();
    }

    /*========== START ==========*/
    void Start()
    {
        SetHealth();
        SetArmor();
    }

    private void Update()
    {
        if(health == 0)
        {
            SceneManager.LoadScene(4);
        }
    }
}
