using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Moq;
using MudBlazor;
using MudBlazor.Services;

namespace Aidn.WebApp.Test.Helpers;

internal static class ServiceCollectionExtensions
{
    public static void AddDefaultServices(this IServiceCollection services)
    {
        var keyInterceptorService = new Mock<IKeyInterceptor>();
        var keyInterceptorFactory = new Mock<IKeyInterceptorFactory>(MockBehavior.Strict);
        var mudPopoverService = new Mock<IMudPopoverService>();
        var scrollManagerService = new Mock<IScrollManager>(MockBehavior.Strict);
        var dialogService = new Mock<IDialogService>(MockBehavior.Strict);
        var snackbarService = new Mock<ISnackbar>(MockBehavior.Strict);
        var jsRuntime = new Mock<IJSRuntime>();

        keyInterceptorFactory.Setup(f => f.Create()).Returns(keyInterceptorService.Object);
        mudPopoverService.Setup(s => s.Handlers).Returns(new List<MudPopoverHandler>());
        mudPopoverService.Setup(s => s.Register(It.IsAny<RenderFragment>())).Returns(new MudPopoverHandler(b => b.CloseRegion(), jsRuntime.Object, () => { }));

        services.AddSingleton(dialogService.Object);
        services.AddSingleton(snackbarService.Object);
        services.AddSingleton(keyInterceptorFactory.Object);
        services.AddSingleton(mudPopoverService.Object);
        services.AddSingleton(scrollManagerService.Object);
    }
}
