
public class ShootGrenade : IShootable
{
    Grenade nade;

    private void Start()
    {
        nade = GetComponentInParent<Grenade>();
        OnShot.AddListener(() =>
        {
            StartCoroutine(nade.Explode());
        });
    }
}
