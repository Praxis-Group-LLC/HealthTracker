namespace Domain.DeviceCredentials
{
    public readonly record struct DeviceCredentialId(Guid Value)
    {
        public static DeviceCredentialId New() => new(Guid.NewGuid());
        public override string ToString() => Value.ToString();
    }
}
