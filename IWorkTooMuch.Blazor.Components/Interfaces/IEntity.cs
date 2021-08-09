using System;

namespace IWorkTooMuch.Blazor.Components.Interfaces
{
    public interface IEntity : IEquatable<IEntity>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
