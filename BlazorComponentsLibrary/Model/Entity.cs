using IWorkTooMuch.Blazor.Components.Interfaces;

namespace BlazorComponentsLibrary.Model
{
    public class Entity : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
