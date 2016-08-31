using UnityEngine;
using System.Collections;

public class ManAnimEventRelayer : MonoBehaviour
{
    [SerializeField]
    WorldDriver driver;

    public void OnWhip()
    {
        driver.OnWhip();
    }

    public void OnRecover()
    {
        driver.OnRecover();
    }
}
