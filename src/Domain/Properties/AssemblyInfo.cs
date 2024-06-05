using System.Runtime.CompilerServices;
using StronglyTypedIds;

[assembly:InternalsVisibleTo("ELifeRPG.Infrastructure")]
[assembly:InternalsVisibleTo("ELifeRPG.Core.Domain.UnitTests")]

[assembly:StronglyTypedIdDefaults(converters: StronglyTypedIdConverter.None)]
