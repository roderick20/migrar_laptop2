using AutoMapper;
using CMAC_Bienestar_Core.ViewModels;
using CMAC_Bienestar_WebAPI.DTOs;

namespace CMAC_Bienestar_WebAPI.Mappers;

public class AutoMappers : Profile
{
	public AutoMappers()
	{
		((Profile)this).CreateMap<TallaVM, TallaDTO>().ReverseMap();
		((Profile)this).CreateMap<UniformeDTOCreate, UniformeVM>();
		((Profile)this).CreateMap<UniformeDTOUpdate, UniformeVM>();
		((Profile)this).CreateMap<GrupoDTOCreate, GrupoVM>();
		((Profile)this).CreateMap<GrupoDTOUpdate, GrupoVM>();
		((Profile)this).CreateMap<UsuarioDtoIn, UsuarioVM>();
		((Profile)this).CreateMap<UniformeGrupoDTOIn, UniformeGrupoVM>();
		((Profile)this).CreateMap<ColaboradorRHDTOCreate, ColaboradorRHVM>();
		((Profile)this).CreateMap<ColaboradorRHDTOUpdate, ColaboradorRHVM>();
		((Profile)this).CreateMap<ColaboradorRHVM, ColaboradorRHDTOOut>();
		((Profile)this).CreateMap<PeriodoDTOCreate, PeriodoVM>();
		((Profile)this).CreateMap<PeriodoDTOUpdate, PeriodoVM>();
		((Profile)this).CreateMap<SedeVM, SedeDTO>();
		((Profile)this).CreateMap<PuestoVM, PuestoDTO>();
		((Profile)this).CreateMap<UnidadVM, UnidadDTO>();
		((Profile)this).CreateMap<RegionVM, RegionDTO>();
		((Profile)this).CreateMap<PedidoDTOCreate, PedidoVM>();
		((Profile)this).CreateMap<ItemPedidoDTOCreate, ItemPedidoVM>();
		((Profile)this).CreateMap<ItemPedidoDTOUpdate, ItemPedidoVM>();
		((Profile)this).CreateMap<PedidoDTOCreate, PedidoVM>();
		((Profile)this).CreateMap<PedidoDTOUpdate, PedidoVM>();
		((Profile)this).CreateMap<EmailDTO, EmailVM>();
		((Profile)this).CreateMap<RolDTOCreate, RolVM>();
		((Profile)this).CreateMap<KardexDTOCreate, KardexVM>();
		((Profile)this).CreateMap<KardexDTOUpdate, KardexVM>();
		((Profile)this).CreateMap<TallaKardexVM, TallaKardexDTO>().ReverseMap();
		((Profile)this).CreateMap<GrupoEntidadVM, GrupoEntidadDTOOut>().ReverseMap();
		((Profile)this).CreateMap<GrupoEntidadDTOIn, GrupoEntidadVM>().ReverseMap();
	}
}
