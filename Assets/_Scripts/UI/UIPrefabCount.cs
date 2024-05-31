using UnityEngine;
using TMPro;

public class UIPrefabCount : MonoBehaviour
{
    [SerializeField] string objTag;
    private TMP_Text text;
    private const string msgText = "Total Objects: {0}";

    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();

        PoolManager.Instance.OnNumberObjectsChanged += UpdateText;
    }

    private void OnDisable()
    {
        PoolManager.Instance.OnNumberObjectsChanged -= UpdateText;
    }

    private void UpdateText(string tag, int number)
    {
        if (objTag != tag)
            return;

        text.text = string.Format(msgText, number);
    }
}
