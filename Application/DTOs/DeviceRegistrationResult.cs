using Domain.Shared;

namespace Application.DTOs;

public sealed record DeviceRegistrationResult(UserId UserId, string DeviceToken, string TokenHash);