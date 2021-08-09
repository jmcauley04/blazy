using System.Linq;

namespace IWorkTooMuch.Blazor.Components
{
    public class SelectEntityListed : SelectEntity
    {
        protected override void ProcessEnter()
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
    }
}
