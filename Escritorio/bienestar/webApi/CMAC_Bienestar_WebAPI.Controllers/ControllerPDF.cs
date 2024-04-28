using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CMAC_Bienestar_WebAPI.Controllers;

public static class ControllerPDF
{
	public static async Task<string> RenderViewAsync<PedidoVM>(this Controller controller, string viewName, PedidoVM pedidoDTOOut, bool partial = false)
	{
		if (string.IsNullOrEmpty(viewName))
		{
			viewName = controller.ControllerContext.ActionDescriptor.ActionName;
		}
		controller.ViewData.Model = pedidoDTOOut;
		using StringWriter writer = new StringWriter();
		IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
		ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, isMainPage: true);
		if (!viewResult.Success)
		{
			return "A view with the name " + viewName + " could not be found";
		}
		ViewContext viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, writer, new HtmlHelperOptions());
		await viewResult.View.RenderAsync(viewContext);
		return writer.GetStringBuilder().ToString();
	}
}
