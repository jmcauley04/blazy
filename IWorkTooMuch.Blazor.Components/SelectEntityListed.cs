using IWorkTooMuch.Blazor.Components.Interfaces;
using System.Linq;

namespace IWorkTooMuch.Blazor.Components
{
    public class SelectEntityListed<T> : SelectEntity<T> where T : IEntity
    {
        protected override void ProcessEnter()
        {
            var localError = string.Empty;
            int errors = 0;

            if (Suggestions == null)
            {
                errorMsg = "Still loading...";
                return;
            }

            foreach (var str in inputText.Split(','))
            {
                if (int.TryParse(str, out int result))
                {
                    var entity = Suggestions.FirstOrDefault(e => e.Id == result);
                    if (entity == null)
                    {
                        errors++;
                        localError = $"Id value not found: {str}";
                    }
                    else if (Selections.Contains(entity))
                    {
                        if (string.IsNullOrEmpty(localError))
                            localError = "Entity already selected";
                    }
                    else
                    {
                        inputText = string.Empty;
                        Selections.Add(entity);
                        SelectionsChanged.InvokeAsync(Selections);
                    }
                }
                else
                {
                    errors++;
                    localError = $"Invalid Id value form: {str}";
                }
            }

            if (!string.IsNullOrEmpty(localError))
                errorMsg = errors <= 1 ? localError : "Multiple input errors";


        }
    }
}
