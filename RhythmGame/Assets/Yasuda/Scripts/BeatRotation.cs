using UnityEngine;

public class BeatRotation : MonoBehaviour
{
    public float beatsPerMinute = 120f; // デフォルトのBPMは120ですが、必要に応じて変更してください
    public float rotationAmount = 10f; // 回転角度

    private float timer;
    private Quaternion startRotation;
    private bool isRotating = false; // 回転のオン・オフを制御するフラグ

    void Start()
    {
        startRotation = transform.rotation; // キャラクターの初期回転を保存
        timer = 0f;
    }

    void Update()
    {
        if (isRotating)
        {
            // 回転を追加
            float rotationAmountThisFrame = Mathf.Sin(timer * Mathf.PI * (beatsPerMinute / 60f)) * rotationAmount;
            transform.rotation = startRotation * Quaternion.Euler(0f, 0f, rotationAmountThisFrame);

            // タイマーを更新
            timer += Time.deltaTime;
        }
    }

    // 回転をオンにする関数
    public void TurnOnRotation()
    {
        isRotating = true;
    }

    // 回転をオフにする関数
    public void TurnOffRotation()
    {
        isRotating = false;
    }
}