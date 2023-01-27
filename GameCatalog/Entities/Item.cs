using System;

namespace GameCatalog.Entities
{
    // use Records type for immutable objs
    // with expressions support and 
    // value-based equality support
    public record Item
    {
        // init is an addition of C# 9 
        // (.net 5) it allows setting a value
        // only during initialization
        // init is better than 'private set'
        public Guid Id {get; init;}
        public string Name { get; init; }
        public decimal Price { get; init; }
        public DateTimeOffset CreationDate { get; init; }
    }
}