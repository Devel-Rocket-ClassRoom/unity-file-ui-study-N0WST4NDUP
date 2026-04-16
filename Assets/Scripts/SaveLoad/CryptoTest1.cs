using UnityEngine;

public class CryptoTest1 : MonoBehaviour
{
    private byte[] encrypted;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            string plainText = "Hello~ AES";
            encrypted = CryptoUtil.Encrypt(plainText);
            Debug.Log($"암호화 완료: {encrypted}");
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (encrypted != null)
            {
                string plainText = CryptoUtil.Decrypt(encrypted);
                Debug.Log($"복호화 완료: {plainText}");
            }
        }
    }
}