using UnityEngine;

public class ButtonDoor : IShootable
{
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        OnShot.AddListener(() =>
        {
            anim.SetTrigger("ShotTrigger");
        });
    }
}
