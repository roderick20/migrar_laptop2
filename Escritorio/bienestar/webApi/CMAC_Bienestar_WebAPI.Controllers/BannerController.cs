using AutoMapper;
using CMAC_Bienestar_Core.Emuns;
using CMAC_Bienestar_Core.IRepositories;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_WebAPI.DTOs;
using CMAC_Bienestar_WebAPI.Exceptions;
using CMAC_Bienestar_WebAPI.Helpers;
using CMAC_Bienestar_WebAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMAC_Bienestar_WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[TypeFilter(typeof(BienestarExceptionFilter))]
public class BannerController : ControllerBase
{
	private readonly IBannerRepository bannerRepository;

	private readonly CustomMappers mapper;

	public BannerController(IBannerRepository bannerRepository, IMapper mapper)
	{
		this.bannerRepository = bannerRepository;
		this.mapper = new CustomMappers(mapper);
	}

	[HttpGet]
	public BannerVM ObtenerBanner()
	{
		return bannerRepository.ObtenerBanner();
	}

	[HttpPost]
	public ActionResult GuardarBanner(BannerDTOCreate bannerDTO)
	{
		BannerVM bannerVM = mapper.BannerDTOToBannerVM(bannerDTO);
		foreach (ImagenBannerVM imagene in bannerVM.Imagenes)
		{
			bannerRepository.AgregarImagenBanner(imagene);
		}
		int[] idsImagenesBannerEliminar = bannerDTO.IdsImagenesBannerEliminar;
		foreach (int idImagenBanner in idsImagenesBannerEliminar)
		{
			bannerRepository.EliminarImagenBanner(idImagenBanner);
		}
		return Ok(new Response
		{
			Status = RespuestaEnum.Success,
			Message = "Banner guardado correctamente."
		});
	}
}
