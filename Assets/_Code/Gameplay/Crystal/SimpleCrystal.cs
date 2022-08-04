using UnityEngine;

[RequireComponent(typeof(CrystalFx))]
public class SimpleCrystal : MonoBehaviour
{
    [SerializeField] private CrystalFx _crystalFx = default;
    [SerializeField] private Transform _body = default;
    public void Disable()
    {
        _crystalFx.Enable();
        _body.gameObject.SetActive(false);
    }

    public void SetRotation(Quaternion angle)
    {
        _body.transform.rotation = angle;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _crystalFx = GetComponent<CrystalFx>();
        _body = transform.GetChild(0);
    }
#endif
}
