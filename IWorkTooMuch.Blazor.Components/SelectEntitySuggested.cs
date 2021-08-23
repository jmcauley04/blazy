using Microsoft.AspNetCore.Components;
using IWorkTooMuch.Blazor.Components.Interfaces;
using System.Collections.Generic;

namespace IWorkTooMuch.Blazor.Components
{
    public partial class SelectEntitySuggested<T> : SelectEntity<T> where T : IEntity
    {
        protected override bool showLoading => base.showLoading && pendingSuggestions;

        [Parameter]
        public int MinLengthForSuggestions { get; set; }

        [Parameter]
        public EventCallback<string> OnEnterPressed { get; set; }

        [Parameter]
        public EventCallback<List<T>> SuggestionsChanged { get; set; }

        private string previousSearch = string.Empty;
        private bool pendingSuggestions { get; set; } = false;

        protected override void ProcessEnter()
        {
            if (inputText.Trim().Length < MinLengthForSuggestions)
            {
                errorMsg = $"At least {MinLengthForSuggestions} chars required";
                return;
            }

            errorMsg = string.Empty;

            if (previousSearch != inputText)
            {
                previousSearch = inputText;
                Suggestions = null;
                pendingSuggestions = true;

                SuggestionsChanged.InvokeAsync(Suggestions);
                OnEnterPressed.InvokeAsync(inputText);
            }
        }
    }
}
