using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ELifeRPG.Infrastructure.Characters;

#pragma warning disable CS8602
public static class PositionDataJsonExtensions
{
    public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            AllowTrailingCommas = true,
            PropertyNameCaseInsensitive = true,
        };

        ValueConverter<T, string> converter = new ValueConverter<T, string>(
            v => JsonSerializer.Serialize(v, options),
            v => string.IsNullOrEmpty(v) ? new T() : JsonSerializer.Deserialize<T>(v, options) ?? new T());

        ValueComparer<T> comparer = new ValueComparer<T>(
            (l, r) => JsonSerializer.Serialize(l, options) == JsonSerializer.Serialize(r, options),
            v => v == null ? 0 : JsonSerializer.Serialize(v, options).GetHashCode(),
            v => JsonSerializer.Deserialize<T>(JsonSerializer.Serialize(v, options), options) ?? new T());

        propertyBuilder.HasConversion(converter);
        propertyBuilder.HasDefaultValue(new T());
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);
        propertyBuilder.HasColumnType("jsonb");

        return propertyBuilder;
    }
}
