using UnityEngine;

public class Builder : Bench
{

    public override void Create()
    {
        if(CanCraft())
        {
            ActiveCraftItem.gameObject.SetActive(false);
        }
    }
}
