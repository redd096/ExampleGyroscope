using UnityEngine;

public class AnalogScript : MonoBehaviour
{
    [Header("Analog")]
    [SerializeField] RectTransform area = default;
    [SerializeField] RectTransform analog = default;
    [SerializeField] float smooth = 10;

    public void AnalogPosition(Vector2 position)
    {
        if (analog == null)
            return;

        //set analog position
        analog.position = Vector2.Lerp(analog.position, position, Time.deltaTime * smooth);

        //clamp in area
        Vector2 anchoredPosition = analog.anchoredPosition;
        anchoredPosition.x = Mathf.Clamp(anchoredPosition.x, -area.sizeDelta.x / 2, area.sizeDelta.x / 2);
        anchoredPosition.y = Mathf.Clamp(anchoredPosition.y, -area.sizeDelta.y / 2, area.sizeDelta.y / 2);

        analog.anchoredPosition = anchoredPosition;
    }

    public void ResetAnalogPosition()
    {
        if (analog == null)
            return;

        //reset analog position
        analog.anchoredPosition = Vector2.Lerp(analog.anchoredPosition, Vector2.zero, Time.deltaTime * smooth);
    }

    public Vector3 GetAnalogAnchoredPosition()
    {
        return analog.anchoredPosition;
    }
}
