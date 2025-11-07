namespace Waves.Code.Players.Core;

public sealed class Cooldown
{
    private readonly float _cooldownSeconds;
    private float _nextAllowedTime;

    public Cooldown(float cooldownSeconds)
        => _cooldownSeconds = cooldownSeconds;

    public bool CanShoot(float now)
        => now >= _nextAllowedTime;

    public void RecordShot(float now)
        => _nextAllowedTime = now + _cooldownSeconds;
}