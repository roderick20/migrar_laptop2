using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using CMAC_Bienestar_Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.Hosting;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreGeneratedDocument;

[RazorCompiledItemMetadata("Identifier", "/Pages/Shared/PedidoDescuentoEmail.cshtml")]
[CreateNewOnMetadataUpdate]
internal sealed class Pages_Shared_PedidoDescuentoEmail : RazorPage<PedidoVM>
{
	private TagHelperExecutionContext __tagHelperExecutionContext;

	private TagHelperRunner __tagHelperRunner = new TagHelperRunner();

	private string __tagHelperStringValueBuffer;

	private TagHelperScopeManager __backed__tagHelperScopeManager = null;

	private HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;

	private BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;

	private TagHelperScopeManager __tagHelperScopeManager
	{
		get
		{
			if (__backed__tagHelperScopeManager == null)
			{
				__backed__tagHelperScopeManager = new TagHelperScopeManager(base.StartTagHelperWritingScope, base.EndTagHelperWritingScope);
			}
			return __backed__tagHelperScopeManager;
		}
	}

	[RazorInject]
	public IModelExpressionProvider ModelExpressionProvider { get; private set; } = null;


	[RazorInject]
	public IUrlHelper Url { get; private set; } = null;


	[RazorInject]
	public IViewComponentHelper Component { get; private set; } = null;


	[RazorInject]
	public IJsonHelper Json { get; private set; } = null;


	[RazorInject]
	public IHtmlHelper<PedidoVM> Html { get; private set; } = null;


	public override async Task ExecuteAsync()
	{
		WriteLiteral("\r\n");
		base.ViewBag.Title = "Pedido a Descuento";
		WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"es\">\r\n");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", TagMode.StartTagAndEndTag, "db9c120f6d3a2a9732541b03c5209c40a9cc0d9f7c903d3be5d0219a1688dc1d3343", async delegate
		{
			WriteLiteral("\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Resumen de Compra</title>\r\n    <style>\r\n        table {\r\n            width: 100%;\r\n            object-fit: contain;\r\n        }\r\n\r\n        .tableInferior {\r\n            width: 100%;\r\n            padding-top: 10PX;\r\n        }\r\n\r\n            .tableInferior thead tr th {\r\n                background-color: white;\r\n                color: black;\r\n                text-align: left;\r\n            }\r\n\r\n        .datos {\r\n            text-align: center;\r\n        }\r\n\r\n        .subtitulos {\r\n            padding-left: 30%;\r\n        }\r\n\r\n        a {\r\n            outline-color: red\r\n        }\r\n\r\n        .trLinea {\r\n            border-bottom: 50px solid #0087AE !important;\r\n        }\r\n\r\n        table thead tr th {\r\n            background-color: #01C5C6;\r\n            color: #fff;\r\n        }\r\n\r\n        th {\r\n            font-weight: 400;\r\n            line-height: 1.25;\r\n        }\r\n\r\n        table thead th {\r\n       ");
			WriteLiteral("     vertical-align: middle;\r\n        }\r\n\r\n        table tbody {\r\n            border-top: none;\r\n        }\r\n\r\n        img {\r\n            object-fit: contain;\r\n        }\r\n\r\n        .bloque {\r\n            width: 100%;\r\n            height: 50px;\r\n            background-color: #002559;\r\n        }\r\n    </style>\r\n\r\n");
		});
		__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<HeadTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n");
		__tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", TagMode.StartTagAndEndTag, "db9c120f6d3a2a9732541b03c5209c40a9cc0d9f7c903d3be5d0219a1688dc1d5707", async delegate
		{
			WriteLiteral("\r\n    <div class=\"bloque\">\r\n    </div>\r\n    <!-- CABECERA DEL RESUMEN -->\r\n    <table>\r\n        <tbody>\r\n            <tr>\r\n                <td>\r\n                    <img src=\"https://www.cajaarequipa.pe/wp-content/uploads/2023/06/logofot.svg\" width=\"70%\" height=\"200px\" alt=\"Card image cap\">\r\n                    <p>Direccion </p>\r\n                </td>\r\n                <td>\r\n                    <h1 style=\"padding-left: 10%;\">Orden de pedido Regular</h1>\r\n                </td>\r\n            </tr>\r\n        </tbody>\r\n\r\n    </table>\r\n    <!-- CUERPO (PARTE SUPERIOR ) DEL RESUMEN -->\r\n    <table>\r\n        <tbody>\r\n\r\n            <tr class=\"trLinea\">\r\n                <td>\r\n                    <h2>A</h2>\r\n                    <div class=\"datos\">\r\n                        <p> ");
			Write(base.Model.Nombre);
			WriteLiteral(" </p>\r\n                    </div>\r\n\r\n                </td>\r\n                <td>\r\n                    <div class=\"subtitulos\">\r\n                        <p><strong>Nro de Recibo</strong></p>\r\n                        <p><strong>Fecha</strong></p>\r\n                        <p><strong>N° Pedido</strong></p>\r\n                        <p><strong>Hora</strong></p>\r\n                    </div>\r\n\r\n                </td>\r\n                <td>\r\n                    <p> RE-");
			Write(base.Model.IdPedido);
			WriteLiteral(" </p>\r\n                    <p> ");
			Write(base.Model.FechaCrea.Date);
			WriteLiteral(" </p>\r\n                    <p> ");
			Write(base.Model.IdPedido);
			WriteLiteral(" </p>\r\n                    <p> ");
			Write(base.Model.FechaCrea.TimeOfDay);
			WriteLiteral(" </p>\r\n                </td>\r\n            </tr>\r\n            <tr style=\"height:5px; background-color: #0087AE\">\r\n                <td colspan=\"3\"></td>\r\n            </tr>\r\n           \r\n        </tbody>\r\n    </table>\r\n    <!-- CUERPO (PARTE INFERIOR ) DEL RESUMEN -->\r\n    <table class=\"tableInferior\">\r\n        <thead>\r\n            <tr>\r\n                <th><strong>Cantidad</strong></th>\r\n                <th><strong>Descripción</strong></th>\r\n                <th></th>\r\n                <th></th>\r\n                <th><strong>Precio Unitario</strong></th>\r\n                <th><strong>Importe</strong></th>\r\n            </tr>\r\n        </thead>\r\n        <tbody>          \r\n");
			foreach (ItemPedidoVM item in base.Model.ItemsPedidos)
			{
				WriteLiteral("                <tr>\r\n                    <td>");
				Write(item.Cantidad);
				WriteLiteral("</td>\r\n                    <td colspan=\"3\">");
				Write(item.Uniforme.Nombre);
				WriteLiteral("</td>\r\n                    <td>");
				Write(item.Uniforme.Precio);
				WriteLiteral("</td>\r\n                    <td>");
				Write(item.Uniforme.Precio);
				WriteLiteral("</td>\r\n                </tr>\r\n");
			}
			WriteLiteral("            <tr>\r\n                <td colspan =\"4\" >\r\n                </td>\r\n                <td>\r\n                    <h3><strong>Subtotal</strong></h3>\r\n                </td>\r\n                <td> S/. ");
			Write(base.Model.Total);
			WriteLiteral(" </td>\r\n            </tr>\r\n            <tr>\r\n                <td colspan=\"4\">\r\n                </td>\r\n                <td>\r\n                    <strong>IVA 21%</strong>\r\n                </td>\r\n                <td> S/. ");
			Write(base.Model.Iva);
			WriteLiteral(" </td>\r\n            </tr>\r\n");
			for (int i = 1; i <= base.Model.CantidadCuotas.Count; i++)
			{
				WriteLiteral("                <tr> \r\n                    <td colspan=\"4\">\r\n                    </td>\r\n                    <td>\r\n                        <strong>Cuota ");
				Write(i);
				WriteLiteral(" - Mes ");
				Write(i);
				WriteLiteral("</strong>\r\n                    </td>\r\n                    <td> S/ ");
				Write(base.Model.CantidadCuotas[i - 1]);
				WriteLiteral(" </td>\r\n                </tr>\r\n");
			}
			WriteLiteral("            <tr class=\"trLinea\"  style=\"height:5px; background-color: #0087AE\">\r\n                <td colspan=\"6\"></td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n    <table>\r\n        <tbody>\r\n            <tr >\r\n \r\n                <td colspan=\"2\">\r\n                    <h1>TOTAL </h1>\r\n                </td>\r\n                <td>\r\n                    <p><strong> S/. ");
			Write(base.Model.Total);
			WriteLiteral(" </strong></p>\r\n                </td>\r\n            </tr>\r\n            <tr  style=\"height:5px; background-color: #0087AE\">\r\n                <td colspan=\"3\"></td>\r\n            </tr>\r\n        </tbody>\r\n    </table>\r\n    <h2>Condiciones y Forma de Pago</h2>\r\n    <p>Acepto que se proseda el descuento por planilla</p>\r\n    <div class=\"bloque\">\r\n    </div>\r\n");
		});
		__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<BodyTagHelper>();
		__tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
		await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
		if (!__tagHelperExecutionContext.Output.IsContentModified)
		{
			await __tagHelperExecutionContext.SetOutputContentAsync();
		}
		Write(__tagHelperExecutionContext.Output);
		__tagHelperExecutionContext = __tagHelperScopeManager.End();
		WriteLiteral("\r\n\r\n</html>\r\n");
	}
}
