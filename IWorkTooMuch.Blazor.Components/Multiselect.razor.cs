using IWorkTooMuch.Blazor.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace IWorkTooMuch.Blazor.Components
{
    public partial class Multiselect
    {
        private bool IsOpen { get; set; }
        private string SelectText { get; set; } = "";

        [Parameter]
        public IEnumerable<IEntity> Entities { get; set; }

        [Parameter]
        public List<IEntity> SelectedEntities { get; set; } = new();

        [Parameter]
        public EventCallback<List<IEntity>> SelectedEntitiesChanged { get; set; }

        private void ToggleEntity(IEntity entity)
        {
            if (SelectedEntities.Contains(entity))
                SelectedEntities.Remove(entity);
            else
                SelectedEntities.Add(entity);

            SelectedEntitiesChanged.InvokeAsync(SelectedEntities);
            UpdateSelectText();
        }

        private void UpdateSelectText()
        {
            if (SelectedEntities.Count == 0)
                SelectText = string.Empty;
            else
                SelectText = SelectedEntities?.Count > 3 ?
                $"{SelectedEntities.Count} Selected" :
                SelectedEntities.Select(x => x.Name).Aggregate((a, b) => $"{a}, {b}");
        }
    }
}
