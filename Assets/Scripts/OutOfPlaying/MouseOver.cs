using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseOver : MonoBehaviour
{
    private Material m_material;
    Color s = new Color(129 / 255f, 69 / 255f, 69 / 255f, 255 / 255f);
    // 加载脚本实例时调用 Awake
    private void Awake()
    {
        m_material = GetComponent<Renderer>().material;
    }

    // 当鼠标进入 GUIElement 或碰撞器时调用 OnMouseEnter
    private void OnMouseEnter()
    {
        m_material.color = s;
    }

    // 当鼠标不再停留在 GUIElement 或碰撞器上时调用 OnMouseExit
    private void OnMouseExit()
    {
        m_material.color = Color.white;
    }
}
