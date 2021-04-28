using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditPanel : MonoBehaviour
{
    [SerializeField]
    RectTransform creditContent = default;
    bool rolling = true;
    Vector3 initPos;
    float rollingSpeed = 15f;
    // Start is called before the first frame update
    void Awake()
    {
        initPos = creditContent.position;
    }

    public void OpenPanel()
    {
        rolling = true;
        creditContent.position = initPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (rolling)
        {
            creditContent.Translate(Vector3.up * rollingSpeed * Time.deltaTime);
        }
    }

    public void ClosePanel()
    {
        rolling = false;
    }
}
