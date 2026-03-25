namespace Application.DTOs;

public sealed record DeviceRegistrationResponse(Guid UserId, string DeviceToken);