#pragma checksum "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9b54096df741c06dd5a34d4021af3b67e2c801c2"
// <auto-generated/>
#pragma warning disable 1591
namespace BlazorComponentsLibrary.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using BlazorComponentsLibrary;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using BlazorComponentsLibrary.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\_Imports.razor"
using IWorkTooMuch.Blazor.Components;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
using IWorkTooMuch.Blazor.Components.Interfaces;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
using Model;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "wrapper");
            __builder.AddAttribute(2, "b-jltdbnn05x");
#nullable restore
#line 7 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
     if (entities == null)
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(3, "<p b-jltdbnn05x>Loading...</p>");
#nullable restore
#line 10 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<IWorkTooMuch.Blazor.Components.SelectSuggestedEntity>(4);
            __builder.AddAttribute(5, "Title", "Suggested Select");
            __builder.AddAttribute(6, "EnterSelectsEntity", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Boolean>(
#nullable restore
#line 13 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                                            true

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(7, "Suggestions", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<IWorkTooMuch.Blazor.Components.Interfaces.IEntity>>(
#nullable restore
#line 13 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                                                               entities

#line default
#line hidden
#nullable disable
            ));
            __builder.CloseComponent();
#nullable restore
#line 14 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
     if (entities == null)
    {

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(8, "<p b-jltdbnn05x>Loading...</p>");
#nullable restore
#line 20 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
    }
    else
    {

#line default
#line hidden
#nullable disable
            __builder.OpenComponent<IWorkTooMuch.Blazor.Components.SelectSuggestedEntity>(9);
            __builder.AddAttribute(10, "Title", "Suggested Select");
            __builder.AddAttribute(11, "MinLengthForSuggestions", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Int32>(
#nullable restore
#line 23 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                                                 3

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "Suggestions", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<System.Collections.Generic.List<IWorkTooMuch.Blazor.Components.Interfaces.IEntity>>(
#nullable restore
#line 23 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                                                                 suggestions

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(13, "OnEnterPressed", Microsoft.AspNetCore.Components.CompilerServices.RuntimeHelpers.TypeCheck<Microsoft.AspNetCore.Components.EventCallback<System.String>>(Microsoft.AspNetCore.Components.EventCallback.Factory.Create<System.String>(this, 
#nullable restore
#line 23 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                                                                                              ProvideSuggestions

#line default
#line hidden
#nullable disable
            )));
            __builder.CloseComponent();
#nullable restore
#line 24 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
    }

#line default
#line hidden
#nullable disable
            __builder.OpenElement(14, "div");
            __builder.AddAttribute(15, "style", "width: max-content;");
            __builder.AddAttribute(16, "b-jltdbnn05x");
            __builder.OpenComponent<IWorkTooMuch.Blazor.Components.InputLabel>(17);
            __builder.AddAttribute(18, "Title", "Message Text");
            __builder.AddAttribute(19, "ChildContent", (Microsoft.AspNetCore.Components.RenderFragment)((__builder2) => {
                __builder2.OpenElement(20, "input");
                __builder2.AddAttribute(21, "value", Microsoft.AspNetCore.Components.BindConverter.FormatValue(
#nullable restore
#line 29 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                input

#line default
#line hidden
#nullable disable
                ));
                __builder2.AddAttribute(22, "onchange", Microsoft.AspNetCore.Components.EventCallback.Factory.CreateBinder(this, __value => input = __value, input));
                __builder2.SetUpdatesAttributeName("value");
                __builder2.AddAttribute(23, "b-jltdbnn05x");
                __builder2.CloseElement();
            }
            ));
            __builder.CloseComponent();
            __builder.AddMarkupContent(24, "\r\n        ");
            __builder.OpenElement(25, "div");
            __builder.AddAttribute(26, "class", "btnbox");
            __builder.AddAttribute(27, "b-jltdbnn05x");
            __builder.OpenElement(28, "button");
            __builder.AddAttribute(29, "class", "btn btn-primary");
            __builder.AddAttribute(30, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 32 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                      () => Notify(1)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(31, "b-jltdbnn05x");
            __builder.AddContent(32, "Test Default");
            __builder.CloseElement();
            __builder.AddMarkupContent(33, "\r\n            ");
            __builder.OpenElement(34, "button");
            __builder.AddAttribute(35, "class", "btn btn-success");
            __builder.AddAttribute(36, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 33 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                      () => Notify(2)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(37, "b-jltdbnn05x");
            __builder.AddContent(38, "Test Success");
            __builder.CloseElement();
            __builder.AddMarkupContent(39, "\r\n            ");
            __builder.OpenElement(40, "button");
            __builder.AddAttribute(41, "class", "btn btn-warning");
            __builder.AddAttribute(42, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 34 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                      () => Notify(3)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(43, "b-jltdbnn05x");
            __builder.AddContent(44, "Test Warning");
            __builder.CloseElement();
            __builder.AddMarkupContent(45, "\r\n            ");
            __builder.OpenElement(46, "button");
            __builder.AddAttribute(47, "class", "btn btn-danger");
            __builder.AddAttribute(48, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 35 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
                                                     () => Notify(4)

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(49, "b-jltdbnn05x");
            __builder.AddContent(50, "Test Danger");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
#nullable restore
#line 41 "C:\Users\Michael Cauley\source\repos\blazor-components\BlazorComponentsLibrary\Pages\Index.razor"
      

    [CascadingParameter]
    public Notifier Notifier { get; set; }

    public string input { get; set; }

    private void Notify(int num)
    {
        switch (num)
        {
            case 1:
                Notifier.ProcessNotification("Default", input);
                break;
            case 2:
                Notifier.ProcessSuccess("Succcess", input);
                break;
            case 3:
                Notifier.ProcessWarning("Warning", input);
                break;
            case 4:
                Notifier.ProcessError(new Exception(input));
                break;
        }
    }

    private List<IEntity> entities;

    private List<IEntity> suggestions;

    protected override void OnInitialized()
    {
        entities = new();

        for (int i = 0; i < 25; i++)
            entities.Add(new Entity()
            {
                Id = i,
                Name = $"entity {i}"
            });

        base.OnInitialized();
    }

    private async Task ProvideSuggestions(string search)
    {
        int totalOptions = 10000;

        int mySuggestions = totalOptions / (search.Length * 2);

        if (mySuggestions > 1000)
            Notifier.ProcessError(new Exception("More than 1000 suggestions found; please narrow search"));

        await Task.Delay(500);

        suggestions = new();

        for (int i = 0; i < mySuggestions; i++)
            suggestions.Add(new Entity()
            {
                Id = i,
                Name = $"entity {i}"
            });
    }


#line default
#line hidden
#nullable disable
    }
}
#pragma warning restore 1591
