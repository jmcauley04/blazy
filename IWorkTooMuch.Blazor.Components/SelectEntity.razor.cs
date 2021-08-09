
using IWorkTooMuch.Blazor.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace IWorkTooMuch.Blazor.Components
{
    public abstract partial class SelectEntity
    {
        protected virtual bool showLoading => Suggestions == null;
        private bool showTable { get; set; }
        private ElementReference InputElement;

        protected string inputText { get; set; } = string.Empty;

        protected string errorMsg { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public string LoadingText { get; set; } = "Loading...";

        [Parameter]
        public List<IEntity> Selections { get; set; } = new();

        [Parameter]
        public EventCallback<List<IEntity>> SelectionsChanged { get; set; }

        [Parameter]
        public List<IEntity> Suggestions { get; set; }

        private void ToggleSelect(IEntity entity)
        {
            if (Selections.Contains(entity))
                Selections.Remove(entity);
            else
                Selections.Add(entity);

            InputElement.FocusAsync();
            SelectionsChanged.InvokeAsync(Selections);
        }

        private void ProcessKeyUp(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
                ProcessEnter();
        }

        protected abstract void ProcessEnter();

        private void RemoveSelection(IEntity selection)
        {
            Selections.Remove(selection);
            SelectionsChanged.InvokeAsync(Selections);
        }
    }
}
