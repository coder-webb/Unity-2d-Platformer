using UnityEngine;

public class CoinBox : HittableFromBelow
{
    [SerializeField] int _maxCoins = 3;

    int _remainingCoins;

    protected override bool CanUse => _remainingCoins > 0;

    void Start()
    {
        _remainingCoins = _maxCoins;
    }

    protected override void Use()
    {
        base.Use();
        --_remainingCoins;
        Coin.CoinsCollected++;

    }
}
