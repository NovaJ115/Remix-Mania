using UnityEngine;
using UnityEngine.UI;

public class SelectedButton : MonoBehaviour
{
    public void SelectThisButton()
    {
        this.gameObject.GetComponent<Image>().color = Color.white;
    }
}
