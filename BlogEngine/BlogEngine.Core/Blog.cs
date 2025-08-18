using System;

namespace BlogEngine.Core
{
    /// <summary>
    /// Minimal stub for Blog class to resolve compilation dependencies
    /// This is a temporary implementation for .NET Core 8 migration
    /// </summary>
    public class Blog
    {
        public static Blog CurrentInstance { get; set; } = new Blog();
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "Default Blog";
        public bool IsDefault { get; set; } = true;
    }
}