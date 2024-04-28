using System;
using System.Collections.Generic;
using System.Diagnostics;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_WebAPI.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CMAC_Bienestar_WebAPI.Exceptions;

public class BienestarExceptionFilter : IExceptionFilter, IFilterMetadata
{
	private readonly IWebHostEnvironment webHostEnvironment;

	private readonly IModelMetadataProvider modelMetadataProvider;

	public BienestarExceptionFilter(IWebHostEnvironment webHostEnvironment, IModelMetadataProvider modelMetadataProvider)
	{
		this.webHostEnvironment = webHostEnvironment;
		this.modelMetadataProvider = modelMetadataProvider;
	}

	public void OnException(ExceptionContext context)
	{
		Response response = new Response
		{
			Status = RespuestaEnum.Invalid,
			Message = "Ocurrieron los siguientes errores al realizar la petición.",
			Errors = new List<Error>()
		};
		Exception exception = context.Exception;
		StackFrame frame = new StackTrace(exception, fNeedFileInfo: true).GetFrame(0);
		response.Errors.Add(new Error
		{
			Detalle = exception.Message,
			Metodo = exception.Source + "." + frame.GetMethod().Name + ", línea: " + frame.GetFileLineNumber() + "."
		});
		context.Result = new ObjectResult(response);
	}
}
