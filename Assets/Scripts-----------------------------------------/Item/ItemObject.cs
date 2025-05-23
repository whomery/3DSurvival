using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


public interface IInteractable
{
    public string GetInteractPrompt();
    public void Onlnteract();
}
public class ItemObject : MonoBehaviour , IInteractable
{

    public ItemData data;
    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}Wn{data.description}";
        return str;
    }
    public void Onlnteract()
    {
        ChharacterManager.Instance.Player.ItemData = data;
        ChharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }


}
