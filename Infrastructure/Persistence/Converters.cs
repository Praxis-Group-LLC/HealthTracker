using Domain.Coping;
using Domain.DeviceCredentials;
using Domain.Journaling;
using Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence;

internal static class Converters
{
    public static readonly ValueConverter<DateOnly, DateTime> DateOnlyConverter =
        new(
            d => d.ToDateTime(TimeOnly.MinValue),
            dt => DateOnly.FromDateTime(dt));

    public static readonly ValueConverter<TimeOnly, TimeSpan> TimeOnlyConverter =
        new(
            t => t.ToTimeSpan(),
            ts => TimeOnly.FromTimeSpan(ts));

    public static PropertyBuilder<UserId> ConfigureUserId(
        this PropertyBuilder<UserId> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            id => id.Value,
            guid => new UserId(guid));
    }

    public static PropertyBuilder<DeviceCredentialId> ConfigureDeviceCredentialId(
        this PropertyBuilder<DeviceCredentialId> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            id => id.Value,
            guid => new DeviceCredentialId(guid));
    }

    public static PropertyBuilder<CopingExerciseId> ConfigureCopingExerciseId(
        this PropertyBuilder<CopingExerciseId> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            id => id.Value,
            guid => new CopingExerciseId(guid));
    }

    public static PropertyBuilder<CopingSessionId> ConfigureCopingSessionId(
        this PropertyBuilder<CopingSessionId> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            id => id.Value,
            guid => new CopingSessionId(guid)
        );
    }

    public static PropertyBuilder<JournalEntryId> ConfigureJournalEntryId(
        this PropertyBuilder<JournalEntryId> propertyBuilder)
    {
        return propertyBuilder.HasConversion(
            id => id.Value,
            guid => new JournalEntryId(guid));
    }

    public static PropertyBuilder<DateOnly> ConfigureDateOnly(
        this PropertyBuilder<DateOnly> propertyBuilder)
    {
        return propertyBuilder
            .HasConversion(DateOnlyConverter)
            .HasColumnType("date");
    }

    public static PropertyBuilder<TimeOnly> ConfigureTimeOnly(
        this PropertyBuilder<TimeOnly> propertyBuilder)
    {
        return propertyBuilder
            .HasConversion(TimeOnlyConverter)
            .HasColumnType("time");
    }
}