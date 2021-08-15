using System;

namespace SEG.Components.Blazor.Interfaces
{
    public interface IEntity : IEquatable<IEntity>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
