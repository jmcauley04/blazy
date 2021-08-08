using IWorkTooMuch.Blazor.Components.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;

namespace IWorkTooMuch.Blazor.Components
{
    public partial class SelectSuggestedEntity
    {
        private bool showTable { get; set; }
        private string previousSearch = string.Empty;
        private ElementReference InputElement;

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public bool EnterSelectsEntity { get; set; } = false;

        [Parameter]
        public List<IEntity> Selections { get; set; } = new();

        [Parameter]
        public EventCallback<List<IEntity>> SelectionsChanged { get; set; }

        private List<IEntity> suggestions = new();
        [Parameter]
        public List<IEntity> Suggestions { get => suggestions; set { suggestions = value; } }

        [Parameter]
        public EventCallback<string> OnEnterPressed { get; set; }

        [Parameter]
        public int MinLengthForSuggestions { get; set; }

        private void ToggleSelect(IEntity entity)
        {
            if (Selections.Contains(entity))
                Selections.Remove(entity);
            else
                Selections.Add(entity);

            InputElement.FocusAsync();
            SelectionsChanged.InvokeAsync(Selections);
        }

        private string inputText { get; set; } = string.Empty;

        private string errorMsg { get; set; }

        private void ProcessKeyUp(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                if (EnterSelectsEntity)
                    ProcessAsSelection();
                else
                    ProcessAsSuggestionFilter();
            }
        }

        private void ProcessAsSuggestionFilter()
        {
            if (inputText.Trim().Length < MinLengthForSuggestions)
            {
                errorMsg = $"At least {MinLengthForSuggestions} chars required";
                return;
            }

            errorMsg = string.Empty;

            showTable = true;

            if (previousSearch != inputText)
            {
                previousSearch = inputText;
                OnEnterPressed.InvokeAsync(inputText);
            }
        }

        private void ProcessAsSelection()
        {
            if (int.TryParse(inputText, out int result))
            {
                var entity = Suggestions.FirstOrDefault(e => e.Id == result);
                if (entity == null)
                    errorMsg = "Id value not found";
                else if (Selections.Contains(entity))
                    errorMsg = "Entity already selected";
                else
                {
                    inputText = string.Empty;
                    Selections.Add(entity);
                    SelectionsChanged.InvokeAsync(Selections);
                }
            }
            else
                errorMsg = "Invalid Id value form";
        }

        private void RemoveSelection(IEntity selection)
        {
            Selections.Remove(selection);
            SelectionsChanged.InvokeAsync(Selections);
        }
    }
}