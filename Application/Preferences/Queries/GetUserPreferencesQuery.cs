using Application.Abstractions;
using Application.DTOs;
using Domain.Shared;

namespace Application.Preferences.Queries;

public sealed record GetUserPreferencesQuery(UserId UserId
) : IQuery<UserPreferencesDto>;