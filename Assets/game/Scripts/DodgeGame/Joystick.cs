using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    //private Image bgImg;
    private RawImage bgImg;
    private Image joystickImg;
    private Vector3 inputVector;

    // Use this for initialization
    void Start()
    {
        //bgImg = GetComponent<Image>();
        bgImg = GetComponent<RawImage>();
        
        joystickImg = transform.GetChild(0).GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnDrag(PointerEventData ped)
    {
        //Debug.Log("Joystick >> OnDrag()");

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x); // 0 ~ -1
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y); // 0 ~ 1
            //Debug.Log(pos);

            // pos.x는 -1 ~ 1,  pos.y는 -1 ~ 1
            inputVector = new Vector3(pos.x * 2 - 1, pos.y * 2 - 1, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            // Move Joystick Image
            joystickImg.rectTransform.anchoredPosition
                    = new Vector3(inputVector.x * (bgImg.rectTransform.sizeDelta.x / 2),
                                  inputVector.y * (bgImg.rectTransform.sizeDelta.y / 2));
        }
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);

        PlayerPrefs.SetInt("onControl", 1);
        PlayerPrefs.Save();
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = Vector3.zero;

        PlayerPrefs.SetInt("onControl", 0);
        PlayerPrefs.Save();
    }

    // PlayerController 스크립트에서 inputVector.x 값을 받기 위해 사용될 함수
    public float GetHorizontalValue()
    {
        return inputVector.x;
    }

    // PlayerController 스크립트에서 inputVector.y 값을 받기 위해 사용될 함수
    public float GetVerticalValue()
    {
        return inputVector.y;
    }
}
